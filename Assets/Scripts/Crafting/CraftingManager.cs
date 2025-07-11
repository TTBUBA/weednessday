using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class CraftingManager : MonoBehaviour
{
    public CraftingRecipe CurrentItemCraft;
    [SerializeField] private InventoryManager InventoryManager;
    [SerializeField] private Canvas Canvas;

    [Header("UI Elements")]
    [SerializeField] private GameObject CraftingPanel;
    [SerializeField] private GameObject Butt_OpenPanelCrafting;
    [SerializeField] private TextMeshProUGUI Text_Panel;


    [Header("Input Action")]
    [SerializeField] private InputActionReference OpenCraftingPanel;
    [SerializeField] private InputActionReference CloseCraftingPanel;

    private void OnEnable()
    {
        OpenCraftingPanel.action.performed += OpenPanelCrafting;
        CloseCraftingPanel.action.performed += ClosePanelCrafting;
    }

    private void OnDisable()
    {
        OpenCraftingPanel.action.performed -= OpenPanelCrafting;
        CloseCraftingPanel.action.performed -= ClosePanelCrafting;
    }

    private void OpenPanelCrafting(InputAction.CallbackContext context)
    {
        CraftingPanel.SetActive(true);
        Text_Panel.text = "Press 'Q'";

    }

    private void ClosePanelCrafting(InputAction.CallbackContext context)
    {
        CraftingPanel.SetActive(false);
        Text_Panel.text = "Press 'E'";
    }

    public void Craft()
    {
        foreach (var Sloot in InventoryManager.slootManager)
        {
            foreach (var ingrediet in CurrentItemCraft.ingredients)
            {
                if(Sloot.slootData == ingrediet.slootData)
                {
                    Sloot.CurrentStorage -= ingrediet.amount;
                    Sloot.UpdateSlot();
                    InventoryManager.AddItem(CurrentItemCraft.resultItem, 1);
                    Debug.Log(Sloot.name + ingrediet.slootData.name);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Butt_OpenPanelCrafting.SetActive(true);
            Text_Panel.text = "Press 'E'";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CraftingPanel.SetActive(false);
            Butt_OpenPanelCrafting.SetActive(false);
            Text_Panel.text = "";
        }
    }

    public void SetData(Camera camera,InventoryManager inventoryManager)
    {
        Canvas.worldCamera = camera;
        InventoryManager = inventoryManager;
    }
}
