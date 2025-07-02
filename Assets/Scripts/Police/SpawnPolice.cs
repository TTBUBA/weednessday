using System.Collections;
using UnityEngine;

public class SpawnPolice : MonoBehaviour
{
    [SerializeField] private GameObject PointspawnPolice;
    public PoliceMovement[] Police;
    private Vector3 PosPolice;
    public MovementBoat MovementBoat;
    public GameManager m_GameManager;

    private void Update()
    {
        if (m_GameManager.boat != null && m_GameManager.boat.speed == 0)
        {
            ActivePolice();
        }
    }

    public void ActivePolice()
    {
        foreach (var police in Police)
        {
            police.gameObject.SetActive(true);
        }
        if (m_GameManager.boat.BoatRight)
        {
            PosPolice = m_GameManager.PointDestination.transform.position - new Vector3(5f,0,0);
            PointspawnPolice.transform.position = PosPolice;
        }
        else
        {
            PosPolice = m_GameManager.PointDestination.transform.position + new Vector3(5f,0, 0);
            PointspawnPolice.transform.position = PosPolice;
        }
    }
}
