using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestSystem : MonoBehaviour
{
    [SerializeField] private List<SlootManager> sloolTest = new List<SlootManager>();
    [SerializeField] private List<SlootManager> slootPlayer = new List<SlootManager>();
    [SerializeField] private List<SlootManager> SlootChest = new List<SlootManager>();


    [SerializeField] private Sprite ChestOpen;
    [SerializeField] private Sprite chestClose;

    [SerializeField] private GameObject PanelSloot;
    [SerializeField] private GameObject butt_panel;
    [SerializeField] private TextMeshProUGUI text_panel;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private Canvas canvas;
    [SerializeField] private InputActionReference Butt_OpenPanel;
    [SerializeField] private InputActionReference Butt_ClosePanel;
    [SerializeField] private InventoryManager InventoryManager;

    private void Start()
    {

        //esclude the first 2 slots which are reserved for the player
        if (InventoryManager != null)
        {
            slootPlayer = InventoryManager.slootManager.Skip(2).ToList();
        }
    }

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
           IsOpen = true;
           butt_panel.SetActive(true);
           text_panel.text = "Press 'E'";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsOpen = false;
            PanelSloot.SetActive(false);
            butt_panel.SetActive(false);
            text_panel.text = "Press 'E'";
        }
    }

    private void OpenPanel(InputAction.CallbackContext context)
    {
        if (IsOpen)
        {
            PanelSloot.SetActive(true);
            text_panel.text = "Press 'Q'";
            SetSlotChest();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ChestOpen;
        }
    }

    private void ClosePanel(InputAction.CallbackContext context)
    {
        if (IsOpen)
        {
            PanelSloot.SetActive(false);
            text_panel.text = "Press 'E'";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = chestClose;
        }
    }

    // This method sets the sloot data for each sloot in the slootPlayer list
    public void SetSlotChest()
    {
        foreach (var sloot in slootPlayer)
        {
            for (int i = 0; i < SlootChest.Count; i++)
            {
                sloot.slootData = SlootChest[i].slootData;
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
