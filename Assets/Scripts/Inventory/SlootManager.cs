using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SlootManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public SlootData slootData;
    public int CurrentStorage;
    public bool StorageFull = false;
    public Image iconTools;
    public TextMeshProUGUI CountText;


    private Vector3 iconOriginalPosition;
    private bool isDragging = false;

    private void Awake()
    {
        UpdateSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragging)
        {
            InventoryManager.Instance.CurrentSlotSelect = slootData;
            //AnimationSlotEnter();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging)
        {
            InventoryManager.Instance.CurrentSlotSelect = null;
            InventoryManager.Instance.IdSlotCurrent = 0;
            //AnimationSlotExit();
        }
    }

    private void AnimationSlotEnter()
    {
        transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
    }

    private void AnimationSlotExit()
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slootData == null) return;

        isDragging = true;

        //save the position Original
        iconOriginalPosition = iconTools.transform.position;

        iconTools.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        Vector3 mousePos = Input.mousePosition;
        iconTools.transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        isDragging = false;

        iconTools.transform.position = iconOriginalPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        SlootManager draggedSlot = eventData.pointerDrag.GetComponent<SlootManager>();

        if (draggedSlot != null && draggedSlot != this && draggedSlot.slootData != null)
        {
            InventoryManager.Instance.DropItem(this, draggedSlot);
        }
    }

    public void UpdateSlot()
    {
        if (slootData != null)
        {
            iconTools.sprite = slootData.ToolsImages;
            CountText.text = CurrentStorage > 0 ? CurrentStorage.ToString() : string.Empty;
            iconTools.color = Color.white;
        }
        else
        {
            iconTools.sprite = null;
            CountText.text = string.Empty;
            iconTools.color = Color.clear;
        }
    }
}