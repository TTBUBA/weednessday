using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Tilemap tile;
    public Direction direction;
    public static Vector3Int currentCell;

    public Animator animatorPlayer;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerInput playerInput;

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
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateCurrentDirection();

        currentCell = tile.WorldToCell(transform.position);
    }

    private void Move()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocity = moveInput * speed; 
        animatorPlayer.SetFloat("Right", moveInput.x);
        animatorPlayer.SetFloat("Left", -moveInput.x);
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
