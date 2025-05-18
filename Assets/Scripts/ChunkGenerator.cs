using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile SpriteTerrain;

    [SerializeField] private List<Vector3Int> CellOccupate;
    [SerializeField] private Vector3Int lastPos;
    [SerializeField] private int ChunkSize;
    [SerializeField] private Direction direction;
    public enum Direction 
    { 
       up,
       right,
       down,
       left
    }

    void Awake()
    {
        for(int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), SpriteTerrain);
                CellOccupate.Add(new Vector3Int(x, y, 0));
                lastPos = new Vector3Int(x, y, 0);
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            createchunk(Direction.right);
        }
    }

    //create a chunkRight 
    public void createchunk(Direction direction)
    {
        //check if the lastPos 
        Vector3Int startPos = lastPos;
        switch (direction)
        {
            case Direction.up:
                lastPos = new Vector3Int(lastPos.x + (ChunkSize - 1), lastPos.y + (ChunkSize - 1), 0);
            break;

            case Direction.down:
                lastPos = new Vector3Int(lastPos.x + (ChunkSize - 1), lastPos.y, 0);
                break;
            case Direction.right:
                lastPos = new Vector3Int(lastPos.x + (ChunkSize - 1), lastPos.y, 0);
                break;
            case Direction.left:
                lastPos = new Vector3Int(lastPos.x, lastPos.y + ChunkSize, 0);
            break;
        }

        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                // set tile and add position
                Vector3Int TilePos = new Vector3Int(startPos.x + x, startPos.y + y, 0);
                tilemap.SetTile(new Vector3Int(TilePos.x, TilePos.y, 0), SpriteTerrain);
                CellOccupate.Add(new Vector3Int(x, y, 0));//add a tile to the list          
            }
        }

        switch (direction)
        {
            case Direction.up:
                lastPos = new Vector3Int(lastPos.x + (ChunkSize - 1), lastPos.y + (ChunkSize - 1), 0);
                break;

            case Direction.down:
                lastPos = new Vector3Int(lastPos.x + (ChunkSize - 1), lastPos.y, 0);
                break;
            case Direction.right:
                lastPos = new Vector3Int(lastPos.x + (ChunkSize - 1), lastPos.y, 0);
                break;
            case Direction.left:
                lastPos = new Vector3Int(lastPos.x, lastPos.y + (ChunkSize - 1), 0);
                break;
        }
    }
}
