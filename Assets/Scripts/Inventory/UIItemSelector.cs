using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemSelector : MonoBehaviour
{
    public SlootManager draggedSlotController = null; // Slot "in mano"

    public void DragItemController()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        Debug.Log("Selected: " + selected);
        if (selected == null) return;

        SlootManager targetSlot = selected.GetComponent<SlootManager>();
        if (targetSlot == null || targetSlot.slootData == null) return;

        // Prendi l'oggetto
        draggedSlotController = targetSlot;

    }

    public void DropItemController()
    {
        if (draggedSlotController == null) return;

        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null) return;

        SlootManager dropTarget = selected.GetComponent<SlootManager>();
        if (dropTarget == null || dropTarget == draggedSlotController) return;

        // Esegui il trasferimento
        InventoryManager.Instance.DropItem(draggedSlotController, dropTarget);

        draggedSlotController = null;
    }
}
