using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PlantManager;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    public ChestSystem chestSystem;
    public GameObject LastObjSpawn;
    [SerializeField] private GameObject PanelUtilty;
    [SerializeField] private Camera CamPlayer;
    [SerializeField] private Tilemap Tm_ObjectPlacement;
    [SerializeField] private GameObject ParentObjspawn;
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private GameObject Obj_DrawMap;
    [SerializeField] private Transform containerDrawMap;

    public bool DrawModeActive = false;
    public bool IsPlacementActive = false;

    public Dictionary<Vector3Int, PlaceableObjectData> CellOccupateObj = new Dictionary<Vector3Int, PlaceableObjectData>();

    [Header("Managers")]
    public MouseManager MouseManager;
    public PlayerManager PlayerManager;
    public PlantManager plantManager;
    public WateringCan WateringCan;
    public InventoryManager InventoryManager;
    public cycleDayNight cycleDayNight;
    public SmsManager SmsManager;

    private void Awake()
    {
        Instance = this;

        BoundsInt bounds = tilemapGround.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemapGround.GetTile(pos);
                if(tile != null)
                {
                    GameObject obj = Instantiate(Obj_DrawMap, tilemapGround.GetCellCenterWorld(pos), Quaternion.identity);
                    obj.transform.SetParent(containerDrawMap);
                }
            }
        }
    }

    private void Update()
    {
        BeforePlaceObj();
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

    //Before placing the object, set the position and transparency of the last spawned object
    public void BeforePlaceObj()
    {
        if(LastObjSpawn != null)
        {
            LastObjSpawn.transform.position = MouseManager.GetMousePosition();
            SpriteRenderer spriteRenderer = LastObjSpawn.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); 
            if(plantManager.GetTerrainState(cellpos) == PlantManager.TerrainState.obstacle || CellOccupateObj.ContainsKey(cellpos))
            {
                spriteRenderer.color = new Color(1f, 0, 0, 0.5f);
            }
            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Reset to semi-transparent white
            }

        }

    }


    // place the object at the current cell position
    public void PlaceObject()
    {
        if (CellOccupateObj.ContainsKey(cellpos)) { return; } // Check if the cell is already occupied

        if (plantManager.GetTerrainState(cellpos) == PlantManager.TerrainState.None)
        {
            //position of the object only is tile 
            if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject.Type == PlaceableObjectType.Tile)
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
            if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost && CurrentplaceableObject.Type == PlaceableObjectType.Prefabs)
            {
                GameObject Obj = Instantiate(CurrentplaceableObject.UtilityPrefab, cellpos, Quaternion.identity);
                Obj.transform.parent = ParentObjspawn.transform;
                PlayerManager.CurrentMoney -= CurrentplaceableObject.Cost;
                CurrentplaceableObject.IsPlaceable = true;


                if (CurrentplaceableObject.IsPlaceable)
                {
                    //send the sms with the description of the object created
                    SmsManager.CreateNewSms(CurrentplaceableObject.UtilityDescription);
                }


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
                var TrashCompactor = Obj.GetComponent<TrashCompactor>();
                var Desiccator = Obj.GetComponent<Desiccator>();
                var packer = Obj.GetComponent<PackerSystem>();
                var packingStationWeed = Obj.GetComponent<PackingWeedSystem>();
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
                    PoleLight.SetData(cycleDayNight);
                }
                if (TrashCompactor)
                {
                    TrashCompactor.SetData(CamPlayer, InventoryManager);
                }
                if (Desiccator)
                {
                    Desiccator.SetData(CamPlayer, InventoryManager);
                }
                if (packer)
                {
                    packer.SetData(CamPlayer, InventoryManager);
                }
                if (packingStationWeed)
                {
                    packingStationWeed.SetData(CamPlayer, InventoryManager);
                }
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
