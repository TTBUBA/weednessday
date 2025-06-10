using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour 
{
    public static InventoryManager Instance;

    [SerializeField] private SlootManager[] slootManager;

    [Header("Player")]
    public GameObject PlayerObjSelect;

    public SlootData CurrentSlotSelect;
    public int IdSlotCurrent;
    public bool isOpenInventory = false;
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

    private void UpdateUi()
    {

    }

}
