using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestSystem : MonoBehaviour
{
    [SerializeField] private List<SlootManager> SlootBarrel = new List<SlootManager>();


    [SerializeField] private GameObject PanelSloot;
    [SerializeField] private GameObject butt_panel;
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
        }
    }

    private void ClosePanel(InputAction.CallbackContext context)
    {
        if (IsOpen)
        {
            PanelSloot.SetActive(false);
            text_panel.text = "Press 'E'";
        }
    }

    public void SetCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }
}
