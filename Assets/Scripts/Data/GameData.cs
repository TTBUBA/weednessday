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

    public bool TutorialCompleted;
    public bool PlayFirstTime;
    public bool UseHoeFirstTime;
    public bool UseWaterCanFirstTime;
    public bool UseBacketFirstTime;
    public bool PlantFirstTime;

    //Inventory Data
    public List<SlootSaveData> slootSlots = new List<SlootSaveData>();

    //Placement Data
    public List<ObjectPlacement> objectPlacements = new List<ObjectPlacement>();

    //Plant Data
    public List<PlantSaveData> plantDatas = new List<PlantSaveData>();

    //Time Data
    public float currentHour;
    public float currentMinute;
    public int currentDay;
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

[System.Serializable]
public class ObjectPlacement
{
    public GameObject ObjSpawn;
    public Vector3Int CellPos;
    public int occupiedAreaX;
    public int occupiedAreaY;
    public PlaceableObjectData placeableObjectData;
}
[System.Serializable]
public class PlantSaveData
{
    public Plant CurrentPlant;
    public Sprite[] plants;
    public Vector3Int CellPos;
    public float time;
    public int CurrentIndex;
    public bool FinishGrowth;
    public bool IsWet;
    public PlantManager.TerrainState StateTerrain;
}


