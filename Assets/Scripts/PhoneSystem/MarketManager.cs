using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public MarketSlot currentSlot;

    public PlayerManager playerManager;

    public void MakeOrder()
    {
        if(playerManager.CurrentMoney >= currentSlot.Price)
        {
            Debug.Log("Order made for: " + currentSlot.SlootMarket.NameTools);
        }
        else
        {
            Debug.Log("Order not made. because not have money.");
        }
    }
}
