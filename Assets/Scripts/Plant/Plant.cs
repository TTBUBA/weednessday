using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Sprite[] plants;
    [SerializeField] private int CurrentIndex;
    [SerializeField] public int time;
    [SerializeField] public bool FinishGrowth;
    public void GrowthPlant()
    {
        StartCoroutine(TimeGrowth());
    }

    public void ResetPlant()
    {
        CurrentIndex = 0;
        FinishGrowth = false;
        GetComponent<SpriteRenderer>().sprite = null;
    }

    IEnumerator TimeGrowth()
    {
        while (CurrentIndex < plants.Length - 1)
        {
            GetComponent<SpriteRenderer>().sprite = plants[CurrentIndex];
            time = Random.Range(5, 10);
            yield return new WaitForSeconds(time);
            CurrentIndex++;
        }
        FinishGrowth = true;
    }
}
