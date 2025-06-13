using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class barrelSystem : MonoBehaviour
{
    [SerializeField] private List<SlootManager> SlootBarrel = new List<SlootManager>();


    [SerializeField] private GameObject PanelSloot;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private Canvas canvas;
    [SerializeField] private InputActionReference Butt_OpenPanel;

    private void OnEnable()
    {
        Butt_OpenPanel.action.Enable();
        Butt_OpenPanel.action.performed += OpenPanel;
    }
    private void OnDisable()
    {
        Butt_OpenPanel.action.Disable();
        Butt_OpenPanel.action.performed -= OpenPanel;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           IsOpen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsOpen = false;
        }
    }

    private void OpenPanel(InputAction.CallbackContext context)
    {
        if (IsOpen)
        {
            PanelSloot.SetActive(true);
            Debug.Log("Open Panel Sloot");
        }
    }

    public void SetCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }
}
