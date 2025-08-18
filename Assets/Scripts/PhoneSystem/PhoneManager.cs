using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PhoneManager : MonoBehaviour
{
    [SerializeField] private List<AppManager> AppManagers = new List<AppManager>();

    public AppManager CurrentAppSelect;

    [Header("UI Components")]
    [SerializeField] private Image Phone;
    [SerializeField] private GameObject ContainerApp;
    [SerializeField] private TextMeshProUGUI Text_Button;
    [SerializeField] private TextMeshProUGUI Text_Hours;

    public cycleDayNight cycleDayNight;
    public AppMessagingManager AppMessagingManager;
    public AppNews AppNews;
    public void OpenPhone()
    {
        Phone.enabled = true;
        Text_Button.text = "Q";
        Text_Hours.enabled = true;
        ContainerApp.SetActive(true);
    }
    
    private void Update()
    {
        Text_Hours.text = cycleDayNight.CurrentHours.ToString("00") + ":" + cycleDayNight.CurrentMinutes.ToString("00");
    }
    public void ClosePhone()
    {
        AppMessagingManager.CloseApp();
        AppNews.CloseApp();
        foreach (var app in AppManagers)
        {
            if (!app.IsActiveApp)
            {
                Phone.enabled = false;
                Text_Hours.enabled = false;
                Text_Button.text = "C";
                ContainerApp.SetActive(false);
                app.gameObject.SetActive(false);
            }
            else
            {
                app.CloseApp();
                Phone.enabled = true;
                Text_Hours.enabled = true;
                ContainerApp.SetActive(true);
            }
        }
    }
}
