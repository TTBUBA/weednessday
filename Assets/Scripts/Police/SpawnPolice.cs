using System.Collections;
using UnityEngine;

public class SpawnPolice : MonoBehaviour
{
    [SerializeField] private GameObject PointspawnPolice;
    public PoliceMovement[] Police;
    private Vector3 PosPolice;
    public MovementBoat MovementBoat;
    public GameManager m_GameManager;
    public bool CoroutineActive = false;
    private void Update()
    {
        if (m_GameManager.boat != null && m_GameManager.boat.speed == 0 && !CoroutineActive)
        {
            ActivePolice();
            StartCoroutine(ActivePoliceSingle());
        }
    }

    public void ActivePolice()
    {

        foreach (var police in Police)
        {
            if (m_GameManager.boat.BoatRight)
            {
                PosPolice = m_GameManager.PointDestination.transform.position - new Vector3(5f, 0, 0);
                police.transform.position = PosPolice;
            }
            else
            {
                PosPolice = m_GameManager.PointDestination.transform.position + new Vector3(5f, 0, 0);
                police.transform.position = PosPolice;
            }
        }
    }

    IEnumerator ActivePoliceSingle()
    {
        yield return new WaitForSeconds(1f);
        CoroutineActive = true;
        for (int i = 0; i < Police.Length; i++)
        {
            Police[i].gameObject.SetActive(true);
        }
    }
}
