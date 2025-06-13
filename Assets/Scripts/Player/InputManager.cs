using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [Header("Input-Inventory")]
    [SerializeField] private InputActionReference Butt_OpenInventory;

    [Header("Input-PlacementObject")]
    [SerializeField] private InputActionReference Butt_PlaceObject;
    [SerializeField] private InputActionReference Butt_RemoveObjectPlace;
    [SerializeField] private InputActionReference Butt_OpenPanelUtility;
    [SerializeField] private InputActionReference Butt_ClosePanelUtility;

    [Header("Input-Player")]
    [SerializeField] private InputActionReference Butt_PlantWeed;
    [SerializeField] private InputActionReference Butt_CollectWeed;

    public PlacementManager PlacementManager;
    public UiManager Uimanager;
    public PlantManager PlantManager; 

    private void OnEnable()
    {
        Butt_OpenInventory.action.Enable();
        Butt_OpenInventory.action.performed += OnOpenInventory;
        Butt_OpenInventory.action.canceled += OnCloseInventory;

        Butt_PlaceObject.action.Enable();
        Butt_RemoveObjectPlace.action.Enable();
        Butt_PlaceObject.action.performed += PlaceObject;
        Butt_RemoveObjectPlace.action.performed += RemoveObjectPlace;


        Butt_OpenPanelUtility.action.Enable();
        Butt_ClosePanelUtility.action.Enable();
        Butt_OpenPanelUtility.action.performed += OnOpenPanelUtilty;
        Butt_ClosePanelUtility.action.performed += OnCloseOpenPanelUtilty;

        Butt_PlantWeed.action.Enable();
        Butt_PlantWeed.action.performed += PlantWeed;
        Butt_CollectWeed.action.Enable();
        Butt_CollectWeed.action.performed += CollectWeed;

    }

    private void OnDisable()
    {
        Butt_OpenInventory.action.Disable();
        Butt_OpenInventory.action.performed -= OnOpenInventory;
        Butt_OpenInventory.action.canceled -= OnCloseInventory;

        Butt_PlaceObject.action.Disable();
        Butt_RemoveObjectPlace.action.Disable();
        Butt_PlaceObject.action.performed -= PlaceObject;
        Butt_RemoveObjectPlace.action.performed -= RemoveObjectPlace;

        Butt_OpenPanelUtility.action.Disable();
        Butt_ClosePanelUtility.action.Disable();
        Butt_OpenPanelUtility.action.performed -= OnOpenPanelUtilty;
        Butt_ClosePanelUtility.action.performed -= OnCloseOpenPanelUtilty;

        Butt_PlantWeed.action.Enable();
        Butt_PlantWeed.action.performed -= PlantWeed;
        Butt_CollectWeed.action.Enable();
        Butt_CollectWeed.action.performed -= CollectWeed;
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        Uimanager.OpenPanelInventory();
    }
    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        Uimanager.ClosePanelInventory();
    }
    public void PlaceObject(InputAction.CallbackContext context)
    {
        if (!PlacementManager.IsPlacementActive) return;
        PlacementManager.PlaceObject();
    }
    private void RemoveObjectPlace(InputAction.CallbackContext context)
    {
        if (!PlacementManager.IsPlacementActive) return;
        PlacementManager.RemoveObjectPlace();
    }
    public void OnOpenPanelUtilty(InputAction.CallbackContext context)
    {
        Uimanager.OpenPanelUtilty();
    }
    public void OnCloseOpenPanelUtilty(InputAction.CallbackContext context)
    {
        Uimanager.ClosePanelUtilty();
    }
    public void PlantWeed(InputAction.CallbackContext context)
    {
        PlantManager.Plant();
    }
    public void CollectWeed(InputAction.CallbackContext context)
    {
        PlantManager.CollectPlant();
    }
}

