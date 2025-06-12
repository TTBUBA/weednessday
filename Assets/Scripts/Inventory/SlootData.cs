using System;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SlootData", menuName = "Scriptable Objects/SlootData")]
public class SlootData : ScriptableObject
{
    public string NameTools;
    public Sprite ToolsImages;
    public int ToolsID;
    public int MaxStorage;
    public ItemType itemType;
}

public enum ItemType
{
    Seed,
    Static
}