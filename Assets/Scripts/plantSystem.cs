using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlantSystem : MonoBehaviour
{
    [SerializeField] private bool ActivePlant;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Sprite Weed;


    private Dictionary<Vector3Int, GameObject> weedObjects = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] private Vector3 MousePos;
    [SerializeField] private Vector3Int cellPos;

    

    //===Input System===//
    [SerializeField] private InputActionReference ButtPlant;

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
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        cellPos = tilemap.WorldToCell(MousePos);
    }

    private void Plant(InputAction.CallbackContext context)
    {
        if (!ActivePlant) return;
        if (weedObjects.ContainsKey(cellPos))
        {
            Debug.Log("Weed already planted here.");
            return;
        }
        GameObject weed = new GameObject("Weed");
        weed.transform.position = tilemap.GetCellCenterWorld(cellPos);
        SpriteRenderer spriteRenderer = weed.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Weed;
        weedObjects[cellPos] = weed;
    }
}
