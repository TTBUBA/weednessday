using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image IconButton;
    [SerializeField] private Sprite IconButtonSelect;
    [SerializeField] private Sprite IconButtonExit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        IconButton.sprite = IconButtonSelect;
        AnimationEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IconButton.sprite = IconButtonExit;
        AnimationExit();
    }

    private void AnimationEnter()
    {
        IconButton.transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.2f);
    }

    private void AnimationExit()
    {
        IconButton.transform.DOScale(new Vector3(1f, 1f, 1), 0.2f);
    }
}
