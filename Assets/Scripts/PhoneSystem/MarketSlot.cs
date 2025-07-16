using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MarketSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SlootData SlootMarket;
    public int PriceItem;
    public int CurrentPrice;
    public int quantity = 1;

    [Header("Ui")]
    [SerializeField] private Image Img_Item;
    [SerializeField] private TextMeshProUGUI nameItem;
    [SerializeField] private TextMeshProUGUI Price_Item;
    [SerializeField] private TextMeshProUGUI Text_TotalItem;

    public MarketManager marketManager;

    private void Start()
    {
        Img_Item.sprite = SlootMarket.ToolsImages;
        nameItem.text = SlootMarket.NameTools;
        Price_Item.text = CurrentPrice.ToString() + "$";
        quantity = 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        marketManager.currentSlot = this;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        marketManager.currentSlot = null;
    }

    public void AddQuantity()
    {
        if(quantity <= 99)
        {
            quantity++;
            CurrentPrice += PriceItem;
            Price_Item.text = CurrentPrice.ToString() + "$";
            Text_TotalItem.text = quantity.ToString();
            //marketManager.marketSlots.Add(quantity, this);
            marketManager.TotalPriceCart += PriceItem;
            marketManager.Text_PriceTotal.text = "Total: "+ marketManager.TotalPriceCart.ToString() + "$";
        }
    }

    public void RemoveQuantity()
    {
        if (quantity >= 1)
        {
            quantity--;
            CurrentPrice -= PriceItem;
            Price_Item.text = CurrentPrice.ToString() + "$";
            Text_TotalItem.text = quantity.ToString();
            //marketManager.marketSlots.Remove(quantity);
            marketManager.TotalPriceCart -= PriceItem;
            marketManager.Text_PriceTotal.text = "Total: " + marketManager.TotalPriceCart.ToString() + "$";
        }
    }

    public void RemoveSlot()
    {
        Destroy(gameObject);
    }
}
