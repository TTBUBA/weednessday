using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PhoneManager : MonoBehaviour
{
    [SerializeField] private List<AppManager> AppManagers = new List<AppManager>();

    public AppManager CurrentAppSelect;

    [SerializeField] private GameObject ContainerPhone;
    [SerializeField] private TextMeshProUGUI Text_Button;


    public void OpenPhone()
    {
        ContainerPhone.SetActive(true);
        Text_Button.text = "Q";
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
