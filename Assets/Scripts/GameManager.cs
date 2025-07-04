using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Boat Spawn")]
    [SerializeField] private MovementBoat[] m_Boats;

    public MovementBoat boat;
    public GameObject PointDestination;
    private bool BoatSpawned;
    private bool BoatReturn;

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
            }
        }
    }
}
