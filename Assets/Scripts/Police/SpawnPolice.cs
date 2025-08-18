using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPolice : MonoBehaviour
{
    [Header("Boat Police Spawn")]
    [SerializeField] private BoatPoliceMovement[] m_Boats;
    [SerializeField] private Image[] Image_StarPolice;
    [SerializeField] private bool BoatSpawned;
    [SerializeField] private bool BoatReturn;
    [SerializeField] private NpcData[] NpcData;
    [SerializeField] private BoatPoliceMovement boat;
    [SerializeField] private GameObject PointDestination;
    [SerializeField] private GameObject PointspawnPolice;
    public PoliceAi[] Police;
    private Vector3 PosPolice;
    public BoatPoliceMovement MovementBoat;
    public bool CoroutineActive = false;

    private void Update()
    {
        if (boat != null && boat.speed == 0 && !CoroutineActive)
        {
            ActivePolice();
            StartCoroutine(ActivePoliceSingle());
        }
    }

    //ActivePolice Boat
    public void ActiveRandomBoatPolice()
    {
        foreach (var npc in NpcData)
        {
            if (!BoatSpawned)
            {
                BoatPoliceMovement movementBoat = m_Boats[Random.Range(0, m_Boats.Length)];
                boat = movementBoat;
                boat.ActiveBoat();
                StartCoroutine(AnimationStar());
                PointDestination = boat.CurrentPoint;
                BoatSpawned = true;
            }
        }

    }

    public void ReturnBaseBoatPolice()
    {
        foreach (var police in Police)
        {
            if (police.ReturnBaseActive && police.ActiveMovement && boat.stateBoat == BoatPoliceMovement.BoatState.Idle)
            {
                boat.ReturnBase();
                foreach (var image in Image_StarPolice)
                {
                    image.color = new Color(1, 1, 1, 0);
                }
            }
        }
    }


    //Active police when boat is stopped
    public void ActivePolice()
    {

        foreach (var police in Police)
        {
            if (boat.BoatRight)
            {
                PosPolice = PointDestination.transform.position - new Vector3(5f, 0, 0);
                police.transform.position = PosPolice;
            }
            else
            {
                PosPolice = PointDestination.transform.position + new Vector3(5f, 0, 0);
                police.transform.position = PosPolice;
            }
        }
    }

    //Active 1 police after 1 second
    IEnumerator ActivePoliceSingle()
    {
        yield return new WaitForSeconds(1f);
        CoroutineActive = true;
        for (int i = 0; i < Police.Length; i++)
        {
            Police[i].gameObject.SetActive(true);
        }
    }

    //Animation of the stars on the police boat
    IEnumerator AnimationStar()
    {
        while (!BoatReturn)
        {
            yield return new WaitForSeconds(1f);
            foreach (var image in Image_StarPolice)
            {
                image.color = new Color(1, 1, 1, 0);
            }

            yield return new WaitForSeconds(1f);
            foreach (var image in Image_StarPolice)
            {
                image.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
