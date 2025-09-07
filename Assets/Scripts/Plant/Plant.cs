using System.Collections;
using UnityEditor;
using UnityEngine;


public class Plant : MonoBehaviour
{
    public Sprite[] plants;
    public int CurrentIndex;
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

    public void StatePlant()
    {
        if(FinishGrowth)
        {
            GetComponent<SpriteRenderer>().sprite = plants[plants.Length - 1];
        }
        else if(!FinishGrowth)
        {
            if(CurrentIndex > 0 && CurrentIndex < plants.Length)
            {
                GetComponent<SpriteRenderer>().sprite = plants[CurrentIndex];
                GrowthPlant();
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
}
