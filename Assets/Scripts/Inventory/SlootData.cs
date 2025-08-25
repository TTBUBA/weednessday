using System;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SlootData", menuName = "Scriptable Objects/SlootData")]
public class SlootData : ScriptableObject
{
    public string NameTools;
    public Sprite ToolsImages;
    public int MaxStorage;
    public ItemType itemType;
    public GunData gunData; // Reference to GunData if this SlootData is a gun
}

public enum ItemType
{
    singleItem,
    MultiplyItem
}