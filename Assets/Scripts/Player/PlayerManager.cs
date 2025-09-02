using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, ISaveable
{
    public int CurrentMoney;

    [Header("System Drog")]
    public List<Image> ImageEye;
    public int TotalCaneSmoke;
    public bool ActiveButtun = true;

    public PlayerMovement PlayerMovement;
    public PlayerHealth PlayerHealth;
    public InventoryManager InventoryManager;
    public EffectWeed EffectWeed;

    private void Awake()
    {
       SaveSystem.Instance.saveables.Add(this);
    }

    public void UseWeed()
    {
        if(InventoryManager.CurrentSlotSelect != null && InventoryManager.CurrentSlotSelect.NameTools == "Cane" && EffectWeed.ActiveDrog && ActiveButtun)
        {
            InventoryManager.RemoveCane();
            ActiveButtun = false;
            EffectWeed.ActiveDrog = true;
            EffectWeed.ValueEffectchromatic = Mathf.Max(0.2f ,0.5f);
            EffectWeed.ValueEffectchromatic += 0.1f;
            TotalCaneSmoke++;
            StartCoroutine(EffectWeed.EffectCane());
            ImageEye[TotalCaneSmoke - 1].gameObject.SetActive(true);
        }
    }

    public void save(GameData data)
    {
        if(GameManager.Instance.TutorialCompleted == true)
        {
            data.health = PlayerHealth.health;
            data.TotalMoney = CurrentMoney;
            data.positionCurrentCell = PlayerMovement.CurrentPosCell;
            data.PosPlayer = PlayerMovement.PositionPlayer;
        }
    }

    public void load(GameData data)
    {
        if (GameManager.Instance.TutorialCompleted == true)
        {
            PlayerHealth.health = data.health;
            CurrentMoney = data.TotalMoney;
            PlayerMovement.CurrentPosCell = data.positionCurrentCell;
            PlayerMovement.PositionPlayer = data.PosPlayer;
        }
    }
}
