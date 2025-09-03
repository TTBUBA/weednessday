using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;


[System.Flags]
public enum ElementType
{
    Empty       = 0,
    NoTile      = 1 << 0,   // 0000 0001

    Player      = 1 << 1,   // 0000 0010
    Police      = 1 << 2,   // 0000 0100
    NPC         = 1 << 3,   // 0000 1000

    Plant       = 1 << 4,   // 0001 0000
    Obstacle    = 1 << 5,   // 0010 0000
}


public class GridElementsManager : Singleton<GridElementsManager>
{
    public Tilemap groundTilemap;

    ElementType[,] gridElements;


    public TextMeshProUGUI Xcoord;
    public TextMeshProUGUI Ycoord;


    protected override void Awake() => base.Awake();

    void Start()
    {
        Vector3Int gridSize = groundTilemap.size;
        gridElements = new ElementType[gridSize.x, gridSize.y];


        var player_pos = groundTilemap.WorldToCell(Movement.Instance.transform.position);

        Xcoord.text = player_pos.x.ToString();
        Ycoord.text = player_pos.y.ToString();



        // Initialize the grid with None
        //        for (int x = 0; x < gridSize.x; x++)
        //        {
        //            for (int y = 0; y < gridSize.y; y++)
        //            {
        //                if (groundTilemap.GetTile(new Vector3Int(x + groundTilemap.origin.x, y + groundTilemap.origin.y, 0)) == null)
        //                    gridElements[x, y] = ElementType.NoTile;
        //                else
        //                    gridElements[x, y] = ElementType.Empty;
        //            }
        //        }
        Debug.Log($"Grid initialized with size: {gridSize.x}x{gridSize.y}");
    }

    
}