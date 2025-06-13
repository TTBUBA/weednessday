using System.Collections;
using UnityEngine;

public class WellSystem : MonoBehaviour
{
    [SerializeField] private bool IsCollision;
    [SerializeField] private float LevelWater;
    [SerializeField] private float TimeCharge;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChargeWaterCan();
        }
    }
    
    private void ChargeWaterCan()
    {
        StartCoroutine(ChargeWater());
    }

    IEnumerator ChargeWater()
    {
        while (LevelWater >= 10)
        {
            yield return new WaitForSeconds(TimeCharge);
            LevelWater += 0.2f;
        }
    }
}
