using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;


    public GameObject SelectBox;
    public Camera CamPlayer;
    public Tilemap tilemapGround;
    public Vector3 MousePos;
    public Vector3Int cellPos;
    public Vector2 PositionController;

    [Header("Input-Controller")]
    [SerializeField] private float SpeedController;
    [SerializeField] private bool isUsingController;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /*
    // This method is called when the controller is moved
    public void OnControllerMoved(InputAction.CallbackContext context)
    {
        isUsingController = true;

        Vector2 ControllerPos = context.ReadValue<Vector2>();

        PositionController += ControllerPos * SpeedController * Time.deltaTime * 100f;

        PositionController.x = Mathf.Clamp(PositionController.x, 0, Screen.width);
        PositionController.y = Mathf.Clamp(PositionController.y, 0, Screen.height);

        UpdateCursorsPos(PositionController);
    }
    */

    //Update the position of the cursors mouse 
    public void UpdateCursorsPos(Vector2 posCursor)
    {
        //Vector3 worldPos = CamPlayer.ScreenToWorldPoint(new Vector3(posCursor.x, posCursor.y, CamPlayer.nearClipPlane));
        //worldPos.z = 0;

        MousePos = CamPlayer.ScreenToWorldPoint(posCursor);
        cellPos = tilemapGround.WorldToCell(MousePos);
        SelectBox.transform.position = tilemapGround.GetCellCenterWorld(cellPos);
    }

    //this method is used to center the position of the controller in the screen
    public void CenterPositionController()
    {
        PositionController = new Vector2(Screen.width * 0.5f , Screen.height * 0.5f);
        UpdateCursorsPos(PositionController);
    }

    // Get the current cell position of the mouse
    public Vector3Int GetMousePosition()
    {
        return cellPos;
    }

    //Get the world position of the mouse
    public Vector3 GetMouseWorldPosition()
    {
        return MousePos;
    }

    //Check if the player is using the controller or not
    public bool IsUsingController()
    {
        return isUsingController;
    }
}
