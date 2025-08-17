using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Scriptable Objects/NpcData")]
public class NpcData : ScriptableObject
{
    public string NameNpc;
    public Sprite NpcImage;
    public Sprite IconNpc;
    public float TotalWeedAssuming;
    public float loyaltyNpc;//Probability of loyalty to the player
    public float AbilityNpc;//Ability to avoid police detection
    public bool IsHome;
    public bool IsArrested;
    public NpcName Name;
}

public enum NpcName
{
    Bonghilda,
    Erbinator,
    MrAlways,
    Lemon,
    Lady,
    Dopephano,
    Puff,
    Jointzilla,
    Captain,
    Mary,
    Crystal,
    Greenburn,
    Ganjalf,
    Cheech,
    Sativar
}
