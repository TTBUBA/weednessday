using System.Collections;
using UnityEngine;

public class SpawnPolice : MonoBehaviour
{
    [SerializeField] private GameObject PointspawnPolice;

    public MovementBoat MovementBoat;
    private void Update()
    {
        ActivePolice();
    }
    
    private void ActivePolice()
    {
        if (MovementBoat.speed == 0)
        {
            PointspawnPolice.gameObject.SetActive(true);
        }
    }
    
    private void ReturnBase()
    {

    }
}
