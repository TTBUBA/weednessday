using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableObjectData", menuName = "Scriptable Objects/PlaceableObjectData")]
public class PlaceableObjectData : ScriptableObject
{
    public string UtilityName;
    public string UtilityDescription;
    public Sprite UtilityIcon;
    public int UtilityID;
    public int UtilityCost;
    public RuleTile Object;

}
