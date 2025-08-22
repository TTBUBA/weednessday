using System.Collections;
using System.Linq;
using TMPro;
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

    private Coroutine coroutineDesiccator;
    [SerializeField] private InventoryManager InventoryManager;

    void Start()
    {
        coroutineDesiccator = StartCoroutine(ActiveDesiccator());
        SlootWeedDried.iconTools.gameObject.SetActive(false);

    }
    public void OpenEsiccator()
    {
        if (!IsOpen && InCollision)
        {
            IsOpen = true;
            InventoryManager.ActiveInventory = false;
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

    public void CloseDesiccator()
    {
        if (IsOpen && InCollision)
        {
            IsOpen = false;
            InventoryManager.ActiveInventory = true;
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
