using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Player Data
    public int health;
    public int TotalMoney;
    public Vector3Int positionCurrentCell;
    public Vector3 PosPlayer;

    //Inventory Data
    public List<SlootSaveData> slootSlots = new List<SlootSaveData>();

}
[System.Serializable]
public class SlootSaveData
{
    public SlootManager SlootManager;
    public SlootData slootData;
    public int CurrentStorage;
    public bool StorageFull = false;
    public bool IsStatic;
    public bool InUse;
    public GunData gunData;
}

