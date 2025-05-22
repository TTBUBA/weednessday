using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
public class WordGenerator : MonoBehaviour
{
    [Header("Generator-Ground")]
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap Ground;
    [SerializeField] private Tile SpriteTerrain;
    [SerializeField] private List<Vector3Int> CellOccupate;
    [SerializeField] private Vector3Int lastPos;
    [SerializeField] private int ChunkSize;
    [SerializeField] private Vector3Int firstChunkOrigin = Vector3Int.zero;
    private Vector3Int currentChunkOrigin; 
    private Vector3Int lastChunkTriggerCell;
    private HashSet<Vector3Int> CellOccupateHashSet = new HashSet<Vector3Int>();

    [Header("Generator-Nature")]
    [SerializeField] private Tilemap Nature;
    [SerializeField] private Tile[] SpriteNature;
    void Awake()
    {
        currentChunkOrigin = firstChunkOrigin; 
        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                Ground.SetTile(new Vector3Int(firstChunkOrigin.x + x, firstChunkOrigin.y + y, 0), SpriteTerrain);
                CellOccupate.Add(new Vector3Int(firstChunkOrigin.x + x, firstChunkOrigin.y + y, 0));
            }
        }
        lastPos = firstChunkOrigin;
    }

    private void Update()
    {
        GenerateChunk();
    }

    public void GeneratorNature(Vector3Int originPos)
    {
        int TotalTiles = Random.Range(1, 6);
        for (int i = 0; i < TotalTiles; i++)
        {
            Vector3Int Pos;
            int posX = Random.Range(0, ChunkSize);
            int posY = Random.Range(0, ChunkSize);
            Pos = new Vector3Int(originPos.x + posX, originPos.y + posY, 0);
            Nature.SetTile(Pos, SpriteNature[Random.Range(0, SpriteNature.Length)]); // set nature tile
        }
    }
    //create a chunk
    public void GenerateChunk()
    {
        Vector3Int spawnPoint = currentChunkOrigin; 
        Vector3Int playerPos = PlayerMovement.currentCell;

        //check if the player is in the chunk
        switch (PlayerMovement.direction)
        {
            case PlayerMovement.Direction.up:
                if (playerPos.y >= currentChunkOrigin.y + ChunkSize - 2)
                {
                    spawnPoint = new Vector3Int(currentChunkOrigin.x, currentChunkOrigin.y + ChunkSize, 0);
                }
                break;
            case PlayerMovement.Direction.down:
                if (playerPos.y <= currentChunkOrigin.y + 2)
                {
                    spawnPoint = new Vector3Int(currentChunkOrigin.x, currentChunkOrigin.y - ChunkSize, 0);
                }
                break;
            case PlayerMovement.Direction.right:
                if (playerPos.x >= currentChunkOrigin.x + ChunkSize - 2)
                {
                    spawnPoint = new Vector3Int(currentChunkOrigin.x + ChunkSize, currentChunkOrigin.y, 0);
                }
                break;
            case PlayerMovement.Direction.left:
                if (playerPos.x <= currentChunkOrigin.x + 2)
                {
                    spawnPoint = new Vector3Int(currentChunkOrigin.x - ChunkSize, currentChunkOrigin.y, 0);
                }
                break;
        }

        //check if the chunk already exists
        if (CellOccupateHashSet.Contains(spawnPoint)) { return; }
    
        //check if the spawnPoint is the same as the lastPos
        if (spawnPoint == lastChunkTriggerCell) { return; }

        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                // set tile and add position
                Vector3Int TilePos = new Vector3Int(spawnPoint.x + x, spawnPoint.y + y, 0);
                Ground.SetTile(TilePos, SpriteTerrain);
                CellOccupate.Add(TilePos);//add a tile to the list          
            }
        }
        GeneratorNature(spawnPoint);
        CellOccupateHashSet.Add(spawnPoint);
        currentChunkOrigin = spawnPoint; //update the current chunk origin
        lastPos = spawnPoint;
        lastChunkTriggerCell = spawnPoint;
    }
}
