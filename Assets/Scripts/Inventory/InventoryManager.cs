using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour 
{
    public static InventoryManager Instance;

    [SerializeField] private List<SlootManager> slootManager = new List<SlootManager>();
    [SerializeField] private List<SlootManager> SlootChest = new List<SlootManager>();


    [Header("Player")]
    public GameObject PlayerObjSelect;

    public SlootData CurrentSlotSelect;
    public SlootData[] Obj;
    public SlootData weed;
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
        int i = Random.Range(0, Obj.Length);
        SlootData data = Obj[i];
        AddItem(data);
    }

    public bool AddItem(SlootData item)
    {
        if(item.itemType != ItemType.Static)
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

        if (item.itemType != ItemType.Static)
        {
            foreach (var slot in slootManager)
            {
                if (slot.slootData == null)
                {
                    slot.slootData = item;
                    slot.CurrentStorage = 1;
                    slot.StorageFull = (slot.CurrentStorage >= slot.slootData.MaxStorage);
                    slot.UpdateSlot();
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
