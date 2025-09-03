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
    [SerializeField] private bool PhoneEnabled = false;
    public bool openedPhoneFirstTime;

    public cycleDayNight cycleDayNight;
    public AppMessagingManager AppMessagingManager;
    public AppNews AppNews;
    public SmsManager SmsManager;

    
    private void Update()
    {
        if(cycleDayNight != null)
        {
            Text_Hours.text = cycleDayNight.CurrentHours.ToString("00") + ":" + cycleDayNight.CurrentMinutes.ToString("00");
        }
    }

    public void OpenPhone()
    {
        if (!PhoneEnabled)
        {
            Phone.enabled = true;
            Text_Button.text = "Q";
            Text_Hours.enabled = true;
            ContainerApp.SetActive(true);
            PhoneEnabled = true;
            openedPhoneFirstTime = true;
        }
    }

    public void ClosePhone()
    {
        if(PhoneEnabled)
        {
            AppMessagingManager.CloseApp();
            AppNews.CloseApp();
            SmsManager.CloseApp();
            PhoneEnabled = false;
            foreach (var app in AppManagers)
            {
                if (!app.IsActiveApp)
                {
                    Phone.enabled = false;
                    Text_Hours.enabled = false;
                    Text_Button.text = "Tab";
                    ContainerApp.SetActive(false);
                    app.gameObject.SetActive(true);
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
}
