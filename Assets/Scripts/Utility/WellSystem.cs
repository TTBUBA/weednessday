using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class  WellSystem : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private GameObject PanelBarWater;
    [SerializeField] private GameObject ButtOpenPanelWater;
    [SerializeField] private Image barWater;
    [SerializeField] private TextMeshProUGUI Text_Button;

    [Header("Settings")]
    [SerializeField] private int LevelWater;
    [SerializeField] private float TimeCharge;
    [SerializeField] private InventoryManager InventoryManager;
    [SerializeField] private WateringCan WateringCan;
    [SerializeField] private bool IsCollision;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference OpenPanelWater;
    [SerializeField] private InputActionReference ClosePanelWater;
    [SerializeField] private Canvas canvas;
    private void Awake()
    {
        barWater.fillAmount = LevelWater;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollision = true;
            ButtOpenPanelWater.SetActive(true);
            LevelWater = WateringCan.waterAmount;
            barWater.fillAmount = LevelWater / 100f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollision = false;
            PanelBarWater.SetActive(false);
            ButtOpenPanelWater.SetActive(false);
        }
    }

    public void Open()
    {
        if (IsCollision)
        {
            PanelBarWater.SetActive(true);
            Text_Button.text = "Close Q";
        }
    }

    public void Close()
    {
        if (IsCollision)
        {
            ButtOpenPanelWater.SetActive(false);
            PanelBarWater.SetActive(false);
            Text_Button.text = "Open E";
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
            if (WateringCan.waterAmount >= 100f) { yield break; } // Exit the coroutine if the water can is full
            yield return new WaitForSeconds(TimeCharge);
            LevelWater = WateringCan.waterAmount;
            barWater.fillAmount = LevelWater / 100f;
            WateringCan.Fill(5);
            WateringCan.Text_CurrentWater.text = WateringCan.waterAmount.ToString() + "%";
        }
    }

    public void SetData(Camera camera, InventoryManager inventory, WateringCan wateringCan)
    {
        canvas.worldCamera = camera;
        InventoryManager = inventory;
        WateringCan = wateringCan;
    }
}
