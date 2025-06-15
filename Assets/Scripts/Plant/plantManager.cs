using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    //use this struct to store weed data for use in the dictionary
    public struct WeedData
    {
        public GameObject WeedObject { get; set; }
        public TerrainState StateTerrain { get; set; }
    }

    [SerializeField] private bool ActivePlant;
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Camera CamPlayer;

    [Header("Terrain-Tile")]
    [SerializeField] private Tile DryTile;
    [SerializeField] private Tile WetTile;

    [SerializeField] private GameObject WeedPlant;
    [SerializeField] private GameObject SelectBox;
    [SerializeField] private GameObject PointPlat;

    [SerializeField] private Plant plant; 
    public Dictionary<Vector3Int, WeedData> CellOccupate = new Dictionary<Vector3Int, WeedData>();

    public MouseManager MouseManager;
    public PlacementManager placementManager;
    public WateringCan WateringCan;
    public enum TerrainState
    {
        None,
        Dry,
        wet,
        planted
    }

    private void Awake()
    {
        BoundsInt bounds = tilemapGround.cellBounds;    
        for(int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemapGround.GetTile(pos);
                if (tile != null)
                {
                    //CellOccupate[cell] = new WeedData { StateTerrain = TerrainState.None }; 
                    GameObject plant = Instantiate(WeedPlant, tilemapGround.GetCellCenterWorld(pos), Quaternion.identity);
                    plant.transform.parent = PointPlat.transform; 
                }
            }
        }
    }

    //checks the terrain state of the cell at the given position
    private TerrainState GetTerrainState(Vector3Int cell)
    {
        return CellOccupate.TryGetValue(cell, out WeedData data) ? data.StateTerrain : TerrainState.None;
    }

    private Vector3Int cellPos => MouseManager.GetMousePosition();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plant = collision.GetComponent<Plant>();
    }

    public void Plant()
    {
        if(!PlacementManager.Instance.DrawModeActive && !placementManager.CellOccupateObj.ContainsKey(cellPos))
        {
            var Inventory = InventoryManager.Instance.CurrentSlotSelect;

            if (!ActivePlant) return;

            if (Inventory.NameTools == "Shovel")
            {
                HoeTerrain();
                return;
            }

            if (Inventory.NameTools == "WateringCan")
            {
                WetTerrain();
                return;
            }

            // Check if the terrain is dry before planting
            if (GetTerrainState(cellPos) == TerrainState.wet && Inventory.NameTools == "Weed")
            {
                plant.GetComponent<Plant>().GrowthPlant(); // Start the growth process of the plant
                CellOccupate[cellPos] = new WeedData { WeedObject = plant.gameObject, StateTerrain = TerrainState.planted };//set the terrain state to planted
                if (CellOccupate.ContainsKey(cellPos)) { return; } // Check if the cell is already occupied
            }
        }
    }

    public void CollectPlant()
    {
        var Inventory = InventoryManager.Instance.CurrentSlotSelect;

        if (GetTerrainState(cellPos) == TerrainState.planted && Inventory.NameTools == "Basket" && plant.FinishGrowth)
        {
            plant.GetComponent<Plant>().ResetPlant(); 
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.None
            };
            CellOccupate.Remove(cellPos); 
            tilemapGround.SetTile(cellPos, WetTile);
        }
    }

    //changing the state to dry if the player has a shovel and the terrain
    private void HoeTerrain()
    {
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
    private void WetTerrain()
    {
        if (InventoryManager.Instance.CurrentSlotSelect.NameTools == "WateringCan" && GetTerrainState(cellPos) == TerrainState.Dry)
        {
            if (WateringCan.waterAmount <= 0f) { return; } // Check if the watering can has water
            int RandomValue = Random.Range(1, 10);
            WateringCan.Use(RandomValue);
            tilemapGround.SetTile(cellPos, WetTile);
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.wet
            };

        }
    }
}
