using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static PlantManager;
using static UnityEditor.PlayerSettings;

public class PoliceAi : MonoBehaviour
{
    [SerializeField] private Transform target; // Target to follow
    [SerializeField] private Tilemap ground;
    [SerializeField] private bool activeGizmos;


    [SerializeField] private List<Vector3Int> CellToSearch = new List<Vector3Int>();
    [SerializeField] private List<Vector3Int> path = new List<Vector3Int>();
    [SerializeField] private int CurrentIndexPath;


    public PlantManager plantManager;
    public Tree[] tree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoundsInt boundsInt = ground.cellBounds;
    
        for (int x = boundsInt.xMin; x < boundsInt.xMax; x++)
        {
            for (int y = boundsInt.yMin; y < boundsInt.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = ground.GetTile(pos);
                if(tile != null)
                {
                    CellToSearch.Add(pos); // Add all points in the tilemap to the PointPath list
                }
            }
        }
        foreach (var tree in tree)
        {
            Vector3Int pos = ground.WorldToCell(tree.transform.position);
            for (int x = -1; x < tree.SizeX; x++)
            {
                for (int y = -1; y < tree.SizeY; y++)
                {
                    Vector3Int cellPos = new Vector3Int(pos.x + x, pos.y + y);
                    Debug.Log(cellPos);
                    CellToSearch.Remove(cellPos);
                }
            }
        }
    }

    private void Update()
    {
        Vector3Int starPos = ground.WorldToCell(transform.position);
        Vector3Int endPos = ground.WorldToCell(target.position);
        path = FindPath(starPos, endPos);
        MovetoTarget();
    }

    private void MovetoTarget()
    {
        if(path.Count == 0 || CurrentIndexPath >= path.Count) { return; } // No path found or index out of bounds

        Vector3 CurrentPosCell = transform.position;
        Vector3Int NextPosCell = path[CurrentIndexPath];
        Vector3 targetPos = ground.CellToWorld(NextPosCell) + new Vector3(0.5f, 0.5f, 0f);
        transform.transform.position = Vector3.MoveTowards(CurrentPosCell, targetPos, Time.deltaTime * 4f);

        if (Vector3.Distance(CurrentPosCell, targetPos) < 0.1f)
        {
            CurrentIndexPath++;
            if (CurrentIndexPath >= path.Count)
            {
                CurrentIndexPath = 0; // Reset index if we reached the end of the path
            }
        }

    }
    List<Vector3Int> FindPath(Vector3Int startPos , Vector3Int endPos)
    {
        var openSet = new List<Vector3Int>() { startPos };
        var comeFrom = new Dictionary<Vector3Int, Vector3Int>();

        var gScore = new Dictionary<Vector3Int, int>();
        gScore[startPos] = 0; // Cost from start to start is 0

        var fScore = new Dictionary<Vector3Int, int>();
        fScore[startPos] = Heuristic(startPos, endPos); // Estimated cost from start to end

        while (openSet.Count > 0)
        {
            //get the node with fscore minor
            Vector3Int current = openSet[0];
            foreach (var node in openSet)
            {
                if (fScore.ContainsKey(node) && fScore[node] < fScore[current])
                {
                    current = node;
                }
            }

            if (current == endPos)
            {
                return BuildThePath(comeFrom, current); // Path found
            }

            openSet.Remove(current);

            foreach(var neighbor in GetNeighBords(current))
            {
                if(CellToSearch.Contains(neighbor)) // Check if the neighbor is a valid cell
                {
                    int tentativeGScore = gScore[current] + 1; // Assuming cost to move to neighbor is 1

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        comeFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, endPos);
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
        return new List<Vector3Int>(); 
    }

    int Heuristic(Vector3Int a, Vector3Int b)
    {
        // Using Manhattan distance as heuristic
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    List<Vector3Int> GetNeighBords(Vector3Int cell)
    {
        return new List<Vector3Int>
        {
            cell + new Vector3Int(1, 0, 0),
            cell + new Vector3Int(-1, 0, 0),
            cell + new Vector3Int(0, 1, 0),
            cell + new Vector3Int(0, -1, 0),
        };
    }

    List<Vector3Int> BuildThePath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        var totalPath = new List<Vector3Int> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);
        }
        totalPath.Reverse(); // Reverse the path to get it from start to end
        return totalPath;
    }

    private void OnDrawGizmos()
    {
        if (activeGizmos)
        {
            Gizmos.color = Color.red; // Set the color for the Gizmos
            foreach (Vector3Int point in CellToSearch)
            {
                Vector3 worldPosition = ground.GetCellCenterWorld(point); // Get the world position of the tile
                Gizmos.DrawCube(worldPosition, new Vector3(0.2f, 0.2f, 0f));
            }

            Gizmos.color = Color.blue; // Set the color for the Gizmos
            foreach (Vector3Int point in path)
            {
                Vector3 worldPosition = ground.GetCellCenterWorld(point); 
                Gizmos.DrawSphere(worldPosition, 0.1f); 
            }
        }
    }
}

