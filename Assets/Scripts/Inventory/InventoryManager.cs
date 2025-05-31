using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour 
{
    public static InventoryManager Instance;

    [Header("Player")]
    [SerializeField] private GameObject PlayerObjSelect;

    [Header("Inventory-Ui")]
    [SerializeField] private GameObject PanelUi;

    public SlootData CurrentSlotSelect;
    public int IdSlotCurrent;
    private bool isOpenInventory = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        UpdateUi();
    }

    public void OpenInventory()
    {
       PanelUi.SetActive(true);





       isOpenInventory = true;
    }

    public void CloseInventory()
    {
        PanelUi.SetActive(false);
        isOpenInventory = false;
    }

    private void UpdateUi()
    {
        if (CurrentSlotSelect != null && isOpenInventory)
        {
            PlayerObjSelect.GetComponent<SpriteRenderer>().sprite = CurrentSlotSelect.ToolsImages;
        }
    }

}
