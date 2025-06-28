using System.Collections;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [Header("Settings Police")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float Radius;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MaxDistanceTarget;
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private bool ActiveMovement;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Vector2 MovementDirection;

    [SerializeField] private Vector2 direction;
    public PoliceGun policeGun;
    private Ray2D ray;
    private RaycastHit2D hit;
    private Coroutine ShootCoroutine;

    private void Awake()
    {
        StartCoroutine(ChooseDirection());
    }

    void Update()
    {
        Raycast();
        MoveToTarget();
        Move();
    }

    private void MoveToTarget()
    {
        if (targetPosition == null) { return; }
        if (!ActiveMovement) { return; }
        Vector3 Target = targetPosition.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.x - MaxDistanceTarget, Target.y - MaxDistanceTarget), speed * Time.deltaTime);
    }

    private void Raycast()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, Radius,LayerMask.GetMask("Player"));
        float Distance = Vector2.Distance(transform.position, targetPosition.transform.position);

        if (hit != null && hit.CompareTag("Player"))
        {
            ActiveMovement = true;
            policeGun.EnableGun = true;

            if(ShootCoroutine == null)
                ShootCoroutine = policeGun.StartCoroutine(policeGun.ActiveShoot());
        }
        else
        {
            ActiveMovement = false;
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
        transform.position = Vector2.MoveTowards(transform.position, MovementDirection, speed * Time.deltaTime);
    }

    IEnumerator ChooseDirection()
    {
        while (true)
        {
            float Distance = Random.Range(0f, 5f);
            float Angle = Random.Range(0f, 360f);
            float angleRad = Angle * Mathf.Deg2Rad;
            Vector2 LinerFollow = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(Angle));
            MovementDirection = new Vector2(transform.position.x + Distance, transform.position.y + Distance) + LinerFollow;
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
        Gizmos.DrawLine(transform.position, MovementDirection);

    }
}
