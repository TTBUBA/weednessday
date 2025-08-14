using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Scriptable Objects/NpcData")]
public class NpcData : ScriptableObject
{
    public string NameNpc;
    public Sprite NpcImage;
    public Sprite IconNpc;
    public float TotalWeedAssuming;
    public NpcName Name;
    public NpcType Type;
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
public enum NpcType
{
    Customer,
    Dealer,
    Delivery
}
