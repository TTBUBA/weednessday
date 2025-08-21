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
    public bool IsMoving;
    public Transform SelectBox;
    public Vector3Int NextPosBox;

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
        transform.position = tilemapGround.GetCellCenterWorld(CurrentPosCell);
        SelectBox.position = tilemapGround.GetCellCenterWorld(NextPosBox);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateCurrentDirection();
    }

    private void Move()
    {
        if (!IsMoving && moveInput != Vector2.zero)
        {
            CurrentPosCell = tilemapGround.WorldToCell(transform.position);
            Vector3Int Direction = new Vector3Int((int)moveInput.x, (int)moveInput.y, 0);
            NextPosCell = CurrentPosCell + Direction;
            IsMoving = true;

            if(IsCellWalkable(NextPosCell))
            {
                IsMoving = true;
            }
            else
            {
                moveInput = Vector2.zero;
            }
        }

        if (IsMoving)
        {
            Vector3 targetPos = tilemapGround.GetCellCenterWorld(NextPosCell);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);



            NextPosBox = NextPosCell;
            switch (direction)
            {
                case Direction.up:
                    NextPosBox += new Vector3Int(0, 1, 0);
                    break;
                case Direction.right:
                    NextPosBox += new Vector3Int(1, 0, 0);
                    break;
                case Direction.down:
                    NextPosBox += new Vector3Int(0, -1, 0);
                    break;
                case Direction.left:
                    NextPosBox += new Vector3Int(-1, 0, 0);
                    break;
            }

            SelectBox.position = tilemapGround.GetCellCenterWorld(NextPosBox);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                transform.position = targetPos;
                IsMoving = false;
            }
        }

        animatorPlayer.SetBool("IsMoving", moveInput != Vector2.zero);
    }

    private bool IsCellWalkable(Vector3Int cell)
    {
        //if (tilemapGround.HasTile(cell)) return false;

        return true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsMoving = false;
        moveInput = Vector2.zero;
        transform.position = tilemapGround.GetCellCenterWorld(CurrentPosCell);
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
