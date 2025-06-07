using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    [SerializeField] private GameObject PanelUtilty;
    [SerializeField] private Camera CamPlayer;
    [SerializeField] private Tilemap Tm_ObjectPlacement;

    public bool DrawModeActive = false;
    public bool IsPlacementActive = false;

    public Dictionary<Vector3Int, PlaceableObjectData> CellOccupateObj = new Dictionary<Vector3Int, PlaceableObjectData>();
   
    [Header("Managers")]
    public MouseManager MouseManager;
    public PlayerManager PlayerManager;
    public PlantManager plantManager;
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
    public void PlaceObject()
    {
        if (CellOccupateObj.ContainsKey(cellpos)) { return; } // Check if the cell is already occupied
        if (plantManager.CellOccupate.ContainsKey(cellpos)) { return; } // Check if the cell is already occupied by a plant

        if (DrawModeActive && IsPlacementActive && PlayerManager.CurrentMoney >= CurrentplaceableObject.Cost)
        {
            Tm_ObjectPlacement.SetTile(cellpos, CurrentplaceableObject.Object);
            CellOccupateObj[cellpos] = CurrentplaceableObject;
            PlayerManager.CurrentMoney -= CurrentplaceableObject.Cost;
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
