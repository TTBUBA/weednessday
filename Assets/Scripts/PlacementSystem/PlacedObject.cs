using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PlacedObject : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public PlaceableObjectData placeableObjectData;
    public Image Icon;

    void Start()
    {
        Icon.sprite = placeableObjectData.UtilityIcon;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlacementManager.Instance.CurrentplaceableObject = placeableObjectData;
        AnimPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimPointerExit();
    }

    private void AnimPointerEnter()
    {
        transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutBack);
    }

    private void AnimPointerExit()
    {
        transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutBack);
    }

}
