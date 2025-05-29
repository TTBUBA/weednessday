using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap Ground;
    [SerializeField] private Tilemap DrawTile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawTile.gameObject.SetActive(false);
    }

    public void ActiveDrawTile()
    {
        DrawTile.gameObject.SetActive(true);
    }
}
