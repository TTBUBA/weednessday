using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [Header("Input")]
    [SerializeField] private InputActionReference OpenChest;
    //[SerializeField] private InputActionReference CloseChest;

    [Header("Ui")]
    [SerializeField] private GameObject ButtOpenChest;
    [SerializeField] private TextMeshProUGUI Text_Chest;


    public Boat_Order boatOrder;
    public InventoryManager InventoryManager;

    private void OnEnable()
    {
        OpenChest.action.Enable();
        OpenChest.action.performed += OpenChestAction;
    }

    private void OnDisable()
    {
        OpenChest.action.Disable();
        OpenChest.action.performed -= OpenChestAction;
    }

    public void OpenChestAction(InputAction.CallbackContext context)
    {
        if (ActiveBox)
        {
            OpenChestOrder();
            StartCoroutine(DisactiveBox());
        }
    }

    //Open the chest and add items to the inventory
    public void OpenChestOrder() 
    {
        OpenBox = true;
        foreach (var item in chestItems)
        {
            //Debug.Log("You have ordered: " + item.quantity + " of " + item.MarketSlot.SlootMarket.NameTools);
            InventoryManager.AddItem(item.MarketSlot.SlootMarket, item.quantity);

            //Reset the list 
            item.quantity = 0; 
            item.MarketSlot = null;
        }
    }

    //Active Ui if the player collider with the box
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActiveBox = true;
            ButtOpenChest.SetActive(true);
            Text_Chest.text = "Open E";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
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
