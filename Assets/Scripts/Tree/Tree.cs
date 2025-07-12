using UnityEngine;
using DG.Tweening;
using System.Collections;


public class Tree : MonoBehaviour
{
    [Header("Settings Tree")]
    [SerializeField] private Sprite Sp_trunk;
    [SerializeField] private Sprite Sp_Tree;
    [SerializeField] private int Health = 100;
    [SerializeField] private SlootData Item;
    public InventoryManager InventoryManager;
    
    public void TreeHit()
    {
        int randomVal = Random.Range(1, 5);
        int Itemval = Random.Range(1, 3);
        Health -= randomVal;
        InventoryManager.AddItem(Item, Itemval);
        if (Health <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Sp_trunk;
            StartCoroutine(GrowthTree());
        }
        StartCoroutine(AnimatioDestroyTree());
    }

    IEnumerator AnimatioDestroyTree()
    {
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(new Vector3(0.95f, 0.95f, 1f), 0.1f).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        });
    }

    IEnumerator GrowthTree()
    {
        //float TimeGrowth = Random.Range(40f, 60f);
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sp_Tree;
        Health = 100;
    }


}
