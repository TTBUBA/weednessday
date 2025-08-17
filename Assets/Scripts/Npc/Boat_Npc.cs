using System.Collections;
using TMPro;
using UnityEngine;

public class Boat_Npc : MonoBehaviour
{
    [SerializeField] private Transform PointFinal;
    [SerializeField] private Transform PointStart;
    [SerializeField] private float Speed = 5f;
    [SerializeField] public bool ActiveBoatNpc = false;
    [SerializeField] private bool ArriveDestination;
    [SerializeField] private Transform PointSpawnNpc;
    [SerializeField] private GameObject Npc;
    [SerializeField] private GameObject Player;
    [SerializeField] private AudioSource AudioBoatClacson;
    [SerializeField] private int timedelivery;

    [Header("Ui")]
    public TextMeshProUGUI Text_timedelivery;

    public InventoryManager InventoryManager;
    public NpcManager NpcManager;
    private void Update()
    {
        if (ActiveBoatNpc)
        {
            Move();
            if (Vector3.Distance(transform.position, PointFinal.position) < 0.1f)
            {
                ActiveBoatNpc = false;
                ArriveDestination = true;
                Npc.SetActive(true);
                Npc.transform.position = PointSpawnNpc.position;
                NpcManager.Npc.IsHome = false;
                AudioBoatClacson.Play();
                timedelivery = 0;
                Text_timedelivery.gameObject.SetActive(false);
            }
        }

        if (ArriveDestination && NpcManager.ActiveBoatReturn)
        {
            ReturnTheBase();
            if (Vector3.Distance(transform.position, PointStart.position) < 0.1f)
            {
                ArriveDestination = false;
                transform.localScale = new Vector3(1, -1, 1);
                NpcManager.Npc.IsHome = true;
            }
        }
    }

    //move the boat in the pointfinal
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointFinal.position, Speed * Time.deltaTime);
    }

    //return the boat to the base
    private void ReturnTheBase()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector3.MoveTowards(transform.position, PointStart.position, Speed * Time.deltaTime);
    }


    //Start the delivery time when the time is 0 the boat move in the pointFinal
    public IEnumerator ActiveBoat()
    {
        timedelivery = Random.Range(80, 150);
        StartCoroutine(UpdateTimeDelivery());
        yield return new WaitForSeconds(timedelivery);
        ActiveBoatNpc = true;
    }

    public IEnumerator UpdateTimeDelivery()
    {
        while (timedelivery > 0)
        {
            int minutes = timedelivery / 60;
            int seconds = timedelivery % 60;
            Text_timedelivery.text = $"Time coming: {minutes:D2}:{seconds:D2}";
            yield return new WaitForSeconds(1f);
            timedelivery--;
        }
    }
}
