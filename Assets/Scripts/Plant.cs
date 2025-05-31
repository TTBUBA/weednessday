using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Sprite[] plants;
    [SerializeField] private int CurrentIndex;
    [SerializeField] private bool FinishGrowth;
    public  void GrowthPlant()
    {
        StartCoroutine(TimeGrowth());
        
        if(CurrentIndex >= plants.Length - 1)
        {
            FinishGrowth = true;
            Debug.Log("Plant has fully grown!");
        }
        else
        {
            FinishGrowth = false;
        }
    }

    IEnumerator TimeGrowth()
    {
        while (CurrentIndex < plants.Length - 1)
        {
            CurrentIndex++;
            GetComponent<SpriteRenderer>().sprite = plants[CurrentIndex];
            yield return new WaitForSeconds(2f); 
        }

    }
}
