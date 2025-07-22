using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class ChestSystem : MonoBehaviour
{
    [SerializeField] private Sprite ChestOpen;
    [SerializeField] private Sprite chestClose;

    [SerializeField] private GameObject PanelSloot;
    [SerializeField] private GameObject butt_panel;
    [SerializeField] private GameObject PanelInventoryPlayer;
    [SerializeField] private TextMeshProUGUI text_panel;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private Canvas canvas;
    [SerializeField] private InputActionReference Butt_OpenPanel;
    [SerializeField] private InputActionReference Butt_ClosePanel;


    [SerializeField] private InventoryManager InventoryManager;

    private void OnEnable()
    {
        Butt_OpenPanel.action.Enable();
        Butt_OpenPanel.action.performed += OpenPanel;

        Butt_ClosePanel.action.Enable();
        Butt_ClosePanel.action.performed += ClosePanel;
    }
    private void OnDisable()
    {
        Butt_OpenPanel.action.Disable();
        Butt_OpenPanel.action.performed -= OpenPanel;

        Butt_ClosePanel.action.Disable();
        Butt_ClosePanel.action.performed -= ClosePanel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           butt_panel.SetActive(true);
           text_panel.text = "Press 'E'";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelSloot.SetActive(false);
            butt_panel.SetActive(false);
            text_panel.text = "Press 'E'";
        }
    }

    private void OpenPanel(InputAction.CallbackContext context)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            PanelSloot.SetActive(true);
            text_panel.text = "Press 'Q'";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ChestOpen;

            foreach(var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                if (!slot.InUse)
                {
                    slot.InUse = true;
                    slot.transform.SetParent(PanelInventoryPlayer.transform, false);
                }
            }
        }
    }
    private void ClosePanel(InputAction.CallbackContext context)
    {
        if (IsOpen)
        {
            IsOpen = false;
            PanelSloot.SetActive(false);
            text_panel.text = "Press 'E'";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = chestClose;
            foreach (var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                if (slot.InUse)
                {
                    slot.transform.SetParent(PanelInventoryPlayer.transform, false);
                    slot.InUse = false;
                }
            }
        }
    }

    // setdata camera & set inventorymanager
    public void SetData(Camera camera , InventoryManager inventoryManager )
    {
        canvas.worldCamera = camera;
        InventoryManager = inventoryManager;
    }
}
