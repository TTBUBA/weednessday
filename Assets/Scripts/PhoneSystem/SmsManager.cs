using TMPro;
using UnityEngine;

public class SmsManager : MonoBehaviour
{
    [SerializeField] private GameObject smsPanel;
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject SmsPrefabs;
    [SerializeField] private Transform containerSms;
    [SerializeField] private GameObject ContainerPanel;

    public AppSetting AppSetting;
    //create new sms and set the message text with placement object
    public void CreateNewSms(string message)
    {
        GameObject newsms = Instantiate(SmsPrefabs, smsPanel.transform);
        newsms.transform.SetParent(containerSms, false);
        newsms.GetComponentInChildren<TextMeshProUGUI>().text = message;
        AppSetting.CurrentNoticationManager.Play();
    }

    //open panelSms and show the message
    public void OpenSms()
    {
        smsPanel.SetActive(false);
        message.SetActive(true);
    }

    //close panel sms
    public void CloseSms()
    {
        smsPanel.SetActive(true);
        message.SetActive(false);
    }

    public void CloseApp()
    {
        ContainerPanel.SetActive(false);
    }
}
