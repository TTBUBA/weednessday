using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PhoneManager : MonoBehaviour
{
    [SerializeField] private List<AppManager> AppManagers = new List<AppManager>();

    public AppManager CurrentAppSelect;

    [SerializeField] private GameObject ContainerPhone;
    [SerializeField] private TextMeshProUGUI Text_Button;
    [SerializeField] private TextMeshProUGUI Text_Hours;

    public Cicle_DayNight Cicle_DayNight;
    public void OpenPhone()
    {
        ContainerPhone.SetActive(true);
        Text_Button.text = "Q";
    }
    
    private void Update()
    {
        Text_Hours.text = Cicle_DayNight.CurrentHours.ToString("00") + ":" + Cicle_DayNight.CurrentMinutes.ToString("00");
    }
    public void ClosePhone()
    {
        foreach (var app in AppManagers)
        {
            if (!app.IsActiveApp)
            {
                ContainerPhone.SetActive(false);
                Text_Button.text = "C";
            }
            else
            {
                app.CloseApp();
                ContainerPhone.SetActive(true);
            }
        }
    }
}
