using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Sprite[] plants;
    [SerializeField] private int CurrentIndex;
    [SerializeField] public bool FinishGrowth;

    public void GrowthPlant()
    {
        StartCoroutine(TimeGrowth());
        
        if(CurrentIndex >= plants.Length - 1)
        {
            FinishGrowth = true;
        }
        else
        {
            FinishGrowth = false;
        }
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
            yield return new WaitForSeconds(5f);
            CurrentIndex++;
        }

    }
}
