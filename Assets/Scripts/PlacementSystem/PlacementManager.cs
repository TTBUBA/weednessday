using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    [SerializeField] private Camera CamPlayer;
    [SerializeField] private Tilemap Tm_ObjectPlacement;


    public bool DrawModeActive = false;

    [Header("Managers")]
    public MouseManager MouseManager;


    private void Awake()
    {
        Instance = this;
    }

    private Vector3Int cellpos => MouseManager.GetMousePosition();
    public void PlaceObject()
    {
        if (DrawModeActive)
        {
            Tm_ObjectPlacement.SetTile(cellpos, CurrentplaceableObject.Object);
        }

    }

    public void RemoveObjectPlace()
    {
        if (DrawModeActive)
        {
            Tm_ObjectPlacement.SetTile(cellpos, null);
        }
    }
}
