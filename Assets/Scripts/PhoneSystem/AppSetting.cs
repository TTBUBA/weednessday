using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class AppSetting : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource CurrentNoticationManager;


    [Header("Ui")]
    [SerializeField] private GameObject ContainerAppSetting;
    [SerializeField] private GameObject PanelNotification;
    [SerializeField] private GameObject ContainerApp;
    [SerializeField] private GameObject Butt_QuitApp;
    public void ClosePanelSetting()
    {
        ContainerApp.SetActive(true);
        ContainerAppSetting.SetActive(false);
    }
    public void OpenPanelNotication()
    {
        PanelNotification.SetActive(true);
        ContainerAppSetting.SetActive(false);
    }

    public void ClosePanelNotication()
    {
        PanelNotification.SetActive(false);
        ContainerAppSetting.SetActive(true);
    }

}
