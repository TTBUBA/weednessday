using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlantSystem : MonoBehaviour
{
    //use this struct to store weed data for use in the dictionary
    public struct WeedData
    {
        public GameObject WeedObject { get; set; }
        public TerrainState StateTerrain { get; set; }
    }

    [SerializeField] private bool ActivePlant;
    [SerializeField] private bool ActiveShovel;
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Camera CamPlayer;

    [Header("Terrain-Tile")]
    [SerializeField] private Tile DryTile;
    [SerializeField] private Tile WetTile;

    [SerializeField] private GameObject WeedPlant;
    [SerializeField] private GameObject SelectBox;
    [SerializeField] private GameObject PointPlat;

    [SerializeField] private Plant plant; 
    private Dictionary<Vector3Int, WeedData> CellOccupate = new Dictionary<Vector3Int, WeedData>();
    [SerializeField] private Vector3 MousePos;
    [SerializeField] private Vector3Int cellPos;


    //===Input System===//
    [SerializeField] private InputActionReference ButtPlant;
    [SerializeField] private InputActionReference buttCollect;

    public enum TerrainState
    {
        None,
        Dry,
        wet,
        planted
    }

    private void OnEnable()
    {
        ButtPlant.action.Enable();
        ButtPlant.action.performed += Plant;
        buttCollect.action.Enable();
        buttCollect.action.performed += CollectPlant;
    }
    private void OnDisable()
    {
        ButtPlant.action.Disable();
        ButtPlant.action.performed -= Plant;
        buttCollect.action.Enable();
        buttCollect.action.performed += CollectPlant;
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
    void Update()
    {
        MousePos = Mouse.current.position.ReadValue();
        MousePos = CamPlayer.ScreenToWorldPoint(MousePos);
        cellPos = tilemapGround.WorldToCell(MousePos);
        SelectBox.transform.position = tilemapGround.GetCellCenterWorld(cellPos);
    }

    //checks the terrain state of the cell at the given position
    private TerrainState GetTerrainState(Vector3Int cell)
    {
        return CellOccupate.TryGetValue(cell, out WeedData data) ? data.StateTerrain : TerrainState.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plant = collision.GetComponent<Plant>();
    }
    private void Plant(InputAction.CallbackContext context)
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
            /*
           GameObject plant = Instantiate(WeedPlant, tilemapGround.GetCellCenterWorld(cellPos), Quaternion.identity);
           plant.transform.parent = PointPlat.transform; // set parent to PointPlat
           GameObject weed = new GameObject("Weed");
           weed.transform.position = tilemap.GetCellCenterWorld(cellPos);
           SpriteRenderer spriteRenderer = weed.AddComponent<SpriteRenderer>();
           spriteRenderer.sortingOrder = 2;
           spriteRenderer.transform.parent = PointPlat.transform;// set parent to PointPlat
           spriteRenderer.sprite = Weed;
           */
            plant.GetComponent<Plant>().GrowthPlant(); // Start the growth process of the plant
            CellOccupate[cellPos] = new WeedData {WeedObject = plant.gameObject, StateTerrain = TerrainState.planted };//set the terrain state to planted
            if (CellOccupate.ContainsKey(cellPos)) { return; } // Check if the cell is already occupied
        }
    }

    private void CollectPlant(InputAction.CallbackContext context)
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

    private void WetTerrain()
    {
        if (InventoryManager.Instance.CurrentSlotSelect.NameTools == "WateringCan" && GetTerrainState(cellPos) == TerrainState.Dry)
        {
            tilemapGround.SetTile(cellPos, WetTile);
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.wet
            };

        }
    }
}
