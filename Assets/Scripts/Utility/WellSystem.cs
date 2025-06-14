using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class WellSystem : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private GameObject PanelBarWater;
    [SerializeField] private Image barWater;
    [SerializeField] private Button Butt_Refill;

    [Header("Settings")]
    [SerializeField] private float LevelWater;
    [SerializeField] private float TimeCharge;
    [SerializeField] private InventoryManager InventoryManager;
    [SerializeField] private WateringCan WateringCan;



    [SerializeField] private Canvas canvas;

    private SlootData slootData;
    private bool IsCollision;

    private void Awake()
    {
        Butt_Refill.onClick.AddListener(ChargeWaterCan);
        barWater.fillAmount = LevelWater;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(InventoryManager.CurrentSlotSelect.NameTools == "WateringCan")
            {
                PanelBarWater.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelBarWater.SetActive(false);
            IsCollision = false;
        }
    }
    private void ChargeWaterCan()
    {
        StartCoroutine(ChargeWater());
    }
    IEnumerator ChargeWater()
    {
        while (true)
        {
            if (WateringCan.waterAmount >= 1f) { yield break; } // Exit the coroutine if the water can is full
            yield return new WaitForSeconds(TimeCharge);;
            WateringCan.Fill(0.1f);
            barWater.fillAmount = WateringCan.waterAmount;
        }
    }

    public void SetData(Camera camera, SlootData slootData, WateringCan wateringCan)
    {
        canvas.worldCamera = camera;
        InventoryManager.CurrentSlotSelect = slootData;
        WateringCan = wateringCan;
    }
}
