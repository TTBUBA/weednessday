using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour 
{
    public static InventoryManager Instance;

    public List<SlootManager> slootManager = new List<SlootManager>();
    public GameObject PanelInventory;

    [Header("Player")]
    public GameObject PlayerObjSelect;

    public SlootData CurrentSlotSelect;
    public SlootManager CurrentSlootManager;
    public int CurrentIndex;


    //ITEM DEBUG//
    public SlootData Seedweed;
    public SlootData weed;
    public SlootData Cane;
    public SlootData Plastic;
    public SlootData Battery;
    public SlootData ziploc;
    //=========//
    public bool isOpenInventory = false;
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
    }
    //Debugging method to test adding items
    public void Test()
    {
        AddItem(Seedweed, 1);
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
    public void AddZiploc()
    {
        AddItem(ziploc, ziploc.MaxStorage);
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
        SlootData tempSlootData = fromSlot.slootData;
        int tempStorage = fromSlot.CurrentStorage;
        bool tempStorageFull = fromSlot.StorageFull;


        fromSlot.slootData = toSlot.slootData;
        fromSlot.CurrentStorage += toSlot.CurrentStorage;
        fromSlot.StorageFull = toSlot.StorageFull;

        toSlot.slootData = null;
        toSlot.CurrentStorage = 0;
        toSlot.StorageFull = false;

        toSlot.UpdateSlot();
        fromSlot.UpdateSlot();
    }

}
