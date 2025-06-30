using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Boat Settings")]
    [SerializeField] private MovementBoat[] m_Boats;
    public MovementBoat boat;
    public GameObject PointDestination;

    public void ActiveRandomBoat()
    {
        MovementBoat movementBoat = m_Boats[Random.Range(0, m_Boats.Length)];
        boat = movementBoat;
        boat.ActiveBoat();
        PointDestination = boat.CurrentPoint;
    }

    public void ReturnBaseBoat()
    {
        if (boat.stateBoat == MovementBoat.BoatState.Idle)
        {
            boat.ReturnBase();
        }
    }
}
