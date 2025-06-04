using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [Header("Placement Settings")]
    public PlaceableObjectData CurrentplaceableObject;
    [SerializeField] private Camera CamPlayer;

    [Header("Ui")]
    public GameObject Panel_Utility;
    [SerializeField] private GameObject Butt_OpenPanelUtilty;
    [SerializeField] private Tilemap Tm_ObjectPlacement;
    [SerializeField] private Tilemap DrawTile;

    private bool DrawModeActive = false;
    private Vector3 MousePos;
    private Vector3Int cellPos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        MousePos = Mouse.current.position.ReadValue();
        MousePos = CamPlayer.ScreenToWorldPoint(MousePos);
        cellPos = Tm_ObjectPlacement.WorldToCell(MousePos);
    }

    public void PlaceObject()
    {
        if (DrawModeActive)
        {
            Tm_ObjectPlacement.SetTile(cellPos, CurrentplaceableObject.Object);
        }
    }

    public void RemoveObjectPlace()
    {
        if (DrawModeActive)
        {
            Tm_ObjectPlacement.SetTile(cellPos, null);
        }
    }

    //======Ui======//
    public void ActivePanel()
    {
        Panel_Utility.SetActive(true);
        Butt_OpenPanelUtilty.SetActive(false);
        DrawTile.gameObject.SetActive(true);
        DrawModeActive = true;
    }

    public void DeactivePanel()
    {
        Butt_OpenPanelUtilty.SetActive(true);
        DrawTile.gameObject.SetActive(false);
        Panel_Utility.SetActive(false);
        DrawModeActive = false;
    }
}
