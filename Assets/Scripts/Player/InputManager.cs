using UnityEngine;
using UnityEngine.InputSystem;

// This script handles player input and delegates logic to other managers.
public class InputManager : MonoBehaviour, InputSystem_Actions.IPlayerActions, InputSystem_Actions.IUIActions
{
    public InputSystem_Actions inputActions;
    public GameObject CurrentObject;

    [Header("Manager References")]
    public PlacementManager PlacementManager;
    public UiManager Uimanager;
    public PlantManager PlantManager;
    public InventoryManager InventoryManager;
    public PhoneManager PhoneManager;
    public PlayerManager playerManager;
    public PlayerMovement PlayerMovement;
    public MouseManager MouseManager;
    public HouseBehaviour HouseBehaviour;
    public BedBehaviur BedBehaviour;
    public EffectFirtsKit EffectFirtsKit;
    public NpcManager NpcManager;
    public Gun Gun;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Utility"))
        {
            CurrentObject = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Utility"))
        {
            CurrentObject = null;
        }
    }

    //========Input Player========//
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Gun.Shoot();
        }
    }

    public void OnReloadGun(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
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
        if(!context.performed && InventoryManager.CurrentSlotSelect?.NameTools != "SeedWeed") return;
        PlantManager.Plant();
    }

    public void OnHoeTerrain(InputAction.CallbackContext context)
    {
        if (!context.performed && InventoryManager.CurrentSlotSelect?.NameTools != "Shovel") return;
        PlantManager.HoeTerrain();
    }

    public void OnWetTerrain(InputAction.CallbackContext context)
    {
        if (!context.performed || InventoryManager.CurrentSlotSelect?.NameTools != "WateringCan") return;
        PlantManager.WetTerrain();
    }

    public void OnCollectPlant(InputAction.CallbackContext context)
    {
        if (!context.performed || PlantManager.plant == null || !PlantManager.plant.FinishGrowth) { return; }
        PlantManager.CollectPlant();
    }

    // Phone
    public void OnOpenPhone(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PhoneManager.OpenPhone();
        }
        PhoneManager.OpenPhone();
    }

    public void OnClosePhone(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PhoneManager.ClosePhone();
        }
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
        if(context.performed && CurrentObject != null)
        {
            BoxOrder boxOrder = CurrentObject.GetComponent<BoxOrder>();
            if (boxOrder == null) return;
            boxOrder.OpenChestOrder();
        }
    }

    //========Input Ui========//

    // Inventory
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            Uimanager.OpenPanelInventory();
        }
    }
    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Uimanager.ClosePanelInventory();
        }
    }

    public void OnChangeSloot(InputAction.CallbackContext context)
    {

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

    public void OnOpenPanelWater(InputAction.CallbackContext context)
    {
        if (context.performed && CurrentObject != null)
        {
            WellSystem wellSystem = CurrentObject.GetComponent<WellSystem>();
            if (wellSystem == null) return;
            wellSystem.Open();
        }

    }
    public void OnClosePanelWater(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            WellSystem wellSystem = CurrentObject.GetComponent<WellSystem>();
            if (wellSystem == null) return;
            wellSystem.Close();
        }

    }

    public void OnOpenTrashCompactor(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            TrashCompactor trashCompactor = CurrentObject.GetComponent<TrashCompactor>();
            if (trashCompactor == null) return;
            trashCompactor.OpenTrashCompactor();
        }
    }
    public void OnCloseTrashCompactor(InputAction.CallbackContext context) 
    { 
        if(context.performed && CurrentObject != null)
        {
            TrashCompactor trashCompactor = CurrentObject.GetComponent<TrashCompactor>();
            if (trashCompactor == null) return;
            trashCompactor.CloseTrashCompactor();
        }
    }
    public void OnOpenUtilty(InputAction.CallbackContext context) { }
    public void OnCloseUtilty(InputAction.CallbackContext context) { }

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

    public void OnNavigate(InputAction.CallbackContext context) { }
    public void OnRightClick(InputAction.CallbackContext context) { }
    public void OnLeftClickMouse(InputAction.CallbackContext context) { }
    public void OnOpenChest(InputAction.CallbackContext context) 
    {
        if(context.performed && CurrentObject != null)
        {
            ChestSystem chestSystem = CurrentObject.GetComponent<ChestSystem>();
            if (chestSystem == null) return;
            chestSystem.OpenPanel();
        }
    }
    public void OnCloseChest(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            ChestSystem chestSystem = CurrentObject.GetComponent<ChestSystem>();
            if (chestSystem == null) return;
            chestSystem.ClosePanel();
        }
    }
    public void OnOpenDesiccator(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            Desiccator desiccator = CurrentObject.GetComponent<Desiccator>();
            if (desiccator == null) return;
            desiccator.OpenEsiccator();
        }
    }
    public void OnCloseDesiccator(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            Desiccator desiccator = CurrentObject.GetComponent<Desiccator>();
            if (desiccator == null) return;
            desiccator.CloseDesiccator();
        }
    }
    public void OnOpenPacker(InputAction.CallbackContext context) 
    { 
        if(context.performed && CurrentObject != null)
        {
            PackerSystem packerSystem = CurrentObject.GetComponent<PackerSystem>();
            if (packerSystem == null) return;
            packerSystem.OpenClosePacker();
        }
    }
    public void OnClosePacker(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            PackerSystem packerSystem = CurrentObject.GetComponent<PackerSystem>();
            if (packerSystem == null) return;
            packerSystem.ClosePacker();
        }
    }
    public void OnOpenPackingSystem(InputAction.CallbackContext context) 
    { 
        if(context.performed && CurrentObject != null)
        {
            PackingWeedSystem packingWeedSystem = CurrentObject.GetComponent<PackingWeedSystem>();
            if (packingWeedSystem == null) return;
            packingWeedSystem.OnOpenPackingSystem();
        }
    }
    public void OnClosePackingSystem(InputAction.CallbackContext context) 
    {
        if (context.performed && CurrentObject != null)
        {
            PackingWeedSystem packingWeedSystem = CurrentObject.GetComponent<PackingWeedSystem>();
            if (packingWeedSystem == null) return;
            packingWeedSystem.OnClosePackingSystem();
        }
    }


    // House and Bed
    public void OnEntryHouse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            HouseBehaviour.EntryHouse();
        }
    }

    public void OnExitHouse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HouseBehaviour.ExitHouse();
        }

    }

    public void OnSleep(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            BedBehaviour.Sleep();
        }
 ;
    }

    public void OnUseFirstKit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EffectFirtsKit.UseFirtsKit();
        }
    }

    public void OnOpenPanelNpc(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            NpcManager.OpenPanelNpc();
        }
    }

    public void OnClosePanelNpc(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NpcManager.ClosePanelNpc();
        }
    }
}
