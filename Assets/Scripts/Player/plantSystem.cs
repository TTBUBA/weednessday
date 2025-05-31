using System;
using System.Collections.Generic;
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
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera CamPlayer;

    [Header("Terrain-Tile")]
    [SerializeField] private Tile DryTile;
    [SerializeField] private Tile WetTile;

    [SerializeField] private GameObject WeedPlant;
    [SerializeField] private GameObject SelectBox;
    [SerializeField] private GameObject PointPlat;

    private Dictionary<Vector3Int, WeedData> CellOccupate = new Dictionary<Vector3Int, WeedData>();
    [SerializeField] private Vector3 MousePos;
    [SerializeField] private Vector3Int cellPos;
    private TerrainState currentTerrainState = TerrainState.None;

    //===Input System===//
    [SerializeField] private InputActionReference ButtPlant;

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
    }
    private void OnDisable()
    {
        ButtPlant.action.Disable();
        ButtPlant.action.performed -= Plant;
    }
    void Update()
    {
        MousePos = Mouse.current.position.ReadValue();
        MousePos = CamPlayer.ScreenToWorldPoint(MousePos);
        cellPos = tilemap.WorldToCell(MousePos);
        SelectBox.transform.position = tilemap.GetCellCenterWorld(cellPos);
    }

    //checks the terrain state of the cell at the given position
    private TerrainState GetTerrainState(Vector3Int cell)
    {
        return CellOccupate.TryGetValue(cell, out WeedData data) ? data.StateTerrain : TerrainState.None;
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
            GameObject plant = Instantiate(WeedPlant, tilemap.GetCellCenterWorld(cellPos), Quaternion.identity);
            plant.GetComponent<Plant>().GrowthPlant(); // Start the growth process of the plant
            plant.transform.parent = PointPlat.transform; // set parent to PointPlat
            /*
            GameObject weed = new GameObject("Weed");
            weed.transform.position = tilemap.GetCellCenterWorld(cellPos);
            SpriteRenderer spriteRenderer = weed.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 2;
            spriteRenderer.transform.parent = PointPlat.transform;// set parent to PointPlat
            spriteRenderer.sprite = Weed;
            */
            CellOccupate[cellPos] = new WeedData {WeedObject = plant, StateTerrain = TerrainState.planted };//set the terrain state to planted
            if (CellOccupate.ContainsKey(cellPos)) { return; } // Check if the cell is already occupied
        }
    }

    private void HoeTerrain()
    {
        if (InventoryManager.Instance.CurrentSlotSelect.NameTools == "Shovel" && GetTerrainState(cellPos) == TerrainState.None)
        {
            tilemap.SetTile(cellPos, DryTile);
            currentTerrainState = TerrainState.Dry;
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
            tilemap.SetTile(cellPos, WetTile);
            currentTerrainState = TerrainState.wet;
            CellOccupate[cellPos] = new WeedData
            {
                StateTerrain = TerrainState.wet
            };

        }
    }
}
