using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TrashCompactor : MonoBehaviour
{
    [Header("Trash Compactor")]
    [SerializeField] private SlootManager CurrentData;
    [SerializeField] private SlootManager slootTrash;
    [SerializeField] private float TimeToCompact = 1f;
    [SerializeField] private Light2D LightOn;
    [SerializeField] private Light2D LightOff;
    [SerializeField] private GameObject slootManagerPrefab;
    [SerializeField] private Selectable TrashSlot;

    [Header("Input")]
    [SerializeField] private InputActionReference Butt_OpenTrashCompactor;
    [SerializeField] private InputActionReference Butt_CloseTrashCompactor;

    [Header("Ui")]
    [SerializeField] private GameObject PanelTrashCompactor;
    [SerializeField] private GameObject Button;
    [SerializeField] private TextMeshProUGUI Text_Button;
    [SerializeField] private GameObject PanelInventoryPlayer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image BarProgress;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private bool ActiveLight;
    [SerializeField] private bool InCollision;
    private Coroutine coroutineTrash;

    public InventoryManager InventoryManager;

    private void Start()
    {
        coroutineTrash = StartCoroutine(ActiveTrashCompactor());
        slootTrash.iconTools.enabled = false;
    }

    private void OnEnable()
    {
        Butt_OpenTrashCompactor.action.Enable();
        Butt_OpenTrashCompactor.action.performed += OpenTrashCompactor;
        Butt_CloseTrashCompactor.action.Enable();
        Butt_CloseTrashCompactor.action.performed += CloseTrashCompactor;
    }

    private void OnDisable()
    {
        Butt_OpenTrashCompactor.action.Disable();
        Butt_OpenTrashCompactor.action.performed -= OpenTrashCompactor;
        Butt_CloseTrashCompactor.action.Disable();
        Butt_CloseTrashCompactor.action.performed -= CloseTrashCompactor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Button.SetActive(true);
            Text_Button.text = "Press 'E'";
            InCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Button.SetActive(false);
            PanelTrashCompactor.SetActive(false);
            Text_Button.text = "Press 'E'";
            InCollision = false;
            IsOpen = false;
        }
    }

    private void OpenTrashCompactor(InputAction.CallbackContext context)
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

                    switch (slot.Index)
                    {
                        case 8:
                        case 12:
                        case 16:
                            // Collega lo slot al TrashSlot (destra)
                            var slotNav = slot.GetComponent<Selectable>().navigation;
                            slotNav.selectOnRight = TrashSlot;
                            slot.GetComponent<Selectable>().navigation = slotNav;

                            var trashNav = TrashSlot.navigation;
                            trashNav.selectOnLeft = slot.GetComponent<Selectable>();
                            TrashSlot.navigation = trashNav;
                            break;
                    }

                }
            }

            PanelTrashCompactor.SetActive(true);
            Text_Button.text = "Press 'Q'";
        }
    }

    private void CloseTrashCompactor(InputAction.CallbackContext context)
    {
        if (IsOpen && InCollision) 
        {
            IsOpen = false;

            foreach (var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                slot.transform.SetParent(InventoryManager.PanelInventory.transform, false);
                slot.InUse = false;

                var selectable = slot.GetComponent<Selectable>().navigation;
                selectable.selectOnUp = null;
                slot.GetComponent<Selectable>().navigation = selectable;
            }

            PanelTrashCompactor.SetActive(false);
            Text_Button.text = "Press 'E'";
        }
    }


    //active the button and add item to the inventory
    public void CompactTrash()
    {
        if (CurrentData.slootData != null && CurrentData.slootData.NameTools == "plastic")
        {
            InventoryManager.AddItem(slootTrash.slootData, slootTrash.CurrentStorage);
            slootTrash.CurrentStorage = 0;
            slootTrash.iconTools.enabled = false;
        }
    }

    private IEnumerator ActiveTrashCompactor()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToCompact);
            if (CurrentData.slootData != null && CurrentData.slootData.NameTools == "plastic")
            {
                BarProgress.fillAmount += 0.1f;
                slootTrash.iconTools.enabled = true;

                // Check if the bar is full and if there is space in the trash compactor
                if (BarProgress.fillAmount >= 1f && CurrentData.CurrentStorage > 0 && slootTrash.CurrentStorage <= slootTrash.slootData.MaxStorage - 1)
                {
                    CurrentData.CurrentStorage--;
                    slootTrash.CurrentStorage++;
                    slootTrash.UpdateSlot();
                    CurrentData.UpdateSlot();
                    BarProgress.fillAmount = 0f;
                    LightOn.enabled = true;
                    ActiveLight = false;

                    //reset data when storage is empty 
                    if (CurrentData.CurrentStorage < 1)
                    {
                        ActiveLight = true;
                        CurrentData.StorageFull = false;
                        CurrentData.slootData = null;
                        CurrentData.iconTools.enabled = false;
                        LightOff.enabled = false;
                    }
                }
            }
            else if(CurrentData.slootData != null && slootTrash.CurrentStorage <= 1)
            {
                LightOn.enabled = false;
                LightOff.enabled = true;
            }
        }
    }

    //Animation of the lights on the boat
    IEnumerator AnimationLight()
    {
        while (ActiveLight)
        {
            yield return new WaitForSeconds(0.5f);
            LightOn.enabled = true;
            yield return new WaitForSeconds(0.5f);
            LightOn.enabled = false;
        }
    }
    //set data
    public void SetData(Camera camera, InventoryManager inventoryManager)
    {
        canvas.worldCamera = camera;
        InventoryManager = inventoryManager;
    }
}
