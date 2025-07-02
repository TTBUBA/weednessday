using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [Header("Settings Police")]
    public Transform PointSpawn;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float Radius;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MaxDistanceTarget;
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private bool ActiveMovementTarget;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private float Angle;
    [SerializeField] private float Distance;
    [SerializeField] private float TimerReturn = 10f;
    public bool ActiveMovement = true;
    public bool ReturnBaseActive;
    public Vector2 StartPos;
    public Vector2 MovementDirection;

    public PoliceGun policeGun;
    private Ray2D ray;
    private RaycastHit2D hit;
    private Coroutine ShootCoroutine;

    private void OnEnable()
    {
        StartPos = PointSpawn.transform.position;
        ActiveMovement = true;
        ReturnBaseActive = false;
        StartCoroutine(ChooseDirection());
        StartCoroutine(TimerReturnBase());
    }

    void Update()
    {
        Raycast();
        MoveToTarget();
        Move();
        ReturnBase();
    }

    private void MoveToTarget()
    {
        if (targetPosition == null) { return; }
        if (!ActiveMovementTarget) { return; }
        Vector3 Target = targetPosition.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.x - MaxDistanceTarget, Target.y - MaxDistanceTarget), speed * Time.deltaTime);
    }

    private void Raycast()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, Radius,LayerMask.GetMask("Player"));
        float Distance = Vector2.Distance(transform.position, targetPosition.transform.position);

        if (hit != null && hit.CompareTag("Player"))
        {
            ActiveMovementTarget = true;
            policeGun.EnableGun = true;

            if(ShootCoroutine == null)
                ShootCoroutine = policeGun.StartCoroutine(policeGun.ActiveShoot());
        }
        else
        {
            ActiveMovementTarget = false;
            policeGun.EnableGun = false;
            if (ShootCoroutine != null)
            {
                policeGun.StopCoroutine(ShootCoroutine);
                ShootCoroutine = null;
            }
        }
    }

    private void Move()
    {
        if (!ActiveMovement) { return; }
        transform.position = Vector2.MoveTowards(transform.position, MovementDirection, speed * Time.deltaTime);
    }

    private void ReturnBase()
    {
        if (ReturnBaseActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, StartPos, speed * Time.deltaTime);
            ActiveMovement = false;
        }
        if (Vector2.Distance(transform.position, StartPos) < 0.1f)
        {
            ActiveMovement = false;
            ReturnBaseActive = false;
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator TimerReturnBase()
    {
        yield return new WaitForSeconds(TimerReturn);
        ReturnBase();
    }

    IEnumerator ChooseDirection()
    {
        //if(!ActiveMovement) { yield return null; }

        while (true)
        {
            Distance = Random.Range(1, 5f);
            Angle = Random.Range(0f, 360f);

            // transform the angle to radians
            float angleRad = Angle * Mathf.Deg2Rad;

            // Calculate the new position based on the angle and distance
            Vector2 LinerFollow = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            MovementDirection = (Vector2)transform.position + LinerFollow * Distance;
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChooseDirection();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (targetPosition != null)
        {
            Gizmos.DrawLine(transform.position, targetPosition.transform.position);
        }
        Gizmos.color = Color.red;
        if(MovementDirection != null)
        {
            Gizmos.DrawLine(transform.position, MovementDirection);
        }

    }
}
