using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class SlootManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SlootData slootData;
    public bool IsStorageSlot = false;
    public Image iconTools;

    private void Awake()
    {
        iconTools.sprite = slootData.ToolsImages;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.Instance.CurrentSlotSelect = slootData;
        InventoryManager.Instance.IdSlotCurrent = slootData.ToolsID;
        AnimationSlotEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        InventoryManager.Instance.CurrentSlotSelect = null;
        InventoryManager.Instance.IdSlotCurrent = 0;
        AnimationSlotExit();
    }

    private void AnimationSlotEnter()
    {
        transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
            
    }
    private void AnimationSlotExit()
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
    }

}
