using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    [SerializeField] private Camera CamPlayer;

    [Header("Ui")]
    [SerializeField] private Tilemap Tm_ObjectPlacement;
    [SerializeField] private Tilemap DrawTile;


    private Vector3 MousePos;
    private Vector3Int cellPos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        MousePos = Mouse.current.position.ReadValue();
        MousePos = CamPlayer.ScreenToWorldPoint(MousePos);
        cellPos = Tm_ObjectPlacement.WorldToCell(MousePos);
        //Debug.Log("Cell Position: " + cellPos);
    }

    public void PlaceObject()
    {
        Tm_ObjectPlacement.SetTile(cellPos, CurrentplaceableObject.Object);
    }

    public void ActiveDrawTile()
    {
        DrawTile.gameObject.SetActive(true);
    }

}
