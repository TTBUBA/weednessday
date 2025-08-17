using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PackerSystem : MonoBehaviour
{
    [Header("Desiccator")]
    [SerializeField] private SlootManager slootPlastic;
    [SerializeField] private SlootManager SlootBattery;
    [SerializeField] private SlootManager SlootZiploc;
    [SerializeField] private int TimePacker;



    [Header("Ui")]
    [SerializeField] private GameObject PanelDesiccator;
    [SerializeField] private GameObject Button;
    [SerializeField] private TextMeshProUGUI Text_Button;
    [SerializeField] private GameObject PanelInventoryPlayer;
    [SerializeField] private Image BarProgress;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private bool ActiveLight;
    [SerializeField] private bool InCollision;
    [SerializeField] private Canvas canvas;

    [Header("Input")]
    [SerializeField] private InputActionReference Butt_OpenPacker;
    [SerializeField] private InputActionReference Butt_ClosePacker;


    private Coroutine coroutinePacker;
    [SerializeField] private InventoryManager InventoryManager;

    void Start()
    {
        coroutinePacker = StartCoroutine(ActivePacker());
        SlootZiploc.iconTools.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        Butt_OpenPacker.action.Enable();
        Butt_ClosePacker.action.Enable();
        Butt_OpenPacker.action.performed += OpenClosePacker;
        Butt_ClosePacker.action.performed += ClosePacker;
    }

    private void OnDisable()
    {
        Butt_OpenPacker.action.Disable();
        Butt_ClosePacker.action.Disable();
        Butt_OpenPacker.action.performed -= OpenClosePacker;
        Butt_ClosePacker.action.performed -= ClosePacker;
    }

    private void OpenClosePacker(InputAction.CallbackContext context)
    {
        if (!IsOpen && InCollision)
        {
            IsOpen = true;

            foreach (var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                if (!slot.InUse)
                {
                    slot.InUse = true;
                    slot.transform.SetParent(PanelInventoryPlayer.transform, false);
                }
            }

            PanelDesiccator.SetActive(true);
            Text_Button.text = "Press 'Q'";
        }
    }

    private void ClosePacker(InputAction.CallbackContext context)
    {
        if (IsOpen && InCollision)
        {
            IsOpen = false;

            foreach (var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                slot.transform.SetParent(InventoryManager.PanelInventory.transform, false);
                slot.InUse = false;
            }

            PanelDesiccator.SetActive(false);
            Text_Button.text = "Press 'E'";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InCollision = true;
            Button.SetActive(true);
            Text_Button.text = "Press 'E'";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InCollision = false;
            Button.SetActive(false);
            Text_Button.text = "Press 'E'";
        }
    }

    public void ButtWithonpacker()
    {
        if (SlootZiploc.CurrentStorage >= 1 && slootPlastic.slootData.NameTools == "plastic" && SlootBattery.slootData.NameTools == "battery")
        {
            InventoryManager.AddItem(SlootZiploc.slootData, SlootZiploc.CurrentStorage);
            SlootZiploc.CurrentStorage = 0;
            SlootZiploc.UpdateSlot();
            SlootZiploc.iconTools.gameObject.SetActive(false);
        }
    }
    IEnumerator ActivePacker()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimePacker);
            if (slootPlastic.slootData != null && SlootBattery.slootData != null && slootPlastic.slootData.NameTools == "plastic" && SlootBattery.slootData.NameTools == "battery")
            {
                BarProgress.fillAmount += 0.1f;

                if (BarProgress.fillAmount >= 1f && slootPlastic.CurrentStorage > 0 && SlootBattery.CurrentStorage > 0 && SlootZiploc.CurrentStorage <= SlootZiploc.slootData.MaxStorage - 1)
                {
                    SlootZiploc.iconTools.gameObject.SetActive(true);
                    SlootZiploc.CurrentStorage++;
                    SlootZiploc.UpdateSlot();
                    BarProgress.fillAmount = 0f;
                    slootPlastic.CurrentStorage--;
                    SlootBattery.CurrentStorage--;
                    slootPlastic.UpdateSlot();
                    SlootBattery.UpdateSlot();

                    if (slootPlastic.CurrentStorage < 1)
                    {
                        slootPlastic.CurrentStorage = 0;
                        slootPlastic.iconTools.enabled = false;
                    }

                    if (SlootBattery.CurrentStorage < 1)
                    {
                        SlootBattery.CurrentStorage = 0;
                        SlootBattery.iconTools.enabled = false;
                    }

                }

            }
        }
    }
    public void SetData(Camera camera, InventoryManager inventoryManager)
    {
        canvas.worldCamera = camera;
        InventoryManager = inventoryManager;
    }
}
