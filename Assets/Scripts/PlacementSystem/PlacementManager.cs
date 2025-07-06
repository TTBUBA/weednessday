using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    public ChestSystem chestSystem;
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
    public Cicle_DayNight CicleDayNight;
    private void Awake()
    {
        Instance = this;
    }

    //Get the current cell position on the mousePosition
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

        //position of the object only is tile 
        if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject.Type == PlaceableObjectType.Prefabs)
        {
            Tm_ObjectPlacement.SetTile(cellpos, CurrentplaceableObject.Object);

            //this create a cube araund the object create
            for (int x = -CurrentplaceableObject.SpaceOccupiedX; x <= CurrentplaceableObject.SpaceOccupiedX; x++)
            {
                for (int y = -CurrentplaceableObject.SpaceOccupiedX; y <= CurrentplaceableObject.SpaceOccupiedY; y++)
                {
                    Vector3Int positionOffset = new Vector3Int(x, y, 0);
                    CellOccupateObj[cellpos + positionOffset] = CurrentplaceableObject;
                }
            }
            PlayerManager.CurrentMoney -= CurrentplaceableObject.Cost;
        }

        //position of the object only is prefabs 
        if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject.Type == PlaceableObjectType.Tile)
        {
            GameObject Obj = Instantiate(CurrentplaceableObject.UtilityPrefab,cellpos, Quaternion.identity);
            Obj.transform.parent = ParentObjspawn.transform; 
            PlayerManager.CurrentMoney -= CurrentplaceableObject.Cost;

            //this create a cube around the object create
            for (int x = -CurrentplaceableObject.SpaceOccupiedX; x <= CurrentplaceableObject.SpaceOccupiedX; x++)
            {
                for (int y = -CurrentplaceableObject.SpaceOccupiedY; y <= CurrentplaceableObject.SpaceOccupiedY; y++)
                {
                    Vector3Int positionOffset = new Vector3Int(x, y, 0);
                    CellOccupateObj[cellpos + positionOffset] = CurrentplaceableObject;
                }
            }


            //When the object is create, set the data for the specific components in the object
            var Chest = Obj.GetComponent<ChestSystem>();
            var WellSystem = Obj.GetComponent<WellSystem>();
            var PoleLight = Obj.GetComponent<PoleLight>();
            if (Chest)
            {
                Chest.SetData(CamPlayer, InventoryManager);
            }
            if (WellSystem)
            {
                WellSystem.SetData(CamPlayer, InventoryManager, WateringCan);
            }
            if (PoleLight)
            {
                PoleLight.SetData(CicleDayNight);
            }

        }
    }

    // Remove the object from the tilemap and the CellOccupateObj dictionary
    public void RemoveObjectPlace()
    {
        if (DrawModeActive)
        {
            Tm_ObjectPlacement.SetTile(cellpos, null);

            // Remove the object from the CellOccupateObj dictionary
            for (int x = -CurrentplaceableObject.SpaceOccupiedX; x <= CurrentplaceableObject.SpaceOccupiedX; x++)
            {
                for (int y = -CurrentplaceableObject.SpaceOccupiedY; y <= CurrentplaceableObject.SpaceOccupiedY; y++)
                {
                    Vector3Int positionOffset = new Vector3Int(x, y, 0);
                    CellOccupateObj.Remove(cellpos + positionOffset);
                }
            }
        }
    }
}
