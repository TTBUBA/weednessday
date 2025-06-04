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

    public PlacementManager PlacementManager;
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
        Butt_OpenPanelUtility.action.performed += OnOpenPanelUtitli;
        Butt_ClosePanelUtility.action.performed += OnClosePanelUtitli;
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
        Butt_OpenPanelUtility.action.performed -= OnOpenPanelUtitli;
        Butt_ClosePanelUtility.action.performed -= OnClosePanelUtitli;
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.OpenInventory();
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.CloseInventory();
    }

    public void PlaceObject(InputAction.CallbackContext context)
    {
        PlacementManager.PlaceObject();
    }
    private void RemoveObjectPlace(InputAction.CallbackContext context)
    {
        PlacementManager.RemoveObjectPlace();
    }
    public void OnOpenPanelUtitli(InputAction.CallbackContext context)
    {
        PlacementManager.ActivePanel();
    }

    public void OnClosePanelUtitli(InputAction.CallbackContext context)
    {
        PlacementManager.DeactivePanel();
    }
}

