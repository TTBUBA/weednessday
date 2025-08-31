using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BoxOrder : MonoBehaviour
{
    public class ItemChest
    {
        public int quantity;
        public MarketSlot MarketSlot;
    }

    public List<ItemChest> chestItems = new List<ItemChest>();
    public bool ActiveBox;
    public bool OpenBox = false;

    [Header("Ui")]
    [SerializeField] private GameObject ButtOpenChest;
    [SerializeField] private TextMeshProUGUI Text_Chest;


    public Boat_Order boatOrder;
    public InventoryManager InventoryManager;

    //Open the chest and add items to the inventory
    public void OpenChestOrder() 
    {
        if (ActiveBox)
        {
            OpenBox = true;
            foreach (var item in chestItems)
            {
                InventoryManager.AddItem(item.MarketSlot.SlootMarket, item.quantity);

                //Reset the list 
                item.quantity = 0;
                item.MarketSlot = null;
            }
            StartCoroutine(DisactiveBox());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActiveBox = true;
            ButtOpenChest.SetActive(true);
            Text_Chest.text = "Open E";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActiveBox = false;
            ButtOpenChest.SetActive(false);
            Text_Chest.text = "Close Q";
        }
    }

    IEnumerator DisactiveBox()
    {
        yield return new WaitForSeconds(1.5f);
        ActiveBox = false;
        ButtOpenChest.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
