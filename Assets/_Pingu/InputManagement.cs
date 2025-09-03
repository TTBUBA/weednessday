using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputManagement : Singleton<InputManagement>, InputSystem_Actions.IPlayerActions
{
    protected override void Awake() => base.Awake();


    Movement movement;
    public InputSystem_Actions input;


    #region SETUP

    private void Start()
    {
        movement = GetComponent<Movement>();
        input.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        input= new InputSystem_Actions();
        input.Player.Enable();
        input.UI.Enable();
        
        EventManager.Instance.OnGamePauseSubscribe(ReleaseButton);
    }

    private void OnDisable()
    {
        input.Player.Disable();
        input.UI.Disable();

        EventManager.Instance.OnGamePauseUnsubscribe(ReleaseButton);
    }

    #endregion


    [SerializeField] bool isKeyPressed = false;

    [SerializeField] InputControl lastMoveKeyPressed;


    public void OnMove(InputAction.CallbackContext context)
    {
        var same_key = (lastMoveKeyPressed == context.control);


        if (context.started && isKeyPressed == false)
        {
            print("PRESS: " + context.control);

            isKeyPressed = true;
            lastMoveKeyPressed = context.control;
            movement.PressButton(direction: context.ReadValue<Vector2>());
        }
        if (context.performed && same_key)
        {
            print("HOLD: " + context.control);
            movement.EnableMovement();
        }
        if (context.canceled && same_key)
        {
            print("RELEASED: " + context.control);
            ReleaseButton();
        }
    }
    

    private void ReleaseButton(bool resumeFromPause = false)
    {
        if (isKeyPressed && !resumeFromPause)
        {
            isKeyPressed = false;
            //DisableMovement();
        }
    }

    

    #region NON NECESSARI
    public void OnCollectPlant(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnDecreseSpeedWord(InputAction.CallbackContext context)
    {
        if (context.started){ }
        if (context.performed) { }
        if (context.canceled) { }
    }
    public void OnHoeTerrain(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnIncreseSpeedWord(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnLeftClickMouse(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnMousePos(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnOpenBoxOrder(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnPlacementObjet(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnPlant(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnReloadGun(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnRemoveObjetPlacement(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnSleep(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnStickMove(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnUseAxe(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnUseDrog(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnUseFirstKit(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    public void OnWetTerrain(InputAction.CallbackContext context)
    {
        if (context.started) {}
        if (context.performed) {}
        if (context.canceled) {}
    }
    #endregion
}
