using System.Collections;
using UnityEngine;

public class Boat_Order : MonoBehaviour
{
    [SerializeField] private Transform PointFinal;
    [SerializeField] private Transform PointStart;
    [SerializeField] private float Speed = 5f;
    [SerializeField] public bool ActiveBoat = false;
    [SerializeField] private bool ArriveDestination;
    [SerializeField] private Transform[] PointSpawnChest;
    [SerializeField] private GameObject Box;
    [SerializeField] private GameObject Player;
    [SerializeField] private AudioSource AudioBoatClacson;
    [SerializeField] private int timedelivery;



    public MarketManager marketManager;
    public InventoryManager InventoryManager;
    public BoxOrder boxOrder;
    private void Update()
    {
        if (ActiveBoat)
        {
            Move();
            if (Vector3.Distance(transform.position, PointFinal.position) < 0.1f)
            {
                ActiveBoat = false;
                ArriveDestination = true;
                int Point = Random.Range(0, PointSpawnChest.Length);
                Box.SetActive(true);
                Box.transform.position = PointSpawnChest[Point].position;
                AudioBoatClacson.Play();
            }
        }
        if (ArriveDestination && boxOrder.OpenBox)
        {
            ReturnTheBase();
            if (Vector3.Distance(transform.position, PointStart.position) < 0.1f)
            {
                ArriveDestination = false;
                boxOrder.OpenBox = false;
                transform.localScale = new Vector3(1, -1, 1);
            }
        }
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,PointFinal.position,Speed * Time.deltaTime);
    }

    private void ReturnTheBase()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector3.MoveTowards(transform.position, PointStart.position, Speed * Time.deltaTime);
    }

    public IEnumerator TimeDelivery()
    {
        timedelivery = Random.Range(250, 300);
        yield return new WaitForSeconds(timedelivery);
        ActiveBoat = true;
        Debug.Log("Time deliver real" + timedelivery / 60 + "Minutes");
    }
}
