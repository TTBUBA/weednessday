using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class AppManager : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler  ,IPointerClickHandler
{
    [SerializeField] private GameObject AppPanel;
    [SerializeField] private GameObject ContainerApp;

    public bool IsActiveApp = false;
    public PhoneManager PhoneManager;
    public void OnPointerEnter(PointerEventData eventData)
    {
        PhoneManager.CurrentAppSelect = this;
        AnimationClikApp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimationExitApp();
    }

    private void AnimationClikApp()
    {
        transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutBack);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (AppPanel != null && ContainerApp != null)
        {
            AppPanel.SetActive(true);
            ContainerApp.SetActive(false);
            IsActiveApp = true;
        }

    }
    private void AnimationExitApp()
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
    }

    public void CloseApp()
    {
        IsActiveApp = false;
        AppPanel.SetActive(false);
        ContainerApp.SetActive(true);

        PhoneManager.CurrentAppSelect = null;
    }
}
