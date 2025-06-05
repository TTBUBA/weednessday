using UnityEngine;
using UnityEngine.Tilemaps;

public class UiManager : MonoBehaviour
{
    [Header("Panels-Utilty")]
    public GameObject Panel_Utility;
    [SerializeField] private GameObject Butt_OpenPanelUtilty;
    [SerializeField] private Tilemap DrawTile;

    [Header("Panels-Inventory")]
    [SerializeField] private GameObject Panel_Inventory;
    [SerializeField] private GameObject Butt_OpenPanel_Inventory;


    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlacementManager placementManager;
    private void Update()
    {
        UpdateUi();
    }

    public void OpenPanelInventory()
    {
        Panel_Inventory.SetActive(true);
        Butt_OpenPanel_Inventory.SetActive(false);
        Butt_OpenPanelUtilty.SetActive(false);
        inventoryManager.isOpenInventory = true;

    }
    public void ClosePanelInventory()
    {
        Panel_Inventory.SetActive(false);
        Butt_OpenPanel_Inventory.SetActive(true);
        Butt_OpenPanelUtilty.SetActive(true);
        inventoryManager.isOpenInventory = false;
    }

    public void OpenPanelUtilty()
    {
        Panel_Utility.SetActive(true);
        Butt_OpenPanelUtilty.SetActive(false);
        Butt_OpenPanel_Inventory.SetActive(false);
        DrawTile.gameObject.SetActive(true);
        placementManager.DrawModeActive = true;
    }

    public void ClosePanelUtilty()
    {
        Panel_Utility.SetActive(false);
        Butt_OpenPanelUtilty.SetActive(true);
        Butt_OpenPanel_Inventory.SetActive(true);
        DrawTile.gameObject.SetActive(false);
        placementManager.DrawModeActive = false;
    }

    private void UpdateUi()
    {
        if (inventoryManager.CurrentSlotSelect != null && inventoryManager.isOpenInventory)
        {
            inventoryManager.PlayerObjSelect.GetComponent<SpriteRenderer>().sprite = inventoryManager.CurrentSlotSelect.ToolsImages;
        }
    }
}
