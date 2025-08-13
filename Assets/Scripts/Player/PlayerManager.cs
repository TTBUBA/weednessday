using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int CurrentMoney;

    [Header("System Drog")]
    public List<Image> ImageEye;
    public int TotalCaneSmoke;
    public bool ActiveButtun = true;

    public InventoryManager InventoryManager;
    public EffectWeed EffectWeed;

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
}
