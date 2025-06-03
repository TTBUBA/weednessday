using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [Header("Input-Inventory")]
    [SerializeField] private InputActionReference Butt_OpenInventory;

    [Header("Input-PlacementObject")]
    [SerializeField] private InputActionReference Butt_PlaceObject;

    private void OnEnable()
    {
        Butt_OpenInventory.action.Enable();
        Butt_OpenInventory.action.performed += OnOpenInventory;
        Butt_OpenInventory.action.canceled += OnCloseInventory;
        Butt_PlaceObject.action.Enable();
        Butt_PlaceObject.action.performed += PlaceObject;
    }

    private void OnDisable()
    {
        Butt_OpenInventory.action.Disable();
        Butt_OpenInventory.action.performed -= OnOpenInventory;
        Butt_OpenInventory.action.canceled -= OnCloseInventory;
        Butt_PlaceObject.action.Disable();
        Butt_PlaceObject.action.performed -= PlaceObject;
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
        PlacementManager.Instance.PlaceObject();
        //Debug.Log("Object Placed at: " + PlacementManager.Instance.CurrentplaceableObject.Object.name);
        Debug.Log("Click");
    }


}

