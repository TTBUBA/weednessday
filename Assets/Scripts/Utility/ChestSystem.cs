using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ChestSystem : MonoBehaviour
{
    [SerializeField] private Sprite ChestOpen;
    [SerializeField] private Sprite chestClose;

    [SerializeField] private GameObject PanelSloot;
    [SerializeField] private GameObject butt_panel;
    [SerializeField] private GameObject PanelInventoryPlayer;
    [SerializeField] private TextMeshProUGUI text_panel;
    [SerializeField] private bool IsOpen = false;
    [SerializeField] private bool Iscollision = false;
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<SlootManager> SlootChest;
    [SerializeField] private InventoryManager InventoryManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            butt_panel.SetActive(true);
            text_panel.text = "Press 'E'";
            Iscollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelSloot.SetActive(false);
            butt_panel.SetActive(false);
            text_panel.text = "Press 'E'";
            Iscollision = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = chestClose;
        }
    }

    public void OpenPanel()
    {
        if (!IsOpen && Iscollision)
        {
            InventoryManager.ActiveInventory = false;// disable inventory when open chest
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

                    //Input Controller set navigation for slots
                    foreach (var sloot in SlootChest)
                    {
                        switch (slot.Index)
                        {
                            case 8:
                                var slotNav = slot.GetComponent<Selectable>().navigation;
                                var SlotChest = SlootChest[0].GetComponent<Selectable>();
                                slotNav.selectOnRight = SlotChest;
                                slot.GetComponent<Selectable>().navigation = slotNav;

                                var trashNav = SlotChest.navigation;
                                trashNav.selectOnLeft = slot.GetComponent<Selectable>();
                                SlotChest.navigation = trashNav;
                                break;
                            case 12:
                                var slotNav2 = slot.GetComponent<Selectable>().navigation;
                                var SlotChest2 = SlootChest[2].GetComponent<Selectable>();
                                slotNav2.selectOnRight = SlotChest2;
                                slot.GetComponent<Selectable>().navigation = slotNav2;

                                var trashNav2 = SlotChest2.navigation;
                                trashNav2.selectOnLeft = slot.GetComponent<Selectable>();
                                SlotChest2.navigation = trashNav2;
                                break;
                            case 16:
                                var slotNav3 = slot.GetComponent<Selectable>().navigation;
                                var SlotChest3 = SlootChest[4].GetComponent<Selectable>();
                                slotNav3.selectOnRight = SlotChest3;
                                slot.GetComponent<Selectable>().navigation = slotNav3;

                                var trashNav3 = SlotChest3.navigation;
                                trashNav3.selectOnLeft = slot.GetComponent<Selectable>();
                                SlotChest3.navigation = trashNav3;
                                break;
                        }
                    }
                }
            }
        }
    }
    public void ClosePanel()
    {
        if (IsOpen && Iscollision)
        {
            InventoryManager.ActiveInventory = true;// active inventory when close chest
            IsOpen = false;
            PanelSloot.SetActive(false);
            text_panel.text = "Press 'E'";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = chestClose;
            foreach (var slot in InventoryManager.slootManager.Skip(5).ToList())
            {
                if (slot.InUse)
                {
                    slot.transform.SetParent(InventoryManager.PanelInventory.transform, false);
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
