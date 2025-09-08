using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, ISaveable
{
    public static InventoryManager Instance;

    public List<SlootManager> slootManager = new List<SlootManager>();
    public GameObject PanelInventory;

    [Header("Player")]
    public GameObject PlayerObjSelect;

    public SlootData CurrentSlotSelect;
    public SlootManager CurrentSlootManager;
    public int CurrentIndex;
    public int PreviusIndex = -1;

    //ITEM DEBUG//
    public SlootData Seedweed;
    public SlootData weed;
    public SlootData Cane;
    public SlootData Plastic;
    public SlootData Battery;
    public SlootData baggie;
    public SlootData FirtKit;
    public SlootData AmmoShooGun;
    //=========//

    public bool isOpenInventory = false;
    public bool ActiveInventory = true;
    public SlootManager draggedSlotController;
    public GameObject LastObjSelect;
    public GameObject currentSelectedObject;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(slootManager[0].gameObject);
        currentSelectedObject = slootManager[0].gameObject;
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SaveSystem.Instance.saveables.Add(this);
        SaveSystem.Instance.LoadGame();
    }
    private void Update()
    {
        //save the currentSelectedObject select in the EventSystem
        currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        if (currentSelectedObject != null && currentSelectedObject != LastObjSelect)
        {
            //save the previous index
            int prevIndex = CurrentIndex;
            LastObjSelect = currentSelectedObject;

            SlootManager slot = currentSelectedObject.GetComponent<SlootManager>();
            if (slot != null)
            {
                CurrentSlootManager = slot;
                CurrentIndex = slot.Index;
                CurrentSlotSelect = slot.slootData;
                slot.AnimationSlotEnter();
            }

            // If the previous index is valid, exit the animation for that slot
            if (prevIndex < slootManager.Count)
            {
                slootManager[prevIndex].AnimationSlotExit();
            }
        }

        ChangeSlot();
    }

    //change sloot when using number keyboard
    public void ChangeSlot()
    {
        for(int i = 0; i < slootManager.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSloot(i);
                return;
            }
        }
    }

    private void SetSloot(int index)
    {
        if (index < 0 || index >= slootManager.Count) return;



        if (PreviusIndex != -1 && PreviusIndex < slootManager.Count)
        {
            slootManager[PreviusIndex].AnimationSlotExit();
        }

        CurrentIndex = index;
        CurrentSlootManager = slootManager[index];
        CurrentSlotSelect = CurrentSlootManager.slootData;

        CurrentSlootManager.AnimationSlotEnter();
        PreviusIndex = index;
    }

    public void save(GameData data)
    {
        foreach (var slot in slootManager)
        {
            SlootSaveData saveSlot = new SlootSaveData();
            saveSlot.slootData = slot.slootData;
            saveSlot.CurrentStorage = slot.CurrentStorage;
            data.slootSlots.Add(saveSlot);
        }
    }

    public void load(GameData data)
    {
        for (int i = 0 ;i < slootManager.Count && i < data.slootSlots.Count; i++)
        {
            SlootSaveData saveSlot = data.slootSlots[i];
            slootManager[i].slootData = saveSlot.slootData;
            slootManager[i].CurrentStorage = saveSlot.CurrentStorage;
            slootManager[i].UpdateSlot();
        }
    }


    //Debugging method to test adding items
    public void Test()
    {
        AddItem(Seedweed, 1);
    }

    public void AddAmmoShootGun()
    {
        AddItem(AmmoShooGun, AmmoShooGun.MaxStorage);
    }

    public void Addweed()
    {
        AddItem(weed, weed.MaxStorage);
    }
    public void Removeweed(int totalRemove)
    {
        RemoveItem(weed, totalRemove);
    }

    public void AddCane()
    {
        AddItem(Cane,1);
    }

    public void RemoveCane()
    {
        RemoveItem(Cane, 1);
    }

    public void RemoveSeedWeed()
    {
        RemoveItem(Seedweed, 1);
    }

    public void AddPlastic()
    {
        AddItem(Plastic, 10);
    }

    public void AddBattery()
    {
        AddItem(Battery, Battery.MaxStorage);
    }
    public void AddBaggie()
    {
        AddItem(baggie, baggie.MaxStorage);
    }
    public void AddFirstKit()
    {
        AddItem(FirtKit, FirtKit.MaxStorage);
    }
    //================//

    public void DragItemController()
    {
        var draggedSlot = slootManager[CurrentIndex];
        draggedSlotController = draggedSlot;
        if (draggedSlot.slootData != null && draggedSlot.CurrentStorage > 0)
        {
            draggedSlot.iconTools.transform.SetAsLastSibling();
            draggedSlot.iconTools.transform.position = Input.mousePosition;
        }
    }

    public void DropItemController()
    {
        var ObjectSelected = EventSystem.current.currentSelectedGameObject;
        CurrentSlootManager = ObjectSelected.GetComponent<SlootManager>();
        DropItem(CurrentSlootManager, draggedSlotController);
        draggedSlotController = null;

    }

    //Add item 
    public bool AddItem(SlootData item, int amount)
    {
        if (item == null || amount <= 0) return false;

        int remaining = amount;
        // before full item exist that contains the item
        foreach (var slot in slootManager)
        {
            if (slot.slootData == item && slot.CurrentStorage < slot.slootData.MaxStorage)
            {
                int space = slot.slootData.MaxStorage - slot.CurrentStorage;
                int toAdd = Mathf.Min(space, remaining);
                slot.CurrentStorage += toAdd;
                slot.UpdateSlot();
                slot.StorageFull = (slot.CurrentStorage >= slot.slootData.MaxStorage);
                remaining -= toAdd;

                if (remaining <= 0) return true;
            }
        }

        // if the slot is empty, try to add the item
        foreach (var slot in slootManager)
        {
            if (slot.slootData == null)
            {
                slot.slootData = item;
                int toAdd = Mathf.Min(item.MaxStorage, remaining);
                slot.CurrentStorage = toAdd;
                slot.StorageFull = (slot.CurrentStorage >= slot.slootData.MaxStorage);
                slot.UpdateSlot();
                remaining -= toAdd;

                if (remaining <= 0) return true;
            }
        }

        return remaining <= 0;
    }

    public bool RemoveItem(SlootData item, int total)
    {
        if (item.itemType != ItemType.singleItem)
        {
            foreach (var slot in slootManager)
            {
                if (slot.slootData == item && slot.CurrentStorage < slot.slootData.MaxStorage)
                {
                    slot.CurrentStorage -= total;
                    slot.UpdateSlot();
                    if (slot.CurrentStorage >= slot.slootData.MaxStorage)
                    {
                        slot.StorageFull = true;
                    }
                    return true;
                }
            }
        }
        return false;
    }
    public void DropItem(SlootManager fromSlot, SlootManager toSlot)
    {
        if (fromSlot.slootData == null) return;

        if (toSlot.slootData == null)
        {
            toSlot.slootData = fromSlot.slootData;
            toSlot.CurrentStorage = fromSlot.CurrentStorage;
            toSlot.StorageFull = fromSlot.StorageFull;

            fromSlot.slootData = null;
            fromSlot.CurrentStorage = 0;
            fromSlot.StorageFull = false;
        }
        else if (toSlot.slootData == fromSlot.slootData && !toSlot.StorageFull)
        {
            int space = toSlot.slootData.MaxStorage - toSlot.CurrentStorage;
            int toAdd = Mathf.Min(space, fromSlot.CurrentStorage);

            toSlot.CurrentStorage += toAdd;
            fromSlot.CurrentStorage -= toAdd;

            toSlot.StorageFull = (toSlot.CurrentStorage >= toSlot.slootData.MaxStorage);

            if (fromSlot.CurrentStorage <= 0)
            {
                fromSlot.slootData = null;
                fromSlot.StorageFull = false;
            }
        }
        else
        {
            var tempData = toSlot.slootData;
            var tempStorage = toSlot.CurrentStorage;
            var tempFull = toSlot.StorageFull;

            toSlot.slootData = fromSlot.slootData;
            toSlot.CurrentStorage = fromSlot.CurrentStorage;
            toSlot.StorageFull = fromSlot.StorageFull;

            fromSlot.slootData = tempData;
            fromSlot.CurrentStorage = tempStorage;
            fromSlot.StorageFull = tempFull;
        }

        fromSlot.UpdateSlot();
        toSlot.UpdateSlot();
    }
}
