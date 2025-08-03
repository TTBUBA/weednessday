using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class  WellSystem : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private GameObject PanelBarWater;
    [SerializeField] private GameObject ButtOpenPanelWater;
    [SerializeField] private Image barWater;
    [SerializeField] private Button Butt_Refill;

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
        Butt_Refill.onClick.AddListener(ChargeWaterCan);
        barWater.fillAmount = LevelWater;
    }

    private void OnEnable()
    {
        OpenPanelWater.action.Enable();
        ClosePanelWater.action.Enable();
        OpenPanelWater.action.performed += OpenPanelWaterAction;
        ClosePanelWater.action.performed += ClosePanelWaterAction;
    }

    private void OnDisable()
    {
        OpenPanelWater.action.Disable();
        ClosePanelWater.action.Disable();
        OpenPanelWater.action.performed -= OpenPanelWaterAction;
        ClosePanelWater.action.performed -= ClosePanelWaterAction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollision = true;
            ButtOpenPanelWater.SetActive(true);
            LevelWater = WateringCan.waterAmount;
            barWater.fillAmount = LevelWater / 100f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollision = false;
            PanelBarWater.SetActive(false);
            ButtOpenPanelWater.SetActive(false);
        }
    }

    private void OpenPanelWaterAction(InputAction.CallbackContext context)
    {
        if (IsCollision)
        {
            PanelBarWater.SetActive(true);
            ButtOpenPanelWater.SetActive(false);
        }
    }
    private void ClosePanelWaterAction(InputAction.CallbackContext context)
    {
        if (IsCollision)
        {
            ButtOpenPanelWater.SetActive(false);
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
