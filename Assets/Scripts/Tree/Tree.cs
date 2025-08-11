using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Tilemaps;
using static PlantManager;


public class Tree : MonoBehaviour
{
    [Header("Settings Tree")]
    [SerializeField] private Sprite Sp_trunk;
    [SerializeField] private Sprite Sp_Tree;
    [SerializeField] private int Health = 100;
    [SerializeField] private SlootData Item;
    [SerializeField] private Tilemap tilemapGround;
    public Vector3Int pos;
    public int SizeX;
    public int SizeY;
    public InventoryManager InventoryManager;
    public PlantManager plantManager;

    private void Start() 
    {
        //the position of the tree is set in the cellOccupate in plantManager e for example when plant
        //or placementObj this not place because the cell is occupied by the tree
        pos = tilemapGround.WorldToCell(transform.position);
        for (int x = -1; x < SizeX; x++)
        {
            for(int y = -1; y < SizeY; y++)
            {
                Vector3Int cellPos = new Vector3Int(pos.x + x, pos.y + y);
                plantManager.CellOccupate[cellPos] = new WeedData { StateTerrain = TerrainState.obstacle };
            }
        }
    }

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
