using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableObjectData", menuName = "Scriptable Objects/PlaceableObjectData")]
public class PlaceableObjectData : ScriptableObject
{
    public string UtilityName;
    public string SpaceOccupied;
    public int SpaceOccupiedX , SpaceOccupiedY;
    public string UtilityDescription;
    public int UtilityID;
    public int Cost;
    public Sprite UtilityIcon;
    public GameObject UtilityPrefab;
    public PlaceableObjectType Type;
    public RuleTile Object;
}

public enum PlaceableObjectType
{
    Utility,
    Decoration
}
