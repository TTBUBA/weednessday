using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Desiccator : MonoBehaviour
{
    [Header("Desiccator")]
    [SerializeField] private SlootManager slootWeed;
    [SerializeField] private SlootManager SlootBattery;
    [SerializeField] private SlootManager SlootWeedDried;
    [SerializeField] private int TimeDesiccator;
    [SerializeField] private Sprite DesiccatorFull;
    [SerializeField] private Sprite DesiccatorEmpty;


    [Header("Ui")]
    [SerializeField] private GameObject PanelPacker;
    [SerializeField] private GameObject Button;
    [SerializeField] private TextMeshProUGUI Text_Button;
    [SerializeField] private GameObject PanelInventoryPlayer;
    [SerializeField] private Image BarProgress;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private bool ActiveLight;
    [SerializeField] private bool InCollision;
    [SerializeField] private Canvas canvas;

    [Header("Input")]
    [SerializeField] private InputActionReference Butt_OpenDesiccator;
    [SerializeField] private InputActionReference Butt_CloseDesiccator;


    private Coroutine coroutineDesiccator;
    [SerializeField] private InventoryManager InventoryManager;

    void Start()
    {
        coroutineDesiccator = StartCoroutine(ActiveDesiccator());
        SlootWeedDried.iconTools.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        Butt_OpenDesiccator.action.Enable();
        Butt_CloseDesiccator.action.Enable();
        Butt_OpenDesiccator.action.performed += OpenCloseDesiccator;
        Butt_CloseDesiccator.action.performed += CloseDesiccator;
    }

    private void OnDisable()
    {
        Butt_OpenDesiccator.action.Disable();
        Butt_CloseDesiccator.action.Disable();
        Butt_OpenDesiccator.action.performed -= OpenCloseDesiccator;
        Butt_CloseDesiccator.action.performed -= CloseDesiccator;
    }

    private void OpenCloseDesiccator(InputAction.CallbackContext context)
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

            PanelPacker.SetActive(true);
            Text_Button.text = "Press 'Q'";
        }
    }

    private void CloseDesiccator(InputAction.CallbackContext context)
    {
        if (IsOpen && InCollision)
        {
            IsOpen = false;

            foreach (var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                slot.transform.SetParent(InventoryManager.PanelInventory.transform, false);
                slot.InUse = false;
            }

            PanelPacker.SetActive(false);
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

    public void ButtWithonDesiccator()
    {
        if (slootWeed.slootData.NameTools == "Weed" && SlootBattery.slootData.NameTools == "carbon")
        {
            InventoryManager.AddItem(SlootWeedDried.slootData, SlootWeedDried.CurrentStorage);
            SlootWeedDried.CurrentStorage = 0;
            SlootWeedDried.iconTools.gameObject.SetActive(false);
        }
    }
    IEnumerator ActiveDesiccator()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeDesiccator);
            if (slootWeed.slootData != null && SlootBattery.slootData != null && slootWeed.slootData.NameTools == "Weed" && SlootBattery.slootData.NameTools == "battery")
            {

                BarProgress.fillAmount += 0.1f;
                this.gameObject.GetComponent<SpriteRenderer>().sprite = DesiccatorFull;

                if (BarProgress.fillAmount >= 1f && slootWeed.CurrentStorage > 0 && SlootBattery.CurrentStorage > 0 && SlootWeedDried.CurrentStorage <= SlootWeedDried.slootData.MaxStorage -1)
                {
                    SlootWeedDried.iconTools.gameObject.SetActive(true);
                    SlootWeedDried.CurrentStorage++;
                    SlootWeedDried.UpdateSlot();
                    BarProgress.fillAmount = 0f;
                    slootWeed.CurrentStorage--;
                    SlootBattery.CurrentStorage--;
                    slootWeed.UpdateSlot();
                    SlootBattery.UpdateSlot();

                    if(slootWeed.CurrentStorage < 1)
                    {
                        slootWeed.CurrentStorage = 0;
                        slootWeed.StorageFull = true;
                        slootWeed.iconTools.enabled = false;
                    }

                    if (SlootBattery.CurrentStorage < 1)
                    {
                        SlootBattery.CurrentStorage = 0;
                        SlootBattery.StorageFull = true;
                        SlootBattery.iconTools.enabled = false;
                    }

                }

            }
        }
    }
    public void SetData(Camera camera,InventoryManager inventoryManager)
    {
        canvas.worldCamera = camera;
        InventoryManager = inventoryManager;
    }
}
