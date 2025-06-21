using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppManager : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler  ,IPointerClickHandler
{
    [SerializeField] private GameObject AppPanel;
    [SerializeField] private GameObject ContainerApp;
    [SerializeField] private Image[] imagesToToggle;
    [SerializeField] private TextMeshProUGUI[] TextsToToggle;
    [SerializeField] private GameObject[] GameObjectsoToggle;
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
        AppPanel.SetActive(true);
        foreach (var image in imagesToToggle)
        {
            image.enabled = true;
        }
        foreach (var text in TextsToToggle)
        {
            text.enabled = true;
        }
        foreach (var obj in GameObjectsoToggle)
        {
            obj.SetActive(true);
        }
        ContainerApp.SetActive(false);
        IsActiveApp = true;
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
        foreach (var image in imagesToToggle)
        {
            image.enabled = false;
        }
        foreach (var text in TextsToToggle)
        {
            text.enabled = false;
        }
        foreach (var obj in GameObjectsoToggle)
        {
            obj.SetActive(true);
        }
        PhoneManager.CurrentAppSelect = null;
    }
}
