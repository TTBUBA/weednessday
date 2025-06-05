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

    [Header("Input-Mouse")]
    [SerializeField] private InputActionReference mouseMove;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        mouseMove.action.Enable();
        mouseMove.action.performed += OnMouseMoved;
    }
    private void OnDisable()
    {
        mouseMove.action.Disable();
        mouseMove.action.performed -= OnMouseMoved;
    }

    public void OnMouseMoved(InputAction.CallbackContext context)
    {
        Vector2 MouseScreenPos = context.ReadValue<Vector2>();
        MousePos = CamPlayer.ScreenToWorldPoint(MouseScreenPos);
        cellPos = tilemapGround.WorldToCell(MousePos);

        SelectBox.transform.position = tilemapGround.GetCellCenterWorld(cellPos);
    }

    public Vector3Int GetMousePosition()
    {
        return cellPos;
    }

}
