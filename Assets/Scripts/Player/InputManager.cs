using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionReference Butt_OpenInventory;


    private void OnEnable()
    {
        Butt_OpenInventory.action.Enable();
        Butt_OpenInventory.action.performed += OnOpenInventory;
        Butt_OpenInventory.action.canceled += OnCloseInventory;
    }

    private void OnDisable()
    {
        Butt_OpenInventory.action.Disable();
        Butt_OpenInventory.action.performed -= OnOpenInventory;
        Butt_OpenInventory.action.canceled -= OnCloseInventory;
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.OpenInventory();
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.CloseInventory();
    }
}

