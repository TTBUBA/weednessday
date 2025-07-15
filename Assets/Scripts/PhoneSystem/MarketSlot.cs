using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MarketSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SlootData SlootMarket;
    public int Price;


    [Header("Ui")]
    [SerializeField] private Image Img_Item;
    [SerializeField] private TextMeshProUGUI nameItem;
    [SerializeField] private TextMeshProUGUI Price_Item;


    public MarketManager marketManager;

    private void Start()
    {
        Img_Item.sprite = SlootMarket.ToolsImages;
        nameItem.text = SlootMarket.NameTools;
        Price_Item.text = Price.ToString() + "$";
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        marketManager.currentSlot = this;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        marketManager.currentSlot = null;
    }
}
