using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Boat Police Spawn")]
    [SerializeField] private MovementBoat[] m_Boats;
    [SerializeField] private Image[] Image_StarPolice;
    [SerializeField] private bool BoatSpawned;
    [SerializeField] private bool BoatReturn;
    public MovementBoat boat;
    public GameObject PointDestination;

    public SpawnPolice SpawnPolice;
    public Cicle_DayNight Cicle_DayNight;

    private void Update()
    {
        ActiveRandomBoat();
        ReturnBaseBoat();
    }
    public void ActiveRandomBoat()
    {
        if (Cicle_DayNight.CurrentHours == 3 && !BoatSpawned)
        {
            MovementBoat movementBoat = m_Boats[Random.Range(0, m_Boats.Length)];
            boat = movementBoat;
            boat.ActiveBoat();
            StartCoroutine(AnimationStar());
            PointDestination = boat.CurrentPoint;
            BoatSpawned = true;
        }
    }

    public void ReturnBaseBoat()
    {
        foreach(var police in SpawnPolice.Police)
        {
            if (police.ReturnBaseActive && police.ActiveMovement && boat.stateBoat == MovementBoat.BoatState.Idle)
            {
                boat.ReturnBase();
                foreach (var image in Image_StarPolice)
                {
                    image.color = new Color(1, 1, 1, 0);
                }
            }
        }
    }
    IEnumerator AnimationStar()
    {
        while(!BoatReturn)
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