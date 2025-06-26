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

    public PoliceGun policeGun;
    private Ray2D ray;
    private RaycastHit2D hit;
    private Coroutine ShootCoroutine;


    // Update is called once per frame
    void Update()
    {
        Raycast();
        Move();
    }

    private void Move()
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (targetPosition != null)
        {
            Gizmos.DrawLine(transform.position, targetPosition.transform.position);
        }
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
