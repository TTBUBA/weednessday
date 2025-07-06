using System;
using UnityEngine;
using UnityEngine.InputSystem;

//This script occupies the role of managing player inputs 
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
    [SerializeField] private InputActionReference Butt_HoeTerrain;
    [SerializeField] private InputActionReference Butt_WetTerrain;
    [SerializeField] private InputActionReference Butt_CollectWeed;
    [SerializeField] private InputActionReference Butt_openPhone;
    [SerializeField] private InputActionReference Butt_closePhone;
    [SerializeField] private InputActionReference Butt_UseDrog;


    [Header("Manager References")]
    [SerializeField] public PlacementManager PlacementManager;
    [SerializeField] public UiManager Uimanager;
    [SerializeField] public PlantManager PlantManager;
    [SerializeField] public InventoryManager InventoryManager;
    [SerializeField] public PhoneManager PhoneManager;
    [SerializeField] public PlayerManager playerManager;

    private void OnEnable()
    {
        // Inventory inputs
        if (Butt_OpenInventory != null)
        {
            Butt_OpenInventory.action.Enable();
            Butt_OpenInventory.action.performed += OnOpenInventory;
            Butt_OpenInventory.action.canceled += OnCloseInventory;
        }

        // Placement inputs
        if (Butt_PlaceObject != null)
        {
            Butt_PlaceObject.action.Enable();
            Butt_PlaceObject.action.performed += PlaceObject;
        }

        if (Butt_RemoveObjectPlace != null)
        {
            Butt_RemoveObjectPlace.action.Enable();
            Butt_RemoveObjectPlace.action.performed += RemoveObjectPlace;
        }

        // Panel utility inputs
        if (Butt_OpenPanelUtility != null)
        {
            Butt_OpenPanelUtility.action.Enable();
            Butt_OpenPanelUtility.action.performed += OnOpenPanelUtilty;
        }

        if (Butt_ClosePanelUtility != null)
        {
            Butt_ClosePanelUtility.action.Enable();
            Butt_ClosePanelUtility.action.performed += OnCloseOpenPanelUtilty;
        }

        // Plant inputs
        if (Butt_PlantWeed != null)
        {
            Butt_PlantWeed.action.Enable();
            Butt_PlantWeed.action.performed += PlantWeed;
        }

        if (Butt_CollectWeed != null)
        {
            Butt_CollectWeed.action.Enable();
            Butt_CollectWeed.action.performed += CollectWeed;
        }

        // Tool inputs
        if (Butt_HoeTerrain != null)
        {
            Butt_HoeTerrain.action.Enable();
            Butt_HoeTerrain.action.performed += HoeTerrain;
        }

        if (Butt_WetTerrain != null)
        {
            Butt_WetTerrain.action.Enable();
            Butt_WetTerrain.action.performed += WetTerrain;
        }

        // Phone inputs
        if (Butt_openPhone != null)
        {
            Butt_openPhone.action.Enable();
            Butt_openPhone.action.performed += OpenPhone;
        }

        if (Butt_closePhone != null)
        {
            Butt_closePhone.action.Enable();
            Butt_closePhone.action.performed += ClosePhone;
        }

        //Player input
        if (Butt_UseDrog != null)
        {
            Butt_UseDrog.action.Enable();
            Butt_UseDrog.action.performed += Butt_SmokeWeed;
        }
    }

    private void OnDisable()
    {
        // Inventory inputs
        if (Butt_OpenInventory != null)
        {
            Butt_OpenInventory.action.Disable();
            Butt_OpenInventory.action.performed -= OnOpenInventory;
            Butt_OpenInventory.action.canceled -= OnCloseInventory;
        }

        // Placement inputs
        if (Butt_PlaceObject != null)
        {
            Butt_PlaceObject.action.Disable();
            Butt_PlaceObject.action.performed -= PlaceObject;
        }

        if (Butt_RemoveObjectPlace != null)
        {
            Butt_RemoveObjectPlace.action.Disable();
            Butt_RemoveObjectPlace.action.performed -= RemoveObjectPlace;
        }

        // Panel utility inputs
        if (Butt_OpenPanelUtility != null)
        {
            Butt_OpenPanelUtility.action.Disable();
            Butt_OpenPanelUtility.action.performed -= OnOpenPanelUtilty;
        }

        if (Butt_ClosePanelUtility != null)
        {
            Butt_ClosePanelUtility.action.Disable();
            Butt_ClosePanelUtility.action.performed -= OnCloseOpenPanelUtilty;
        }

        // Plant inputs
        if (Butt_PlantWeed != null)
        {
            Butt_PlantWeed.action.Disable(); // Era Enable() - BUG CORRETTO
            Butt_PlantWeed.action.performed -= PlantWeed;
        }

        if (Butt_CollectWeed != null)
        {
            Butt_CollectWeed.action.Disable(); // Era Enable() - BUG CORRETTO
            Butt_CollectWeed.action.performed -= CollectWeed;
        }

        // Tool inputs
        if (Butt_HoeTerrain != null)
        {
            Butt_HoeTerrain.action.Disable();
            Butt_HoeTerrain.action.performed -= HoeTerrain;
        }

        if (Butt_WetTerrain != null)
        {
            Butt_WetTerrain.action.Disable();
            Butt_WetTerrain.action.performed -= WetTerrain;
        }

        // Phone inputs
        if (Butt_openPhone != null)
        {
            Butt_openPhone.action.Disable();
            Butt_openPhone.action.performed -= OpenPhone;
        }

        if (Butt_closePhone != null)
        {
            Butt_closePhone.action.Disable();
            Butt_closePhone.action.performed -= ClosePhone;
        }

        //Player input
        if (Butt_UseDrog != null)
        {
            Butt_UseDrog.action.Disable();
            Butt_UseDrog.action.performed -= Butt_SmokeWeed;
        }
    }

    // Inventory methods
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (Uimanager != null)
        {
            Uimanager.OpenPanelInventory();
        }
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (Uimanager != null)
        {
            Uimanager.ClosePanelInventory();
        }
    }

    // Placement methods
    public void PlaceObject(InputAction.CallbackContext context)
    {
        if (PlacementManager == null || !PlacementManager.IsPlacementActive) return;
        PlacementManager.PlaceObject();
    }

    private void RemoveObjectPlace(InputAction.CallbackContext context)
    {
        if (PlacementManager == null || !PlacementManager.IsPlacementActive) return;
        PlacementManager.RemoveObjectPlace();
    }

    // Panel utility methods
    public void OnOpenPanelUtilty(InputAction.CallbackContext context)
    {
        if (Uimanager != null)
        {
            Uimanager.OpenPanelUtilty();
        }
    }

    public void OnCloseOpenPanelUtilty(InputAction.CallbackContext context)
    {
        if (Uimanager != null)
        {
            Uimanager.ClosePanelUtilty();
        }
    }

    // Plant methods
    public void PlantWeed(InputAction.CallbackContext context)
    {
        if (InventoryManager.CurrentSlotSelect == null) { return; }
        if (InventoryManager.CurrentSlotSelect.NameTools != "SeedWeed") { return; }

        PlantManager.Plant();
    }

    // Tool methods
    public void HoeTerrain(InputAction.CallbackContext context)
    {
        if (InventoryManager.CurrentSlotSelect == null || InventoryManager.CurrentSlotSelect.NameTools != "Shovel") { return; }
        PlantManager.HoeTerrain();
    }

    public void WetTerrain(InputAction.CallbackContext context)
    {
        if (InventoryManager.CurrentSlotSelect == null || InventoryManager.CurrentSlotSelect.NameTools != "WateringCan") { return; }

        PlantManager.WetTerrain();
    }

    public void CollectWeed(InputAction.CallbackContext context)
    {
        if ( PlantManager.plant == null || !PlantManager.plant.FinishGrowth) { return; }

        PlantManager.CollectPlant();
    }

    // Phone methods
    public void OpenPhone(InputAction.CallbackContext context)
    {
        if (PhoneManager != null)
        {
            PhoneManager.OpenPhone();
        }
    }

    public void ClosePhone(InputAction.CallbackContext context)
    {
        if (PhoneManager != null)
        {
            PhoneManager.ClosePhone();
        }
    }

    //Player Input
    private void Butt_SmokeWeed(InputAction.CallbackContext context)
    {
        playerManager.UseWeed();
    }
}