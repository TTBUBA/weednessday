using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour 
{
    public static InventoryManager Instance;

    public List<SlootManager> slootManager = new List<SlootManager>();
    public GameObject PanelInventory;

    [Header("Player")]
    public GameObject PlayerObjSelect;

    public SlootData CurrentSlotSelect;
    public SlootManager CurrentSlootManager;

    public SlootData Seedweed;
    public SlootData weed;
    public SlootData Cane;
    public int IdSlotCurrent;
    public bool isOpenInventory = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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

    //Add item 
    public bool AddItem(SlootData item, int amount)
    {
        // Check if the item is null or not
        if(item.itemType != ItemType.Seed)
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

        // Check if the item is not static and if there is an empty slot
        if (item.itemType != ItemType.Static)
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
        if (item.itemType != ItemType.Static)
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
        fromSlot.CurrentStorage = toSlot.CurrentStorage;
        fromSlot.StorageFull = toSlot.StorageFull;

        toSlot.slootData = tempSlootData;
        toSlot.CurrentStorage = tempStorage;
        toSlot.StorageFull = tempStorageFull;

        toSlot.UpdateSlot();
        fromSlot.UpdateSlot();
    }

}
