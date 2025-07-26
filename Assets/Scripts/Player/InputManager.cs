using UnityEngine;
using UnityEngine.InputSystem;

// This script handles player input and delegates logic to other managers.
public class InputManager : MonoBehaviour, InputSystem_Actions.IPlayerActions, InputSystem_Actions.IUIActions
{
    public InputSystem_Actions inputActions;

    [Header("Manager References")]
    public PlacementManager PlacementManager;
    public UiManager Uimanager;
    public PlantManager PlantManager;
    public InventoryManager InventoryManager;
    public PhoneManager PhoneManager;
    public PlayerManager playerManager;
    public PlayerMovement PlayerMovement;
    public MouseManager MouseManager;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        inputActions.UI.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.UI.Disable();
    }

    // Placement
    public void OnPlacementObjet(InputAction.CallbackContext context)
    {
        if (!context.performed || PlacementManager == null) return;
        PlacementManager.PlaceObject();
    }

    public void OnRemoveObjetPlacement(InputAction.CallbackContext context)
    {
        if (!context.performed || PlacementManager == null) return;
        PlacementManager.RemoveObjectPlace();
    }

    // Planting
    public void OnPlant(InputAction.CallbackContext context)
    {
        if (!context.performed || InventoryManager.CurrentSlotSelect?.NameTools != "SeedWeed") return;
        PlantManager.Plant();
    }

    public void OnHoeTerrain(InputAction.CallbackContext context)
    {
        if (!context.performed || InventoryManager.CurrentSlotSelect?.NameTools != "Shovel") return;
        PlantManager.HoeTerrain();
    }

    public void OnWetTerrain(InputAction.CallbackContext context)
    {
        if (!context.performed || InventoryManager.CurrentSlotSelect?.NameTools != "WateringCan") return;
        PlantManager.WetTerrain();
    }

    public void OnCollectPlant(InputAction.CallbackContext context)
    {
        if (!context.performed || PlantManager.plant == null || !PlantManager.plant.FinishGrowth) return;
        PlantManager.CollectPlant();
    }

    // Phone
    public void OnOpenPhone(InputAction.CallbackContext context)
    {
        if (!context.performed || PhoneManager == null) return;
        PhoneManager.OpenPhone();
    }

    public void OnClosePhone(InputAction.CallbackContext context)
    {
        if (!context.performed || PhoneManager == null) return;
        PhoneManager.ClosePhone();
    }

    // Player Movement
    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerManager == null) return;
        Vector2 input = context.ReadValue<Vector2>();
        PlayerMovement.moveInput = input;
    }
    public void OnMousePos(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Vector2 PosMouse = context.ReadValue<Vector2>();
        MouseManager.isUsingController = false;
        MouseManager.UpdateCursorsPos(PosMouse);
    }
    public void OnStickMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 PosController = context.ReadValue<Vector2>();
        MouseManager.isUsingController = true;
        MouseManager.UpdateCursorController(PosController);
    }

    // Interaction
    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    // Speed Word 
    public void OnIncreseSpeedWord(InputAction.CallbackContext context)
    {
    }

    public void OnDecreseSpeedWord(InputAction.CallbackContext context)
    {
    }

    // Tools & Actions
    public void OnUseDrog(InputAction.CallbackContext context)
    {
        if (!context.performed || playerManager == null) return;
        playerManager.UseWeed();
    }

    public void OnUseAxe(InputAction.CallbackContext context)
    {
    }

    // Orders / Boxes
    public void OnOpenBoxOrder(InputAction.CallbackContext context)
    {
    }

    //========Input Ui========//

    // Inventory
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            Uimanager.OpenPanelInventory();
        }
        if(context.canceled)
        {
            Uimanager.ClosePanelInventory();
        }
    }

    // Utility Panels
    public void OnOpenPanelUtilty(InputAction.CallbackContext context)
    {
        if (!context.performed || Uimanager == null) return;
        Uimanager.OpenPanelUtilty();
    }

    public void OnClosePanelUtilty(InputAction.CallbackContext context)
    {
        if (!context.performed || Uimanager == null) return;
        Uimanager.ClosePanelUtilty();
    }

    public void OnClosePanelWater(InputAction.CallbackContext context)
    {
    }

    public void OnOpenPanelWater(InputAction.CallbackContext context)
    {
    }

    public void OnOpenTableCrafting(InputAction.CallbackContext context)
    {
    }

    public void OnCloseTableCrafting(InputAction.CallbackContext context)
    {
    }

    public void OnOpenTrashCompactor(InputAction.CallbackContext context)
    {
    }

    public void OnCloseTrashCompactor(InputAction.CallbackContext context)
    {
    }

    public void OnOpenUtilty(InputAction.CallbackContext context)
    {
    }

    public void OnCloseUtilty(InputAction.CallbackContext context)
    {
    }

    public void OnDragController(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            InventoryManager.DragItemController();
        }
    }

    public void OnDropController(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InventoryManager.DropItemController();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnLeftClickMouse(InputAction.CallbackContext context)
    {
    }

    public void OnOpenChest(InputAction.CallbackContext context) { }
    public void OnCloseChest(InputAction.CallbackContext context) { }

}
