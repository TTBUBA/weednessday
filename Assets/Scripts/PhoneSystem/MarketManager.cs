using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public MarketSlot currentSlot;
    public PlayerManager playerManager;
    public int TotalItemAddCart;
    public Dictionary<int, MarketSlot> marketSlots = new Dictionary<int, MarketSlot>();
    public GameObject Obj_Spawn;
    public Transform PointSpawn;
    public int TotalPriceCart;

    [Header("Ui")]
    [SerializeField] private TextMeshProUGUI Text_ItemInTheCart;
    public TextMeshProUGUI Text_PriceTotal;


    public void MakeOrder()
    {
        if(playerManager.CurrentMoney >= currentSlot.CurrentPrice)
        {
            TotalItemAddCart++;
            Text_ItemInTheCart.text = TotalItemAddCart.ToString();
            GameObject obj = Instantiate(Obj_Spawn);
            obj.transform.SetParent(PointSpawn, false);
            MarketSlot marketSlot = obj.GetComponent<MarketSlot>();
            marketSlot.SlootMarket = currentSlot.SlootMarket;
            marketSlot.marketManager = this;
            marketSlot.PriceItem = currentSlot.PriceItem;
            marketSlot.CurrentPrice = currentSlot.PriceItem;
            TotalPriceCart += marketSlot.CurrentPrice;
            Text_PriceTotal.text = "Total: " + TotalPriceCart.ToString() + "$";
            //marketSlots.Add(marketSlot.quantity, marketSlot);
        }
        else
        {
            Debug.Log("Order not made. because not have money.");
        }
        /*
        foreach(KeyValuePair<int, MarketSlot> slot in marketSlots)
        {
            Debug.Log("Price Total" + slot.Value.CurrentPrice + slot.Value.PriceItem);
        }
        */
    }
}
