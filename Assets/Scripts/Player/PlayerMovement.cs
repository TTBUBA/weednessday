using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Direction direction;

    public Animator animatorPlayer;
    private Rigidbody2D rb;
    public Vector2 moveInput;
    private PlayerInput playerInput;
    public Tilemap tilemapGround;
    public Vector3Int CurrentPosCell;
    public Vector3Int NextPosCell;
    public float TimeMove;
    public enum Direction
    {
        up,
        right,
        down,
        left
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animatorPlayer = GetComponent<Animator>();
        CurrentPosCell = tilemapGround.WorldToCell(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateCurrentDirection();
    }

    private void Move()
    {
        if (moveInput.y > 0)
        {
            CurrentPosCell = tilemapGround.WorldToCell(transform.position);
            NextPosCell = new Vector3Int(CurrentPosCell.x, CurrentPosCell.y + 1, CurrentPosCell.z);
            Vector3 nextWorldPosition = tilemapGround.CellToWorld(NextPosCell) + new Vector3(0.5f, 0.5f, 0f); 
            transform.position = Vector3.Lerp(transform.position, nextWorldPosition, TimeMove);
        }
        if (moveInput.y < 0)
        {
            CurrentPosCell = tilemapGround.WorldToCell(transform.position);
            NextPosCell = new Vector3Int(CurrentPosCell.x, CurrentPosCell.y - 1, CurrentPosCell.z);
            Vector3 nextWorldPosition = tilemapGround.CellToWorld(NextPosCell) + new Vector3(0.5f, 0.5f, 0f);
            transform.position = Vector3.Lerp(transform.position, nextWorldPosition, TimeMove);
        }
        if (moveInput.x > 0)
        {
            CurrentPosCell = tilemapGround.WorldToCell(transform.position);
            NextPosCell = new Vector3Int(CurrentPosCell.x + 1, CurrentPosCell.y, CurrentPosCell.z);
            Vector3 nextWorldPosition = tilemapGround.CellToWorld(NextPosCell) + new Vector3(0.5f, 0.5f, 0f);
            transform.position = Vector3.Lerp(transform.position, nextWorldPosition, TimeMove);
        }
        if (moveInput.x < 0)
        {
            CurrentPosCell = tilemapGround.WorldToCell(transform.position);
            NextPosCell = new Vector3Int(CurrentPosCell.x - 1, CurrentPosCell.y, CurrentPosCell.z);
            Vector3 nextWorldPosition = tilemapGround.CellToWorld(NextPosCell) + new Vector3(0.5f, 0.5f, 0f);
            transform.position = Vector3.Lerp(transform.position, nextWorldPosition, TimeMove);
        }
        animatorPlayer.SetBool("IsMoving", moveInput != Vector2.zero);
    }
    private void UpdateCurrentDirection()
    {
        if (moveInput.y > 0)
        {
            direction = Direction.up;
        }
        else if (moveInput.x > 0)
        {
            direction = Direction.right;
        }
        else if (moveInput.y < 0)
        {
            direction = Direction.down;
        }
        else if (moveInput.x < 0)
        {
           direction = Direction.left;
        }
    }
}
