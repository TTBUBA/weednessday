using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PlacedObject : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    public PlaceableObjectData placeableObjectData;
    public Image Icon;

    public UiManager UiManager;
    void Start()
    {
        Icon.sprite = placeableObjectData.UtilityIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlacementManager.Instance.CurrentplaceableObject = placeableObjectData;

        if(PlacementManager.Instance.LastObjSpawn != null)
        {
            Destroy(PlacementManager.Instance.LastObjSpawn);
        }

        PlacementManager.Instance.LastObjSpawn = Instantiate(placeableObjectData.UtilityPrefab, PlacementManager.Instance.MouseManager.MousePos, Quaternion.identity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
