using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;
    //use this struct to store weed data for use in the dictionary
    public struct WeedData
    {
        public GameObject WeedObject { get; set; }
        public TerrainState StateTerrain { get; set; }
    }

    public bool ActivePlant;
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Camera CamPlayer;

    [Header("Terrain-Tile")]
    [SerializeField] private RuleTile DryTile;
    [SerializeField] private RuleTile WetTile;

    [SerializeField] private GameObject WeedPlant;
    [SerializeField] private GameObject SelectBox;
    [SerializeField] private GameObject PointPlat;
    [SerializeField] private GameObject Obj_DrawMap;
    [SerializeField] private Transform containerDrawMap;


    public int MultiplyTime;
    public Plant plant; 
    public Dictionary<Vector3Int, WeedData> CellOccupate = new Dictionary<Vector3Int, WeedData>();
    public List<Plant> PlantsCreate = new List<Plant>();

    public PlayerMovement PlayerMovement;
    public PlacementManager placementManager;
    public WateringCan WateringCan;
    public InventoryManager InventoryManager;

    public enum TerrainState
    {
        None,
        Dry,
        wet,
        planted,
        obstacle
    }

    private void Awake()
    {
        Instance = this; 

        BoundsInt bounds = tilemapGround.cellBounds;    
        for(int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemapGround.GetTile(pos);
                if (tile != null)
                {
                    GameObject plant = Instantiate(WeedPlant, tilemapGround.GetCellCenterWorld(pos), Quaternion.identity);
                    PlantsCreate.Add(plant.GetComponent<Plant>());
                    plant.transform.parent = PointPlat.transform;
                    CellOccupate[cellPos] = new WeedData { WeedObject = plant.gameObject, StateTerrain = TerrainState.None };

                }
            }
        }
    }

    private void Update()
    {
        foreach(var plant in PlantsCreate)
        {
            Vector3Int CurrentCellPos = tilemapGround.WorldToCell(plant.transform.position);

            if (GetTerrainState(CurrentCellPos) == TerrainState.wet && !plant.IsWet)
            {
               plant.time = plant.TimeBase / 2f;
               plant.IsWet = true; 
            }
            else if (GetTerrainState(CurrentCellPos) == TerrainState.Dry && plant.IsWet)
            {
                plant.time = plant.TimeBase;
                plant.IsWet = false;
            }
        }
    }

    //checks the terrain state of the cell at the given position
    public TerrainState GetTerrainState(Vector3Int cell)
    {
        return CellOccupate.TryGetValue(cell, out WeedData data) ? data.StateTerrain : TerrainState.None;
    }

    private Vector3Int cellPos => PlayerMovement.NextPosBox;

    public void Plant()
    {
        if(!PlacementManager.Instance.DrawModeActive && !placementManager.CellOccupateObj.ContainsKey(cellPos))
        {
            var Inventory = InventoryManager.Instance.CurrentSlotSelect;
            var slotmanager = InventoryManager.Instance.CurrentSlootManager;

            if (!ActivePlant) return;

            // Check if the terrain is dry before planting
            if (GetTerrainState(cellPos) == TerrainState.Dry && Inventory.NameTools == "SeedWeed" && slotmanager.CurrentStorage > 1)
            {
                InventoryManager.RemoveSeedWeed();
                float Time = Random.Range(20f, 40f);
                plant.GetComponent<Plant>().time = Time;
                plant.GetComponent<Plant>().GrowthPlant(); 
                CellOccupate[cellPos] = new WeedData { WeedObject = plant.gameObject, StateTerrain = TerrainState.planted };//set the terrain state to planted
                if (CellOccupate.ContainsKey(cellPos)) { return; } // Check if the cell is already occupied 
                Debug.Log(GetTerrainState(cellPos));

            }
            else if (GetTerrainState(cellPos) == TerrainState.wet && Inventory.NameTools == "SeedWeed" && slotmanager.CurrentStorage > 1)
            {
                InventoryManager.RemoveSeedWeed();
                float Time = Random.Range(5f, 10f);
                plant.GetComponent<Plant>().time = Time;
                plant.GetComponent<Plant>().GrowthPlant(); 
                CellOccupate[cellPos] = new WeedData { WeedObject = plant.gameObject, StateTerrain = TerrainState.planted };//set the terrain state to planted
                if (CellOccupate.ContainsKey(cellPos)) { return; } // Check if the cell is already occupied 
            }
        }
    }
    public void CollectPlant()
    {
        var Inventory = InventoryManager.Instance.CurrentSlotSelect;

        if (Inventory.NameTools == "Basket" && plant.FinishGrowth)
        {
            int RandomValue = Random.Range(1, 3);
            InventoryManager.Instance.AddItem(InventoryManager.Instance.weed, RandomValue);
            plant.GetComponent<Plant>().ResetPlant();
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.Dry
            };
            tilemapGround.SetTile(cellPos, DryTile);
        }
    }

    //changing the state to dry if the player has a shovel and the terrain
    public void HoeTerrain()
    {
        if (!ActivePlant) { return; }
        if (!plant) { return; } 
        if (InventoryManager.Instance.CurrentSlotSelect.NameTools == "Shovel" && GetTerrainState(cellPos) == TerrainState.None)
        {
            tilemapGround.SetTile(cellPos, DryTile);
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.Dry
            };
        }
    }

    //changing the state to wet if the player has a watering can and the terrain is dry
    public void WetTerrain()
    {
        if (!ActivePlant) { return; }
        if (InventoryManager.Instance.CurrentSlotSelect.NameTools == "WateringCan" && GetTerrainState(cellPos) == TerrainState.Dry || GetTerrainState(cellPos) == TerrainState.planted)
        {
            if (WateringCan.waterAmount <= 0f) { return; } // Check if the watering can has water
            int RandomValue = Random.Range(1, 10);
            WateringCan.Use(RandomValue);
            WateringCan.Text_CurrentWater.text = WateringCan.waterAmount.ToString() + "%";
            tilemapGround.SetTile(cellPos, WetTile);
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.wet
            };
            StartCoroutine(changeStateTerrain(cellPos));
        }
    }

    IEnumerator changeStateTerrain(Vector3Int cell)
    {
        float RandomValue = Random.Range(40, 200);
        yield return new WaitForSeconds(RandomValue);
        if (CellOccupate.TryGetValue(cell, out WeedData data) && data.StateTerrain == TerrainState.wet)
        {
            tilemapGround.SetTile(cell, DryTile);
            CellOccupate[cell] = new WeedData
            {
                StateTerrain = TerrainState.Dry
            };
        }
    }

}
