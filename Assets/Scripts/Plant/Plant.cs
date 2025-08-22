using System.Collections;
using UnityEngine;


public class Plant : MonoBehaviour
{
    [SerializeField] private Sprite[] plants;
    [SerializeField] private int CurrentIndex;
    public float time;
    public float TimeBase;
    public bool FinishGrowth;
    public bool IsWet;

    public void GrowthPlant()
    {
        TimeBase = time;
        StartCoroutine(TimeGrowth());
    }

    public void ResetPlant()
    {
        CurrentIndex = 0;
        FinishGrowth = false;
        time = 0f;
        TimeBase = 0f;
        GetComponent<SpriteRenderer>().sprite = null;
    }
    IEnumerator TimeGrowth()
    {
        while (CurrentIndex < plants.Length)
        {
            GetComponent<SpriteRenderer>().sprite = plants[CurrentIndex];
            yield return new WaitForSeconds(time);
            CurrentIndex++;
        }
        FinishGrowth = true;
        time = TimeBase;
    }
}
