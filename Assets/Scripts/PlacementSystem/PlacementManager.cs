using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    public barrelSystem barrelsystem;
    [SerializeField] private GameObject PanelUtilty;
    [SerializeField] private Camera CamPlayer;
    [SerializeField] private Tilemap Tm_ObjectPlacement;
    [SerializeField] private GameObject ParentObjspawn;


    public bool DrawModeActive = false;
    public bool IsPlacementActive = false;

    public Dictionary<Vector3Int, PlaceableObjectData> CellOccupateObj = new Dictionary<Vector3Int, PlaceableObjectData>();
   
    [Header("Managers")]
    public MouseManager MouseManager;
    public PlayerManager PlayerManager;
    public PlantManager plantManager;
    public WateringCan WateringCan;
    public InventoryManager InventoryManager;
    private void Awake()
    {
        Instance = this;
    }

    private Vector3Int cellpos => MouseManager.GetMousePosition();

    public void SelectObject()
    {
        if (PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject)
        {
            IsPlacementActive = true;
            PanelUtilty.gameObject.SetActive(false);
        }
    }

    // place the object at the current cell position
    public void PlaceObject()
    {
        if (CellOccupateObj.ContainsKey(cellpos)) { return; } // Check if the cell is already occupied
        if (plantManager.CellOccupate.ContainsKey(cellpos)) { return; } // Check if the cell is already occupied by a plant

        if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject.Type == PlaceableObjectType.Decoration)
        {
            Tm_ObjectPlacement.SetTile(cellpos, CurrentplaceableObject.Object);
            CellOccupateObj[cellpos] = CurrentplaceableObject;
            PlayerManager.CurrentMoney -= CurrentplaceableObject.Cost;
        }

        if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject.Type == PlaceableObjectType.Utility)
        {
            GameObject Obj = Instantiate(CurrentplaceableObject.UtilityPrefab,cellpos, Quaternion.identity);
            Obj.transform.parent = ParentObjspawn.transform; 
            CellOccupateObj[cellpos] = CurrentplaceableObject;
            PlayerManager.CurrentMoney -= CurrentplaceableObject.Cost;


            var barrel = Obj.GetComponent<barrelSystem>();
            var WellSystem = Obj.GetComponent<WellSystem>();
            if (barrel)
            {
                barrel.SetCamera(CamPlayer);
            }
            if (WellSystem)
            {
                WellSystem.SetData(CamPlayer, InventoryManager.CurrentSlotSelect, WateringCan);
            }
        }
    }

    public void RemoveObjectPlace()
    {
        if (DrawModeActive)
        {
            Tm_ObjectPlacement.SetTile(cellpos, null);
            CellOccupateObj.Remove(cellpos);
        }
    }
}
