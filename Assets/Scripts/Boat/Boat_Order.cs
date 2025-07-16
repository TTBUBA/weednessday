using UnityEngine;

public class Boat_Order : MonoBehaviour
{
    [SerializeField] private Transform PointFinal;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool ActiveBoat = false;
    [SerializeField] private bool ArriveDestination;
    [SerializeField] private GameObject Pilot;
    private void Update()
    {
        if (ActiveBoat)
        {
            Move();
            if (Vector3.Distance(transform.position, PointFinal.position) < 0.1f)
            {
                ActiveBoat = false;
                ArriveDestination = true;
            }
        }
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,PointFinal.position,Speed * Time.deltaTime);
    }


}
