using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PackingWeedSystem : MonoBehaviour
{
    [SerializeField] private SlootManager SlootDataWeed;
    [SerializeField] private SlootManager SlootDataBaggie;
    [SerializeField] private SlootManager SlootDataBaggieWeed;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private bool InCollision;
    [SerializeField] private bool ActivePack;

    [Header("Ui")]
    [SerializeField] private GameObject ContainerSlootPlayer;
    [SerializeField] private GameObject ContainerSlootBaggie;
    [SerializeField] private GameObject PanelPackingSystem;
    [SerializeField] private Image BarProgress;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject ButtonPacking;
    [SerializeField] private TextMeshProUGUI Text_Button;


    [Header("Input")]
    [SerializeField] private InputActionReference OpenPackingSystem;
    [SerializeField] private InputActionReference ClosePackingSystem;


    private Coroutine coroutinePacking;
    [SerializeField] private InventoryManager InventoryManager;

    private void Start()
    {
        //coroutinePacking = StartCoroutine(ActivePackingSystem());
        SlootDataBaggieWeed.iconTools.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        OpenPackingSystem.action.Enable();
        ClosePackingSystem.action.Enable();
        OpenPackingSystem.action.performed += OnOpenPackingSystem;
        ClosePackingSystem.action.performed += OnClosePackingSystem;
    }

    private void OnDisable()
    {
        OpenPackingSystem.action.Disable();
        ClosePackingSystem.action.Disable();
        OpenPackingSystem.action.performed -= OnOpenPackingSystem;
        ClosePackingSystem.action.performed -= OnClosePackingSystem;
    }
    private void OnOpenPackingSystem(InputAction.CallbackContext context)
    {
        if (!IsOpen && InCollision)
        {
            IsOpen = true;

            foreach (var sloot in InventoryManager.slootManager.Skip(5).ToList())
            {
                if (!sloot.InUse)
                {
                    sloot.transform.SetParent(ContainerSlootPlayer.transform, false);
                    sloot.InUse = true;
                }
            }
            PanelPackingSystem.SetActive(true);
            Text_Button.text = "Press Q";

        }
    }
    private void OnClosePackingSystem(InputAction.CallbackContext context)
    {
        if (IsOpen && InCollision)
        {
            foreach (var sloot in InventoryManager.slootManager.Skip(5).ToList())
            {
                if (sloot.InUse)
                {
                    sloot.transform.SetParent(ContainerSlootPlayer.transform, false);
                    sloot.InUse = false;
                }
            }
            PanelPackingSystem.SetActive(false);
            Text_Button.text = "Press E";
            IsOpen = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InCollision = true;
            Text_Button.text = "Press E";
            ButtonPacking.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InCollision = false;
            Text_Button.text = "Press E";
            ButtonPacking.SetActive(false);
        }
    }
    public void ButtActivePacking()
    {
        ActivePack = true;
        StartCoroutine(ActivePackingSystem());
    }

    IEnumerator ActivePackingSystem()
    {
        while (ActivePack)
        {
            yield return new WaitForSeconds(1f);
            if (SlootDataWeed != null && SlootDataBaggie != null && SlootDataWeed.slootData.NameTools == "Weed" && SlootDataBaggie.slootData.NameTools == "baggie")
            {
                BarProgress.fillAmount += 0.1f;
                if (BarProgress.fillAmount >= 1f && SlootDataWeed.CurrentStorage > 0 && SlootDataBaggie.CurrentStorage > 0 && SlootDataBaggieWeed.CurrentStorage <= SlootDataBaggieWeed.slootData.MaxStorage)
                {
                    SlootDataBaggieWeed.iconTools.gameObject.SetActive(true);
                    SlootDataBaggie.CurrentStorage--;
                    SlootDataWeed.CurrentStorage--;
                    SlootDataBaggieWeed.CurrentStorage++;
                    SlootDataBaggieWeed.UpdateSlot();
                    SlootDataWeed.UpdateSlot();
                    SlootDataBaggie.UpdateSlot();
                    BarProgress.fillAmount = 0f;
                    ActivePack = false;

                    if (SlootDataWeed.CurrentStorage < 1)
                    {
                        SlootDataWeed.CurrentStorage = 0;
                        SlootDataWeed.iconTools.gameObject.SetActive(false);
                    }

                    if (SlootDataWeed.CurrentStorage < 1)
                    {
                        SlootDataBaggie.CurrentStorage = 0;
                        SlootDataBaggie.iconTools.gameObject.SetActive(false);
                    }
                }
            }
        }   
    }

    public void SetData(Camera camera ,InventoryManager inventoryManager)
    {
        canvas.worldCamera = camera;
        InventoryManager = inventoryManager;
    }
}
