using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public class ItemCard
    {
        public int quantity;
        public MarketSlot MarketSlot;
    }

    public MarketSlot currentSlot;
    public PlayerManager playerManager;
    public int TotalItemAddCart;
    public List<ItemCard> cartItems = new List<ItemCard>();
    public List<GameObject> ObjSpawn = new List<GameObject>();
    public GameObject Obj_Spawn;
    public Transform PointSpawn;
    public int TotalPriceCart;
    public bool isOrderMade = false;

    //===Player stats Tutorial===//
    public bool OpenAppFirstTime;
    public bool AddFirstObjetInCart;
    public bool EffectFirstOrder;
    public bool OpenCartFirstTime;


    [Header("Ui")]
    [SerializeField] private TextMeshProUGUI Text_ItemInTheCart;
    public TextMeshProUGUI Text_PriceTotal;

    public InventoryManager InventoryManager;
    public Boat_Order boatOrder;
    public BoxOrder boxOrder;

    public void OpenApp()
    {
        OpenAppFirstTime = true;
    }

    public void OpenCart()
    {
        OpenCartFirstTime = true;
    }
    public void AddToCart()
    {
        if (playerManager.CurrentMoney >= currentSlot.CurrentPrice)
        {
            AddFirstObjetInCart = true;
            TotalItemAddCart++;
            Text_ItemInTheCart.text = TotalItemAddCart.ToString();
            GameObject obj = Instantiate(Obj_Spawn);
            obj.transform.SetParent(PointSpawn, false);
            ObjSpawn.Add(obj);
            MarketSlot marketSlot = obj.GetComponent<MarketSlot>();
            marketSlot.SlootMarket = currentSlot.SlootMarket;
            marketSlot.marketManager = this;
            marketSlot.PriceItem = currentSlot.PriceItem;
            marketSlot.CurrentPrice = currentSlot.PriceItem;
            marketSlot.quantity = 1;
            TotalPriceCart += marketSlot.CurrentPrice;
            Text_PriceTotal.text = "Total: " + TotalPriceCart.ToString() + "$";

            //Save Item in the cart
            ItemCard item = new ItemCard();
            item.MarketSlot = marketSlot;
            item.quantity = marketSlot.quantity;
            cartItems.Add(item);
        }
        else
        {
            Debug.Log("Order not made. because not have money.");
        }
    }
    public void MakeOrder()
    {
        if (!EffectFirstOrder)
        {
            EffectFirstOrder = true;
            foreach (var item in cartItems)
            {
                playerManager.CurrentMoney -= TotalPriceCart; //remove money from the player

                InventoryManager.AddItem(item.MarketSlot.SlootMarket, 1);

                item.quantity = 0; // Reset the quantity after adding to the inventory
                item.MarketSlot = null; // Reset the market slot after adding to the inventory
                TotalPriceCart = 0; // Reset the total price after the order is made
                Text_PriceTotal.text = "Total: " + TotalPriceCart.ToString() + "$";
            }
        }
        else if (!isOrderMade && !boxOrder.OpenBox && EffectFirstOrder)
        {
            Debug.Log("Order Made");
            isOrderMade = true;
            boatOrder.StartCoroutine(boatOrder.TimeDelivery());
            boatOrder.StartCoroutine(boatOrder.UpdateTimeDelivery());
            boatOrder.Text_timedelivery.gameObject.SetActive(true);
            foreach (var item in cartItems)
            {
                playerManager.CurrentMoney -= TotalPriceCart; //remove money from the player

                BoxOrder.ItemChest chestItem = new BoxOrder.ItemChest();
                chestItem.quantity = item.quantity;
                chestItem.MarketSlot = item.MarketSlot;

                boxOrder.chestItems.Add(chestItem);

                item.quantity = 0; // Reset the quantity after adding to the box order
                item.MarketSlot = null; // Reset the market slot after adding to the box order
                TotalItemAddCart = 0;//reset the total item in the cart
                foreach (var obj in ObjSpawn)
                {
                    Destroy(obj);
                    TotalPriceCart = 0; // Reset the total price after the order is made
                    Text_PriceTotal.text = "Total: " + TotalPriceCart.ToString() + "$";
                }
            }
        }
    }

    //Update the cart item quantity and remove
    public void UpdateCartItem(MarketSlot marketSlot,int newQuantiy)
    {
        var item = cartItems.Find(i => i.MarketSlot == currentSlot);
        if (item != null)
        {
            item.quantity = newQuantiy;   
            if(newQuantiy <= 0)
            {
                cartItems.Remove(item);
            }
        }
    }
}

