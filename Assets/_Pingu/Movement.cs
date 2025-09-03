using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;


public enum MotionState
{
    None,
    Turning,
    Stepped,
    Walking,
    Stopping
}



public class Movement : Singleton<Movement>
{ 
    protected override void Awake() => base.Awake();


    public Facing facing = Facing.Up;    
    public MotionState state = MotionState.None;


    [Space]

    public Transform bodySprite;


    [ContextMenu("Initialize Position")]
    public void InitializePosition()
    {
        var groundTilemap = GridElementsManager.Instance.groundTilemap;
        Vector3Int cellPos = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(cellPos);
    }



    void Start()
    {
        Debug.LogWarning("Get step animation duration");
        // secondsToMoveOneTile = durata animazione

        InitializePosition();
    }



    private void Update()
    {
        // check if arrived at destination
        if ((Vector2)transform.position == destination)
        {
            Debug.Log("Arrived at destination: " + transform.position);

            if (state == MotionState.Walking)
            {
                // continue moving
                CalculateDestination();
            }
            else // stepped or stopping
            {
                // stop moving
                state = MotionState.None;
                direction = Vector2.zero;
                this.enabled = false;
            }
        }
        else
        {
            // move towards destination
            transform.Translate(destination * (Time.deltaTime / secondsToMoveOneTile));
        }

    }






    public float secondsToMoveOneTile = 0.25f;
    public Vector2 direction = new(0, 1);
    public Vector2 destination = Vector2.zero;



    private void StartWalking()
    {
        Debug.LogWarning("Controlla qui se ci sono ostacoli");
        // if true return

        CalculateDestination();

        this.enabled = true;
    }



    private void CalculateDestination()
    {
        destination = (Vector2)transform.position + direction;
        Debug.Break();
        Debug.LogWarning("Usa questo se il player non si ferma al centro della cella");
        // destination = new(
        //     Mathf.RoundToInt(destination.x) - 0.5f,
        //     Mathf.RoundToInt(destination.y) + 0.5f);


        StartCoroutine(AntiLoopWalking());
    }


    IEnumerator AntiLoopWalking()
    {
        float secure_control_timer = secondsToMoveOneTile * 2f;

        while (secure_control_timer > 0)
        {
            // se il movimento è finito da solo, esci dalla coroutine
            if (state == MotionState.None)
            {
                StopAllCoroutines();
                break;
            }
            else
            {
                yield return null;
                secure_control_timer -= Time.deltaTime;
            }
        }

        // se arriva qui, il movimento è andato in loop
        Debug.LogError("Secure control timer expired, forcing position to destination");
        transform.position = destination;
    }




    public void PressButton(Vector2 direction)
    {
        if (state != MotionState.None) return;


        // update current state
        state = MotionState.Turning;
        
        // save old direction
        //facing = currFacing;
        
        // update current direction
        this.direction = direction;

        // initialize new direction
        Facing newFacing = facing;
        // check where facing the new direction
        if (direction.x > 0) newFacing = Facing.Right;
        else if (direction.x < 0) newFacing = Facing.Left;
        else if (direction.y > 0) newFacing = Facing.Up;
        else if (direction.y < 0) newFacing = Facing.Down;


        if (newFacing != facing)
        {
            facing = newFacing;
            UpdateSpriteDirection();
            state = MotionState.None;
        }
        else
        {
            state = MotionState.Stepped;
            StartWalking();
        }
    }

    void UpdateSpriteDirection()
    {
        switch (facing)
        {
            case Facing.Left:
                bodySprite.localEulerAngles = new Vector3(0, 0, 090);
                // direction = Vector2.left;
                //direction = direction;
                break;
            case Facing.Right:
                bodySprite.localEulerAngles = new Vector3(0, 0, 270);
                // direction = Vector2.right;
                //direction = direction;
                break;
            case Facing.Up:
                bodySprite.localEulerAngles = new Vector3(0, 0, 000);
                // direction = Vector2.up;
                //direction = direction;
                break;
            case Facing.Down:
                bodySprite.localEulerAngles = new Vector3(0, 0, 180);
                // direction = Vector2.down;
                //direction = direction;
                break;
        }
    }

    public void EnableMovement()
    {
        if (state != MotionState.Turning) return;
        
        state = MotionState.Walking;
        StartWalking();
    }

    public void ReleaseButton()
    {
        if (state != MotionState.Walking) return;
        state = MotionState.Stopping;
        // StartCoroutine(StopMovementAfterDelay());
    }
}
