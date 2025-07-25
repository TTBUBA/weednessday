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
    //=========//
    public bool isOpenInventory = false;
    public SlootManager draggedSlotController;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(slootManager[0].gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //Debugging method to test adding items
    public void Test()
    {
        AddItem(Seedweed, 1);
    }
    public void Addweed()
    {
        AddItem(weed, 2);
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
    //================//

    public void NextSlotController()
    {
        if (CurrentIndex >= 0 && CurrentIndex < slootManager.Count)
        {
            slootManager[CurrentIndex].AnimationSlotExit();
        }
        if(CurrentIndex >= slootManager.Count)
        {
            CurrentIndex = 0;
        }
        CurrentIndex++;
        CurrentSlotSelect = slootManager[CurrentIndex].slootData;
        slootManager[CurrentIndex].AnimationSlotEnter();

    }

    public void PreviousSlotController()
    {
        if (CurrentIndex >= 0 && CurrentIndex < slootManager.Count)
        {
            slootManager[CurrentIndex].AnimationSlotExit();
        }

        CurrentIndex--;
        if (CurrentIndex < 0)
        {
            CurrentIndex = slootManager.Count - 1; // Loop to the last slot
        }
        CurrentSlotSelect = slootManager[CurrentIndex].slootData;
        slootManager[CurrentIndex].AnimationSlotEnter();
    }

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
        // Check if the item is null or not
        if(item.itemType != ItemType.MultiplyItem)
        {
            foreach (var slot in slootManager)
            {
                if (slot.slootData == item && slot.CurrentStorage < slot.slootData.MaxStorage)
                {
                    slot.CurrentStorage++;
                    slot.UpdateSlot();
                    if (slot.CurrentStorage >= slot.slootData.MaxStorage)
                    {
                        slot.StorageFull = true;
                    }
                    return true;
                }
            }
        }

        // Check if the item is not singleItem and if there is an empty slot
        if (item.itemType != ItemType.singleItem)
        {
            foreach (var slot in slootManager)
            {
                if (slot.slootData == null)
                {
                    slot.slootData = item;
                    slot.CurrentStorage = amount;
                    slot.StorageFull = (slot.CurrentStorage >= slot.slootData.MaxStorage);
                    slot.UpdateSlot();
                    return true;
                }
            }
        }
        return false;
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
