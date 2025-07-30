using UnityEngine;

public class SmsManager : MonoBehaviour
{
    [SerializeField] private GameObject smsPanel;
    [SerializeField] private GameObject message;

    public void OpenSms()
    {
        smsPanel.SetActive(false);
        message.SetActive(true);
    }

    public void CloseSms()
    {
        smsPanel.SetActive(true);
        message.SetActive(false);
    }
}
