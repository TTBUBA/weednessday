using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Panels-Utilty")]
    public GameObject Panel_Utility;
    [SerializeField] private GameObject Butt_OpenPanelUtilty;
    [SerializeField] private GameObject DrawTile;
    [SerializeField] private Image ImgCurrentUtily;
    [SerializeField] private TextMeshProUGUI Text_UtilityName;
    [SerializeField] private TextMeshProUGUI Text_UtilitySpaceOccupied;
    [SerializeField] private TextMeshProUGUI Text_UtilityCost;

    [Header("Panels-Inventory")]
    [SerializeField] private GameObject Panel_Inventory;
    [SerializeField] private GameObject Butt_ClosePanelInventory;

    [Header("Player-Ui")]
    [SerializeField] private Image BarLife;
    [SerializeField] private TextMeshProUGUI Text_CurrentMoney;
    [SerializeField] private GameObject Butt_UseDrog;


    [Header("Managers")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlacementManager placementManager;
    [SerializeField] private PlayerManager playerManager;

    private void Update()
    {
        UiInventory();
        UiPlayer();
        UiPanelUtilty();
        UiButtonUseDrog();
    }

    public void UiPanelUtilty()
    {
        if (!placementManager.CurrentplaceableObject) { return; }

        ImgCurrentUtily.sprite = placementManager.CurrentplaceableObject.UtilityIcon;
        Text_UtilityName.text = placementManager.CurrentplaceableObject.UtilityName;
        Text_UtilitySpaceOccupied.text = "Space Occupied:" + placementManager.CurrentplaceableObject.SpaceOccupied;
        Text_UtilityCost.text = "Price:" + placementManager.CurrentplaceableObject.Cost.ToString();
        //remove currentslot when the panel is open 
        inventoryManager.CurrentSlotSelect = null;
    }
    public void OpenPanelInventory()
    {
        Panel_Inventory.SetActive(true); 
        inventoryManager.isOpenInventory = true;
        Butt_ClosePanelInventory.SetActive(true);

    }

    public void ClosePanelInventory()
    {
        Panel_Inventory.SetActive(false);
        inventoryManager.isOpenInventory = false;
        Butt_ClosePanelInventory.SetActive(false);
    }

    public void OpenPanelUtilty()
    {
        Panel_Utility.SetActive(true);
        Butt_OpenPanelUtilty.SetActive(false);
        DrawTile.SetActive(true);
        placementManager.DrawModeActive = true;
        placementManager.CurrentplaceableObject = null;
        Destroy(placementManager.LastObjSpawn);
        placementManager.LastObjSpawn = null;
    }
    public void ClosePanelUtilty()
    {
        Panel_Utility.SetActive(false);
        Butt_OpenPanelUtilty.SetActive(true);
        DrawTile.SetActive(false);
        placementManager.DrawModeActive = false;
        placementManager.CurrentplaceableObject = null;
        placementManager.IsPlacementActive = false;
        Destroy(placementManager.LastObjSpawn);
        placementManager.LastObjSpawn = null;
    }
    private void UiInventory()
    {
        if (inventoryManager.CurrentSlotSelect != null && inventoryManager.isOpenInventory)
        {
            inventoryManager.PlayerObjSelect.GetComponent<SpriteRenderer>().sprite = inventoryManager.CurrentSlotSelect.ToolsImages;
        }
    }   
    private void UiPlayer()
    {
        Text_CurrentMoney.text = playerManager.CurrentMoney.ToString();
    }

    private void UiButtonUseDrog()
    {
        if(inventoryManager.CurrentSlotSelect == null) { return; }

        if (inventoryManager.CurrentSlotSelect.NameTools == "Cane" && playerManager.ActiveButtun)
        {
           Butt_UseDrog.SetActive(true);
        }
        else
        {
            Butt_UseDrog.SetActive(false);
        }
    }
}