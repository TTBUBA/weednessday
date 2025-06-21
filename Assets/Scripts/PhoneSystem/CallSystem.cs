using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CallSystem : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private GameObject callPanel;
    [SerializeField] private GameObject PanelContainerApp;
    [SerializeField] private TextMeshProUGUI TextNumber;
    [SerializeField] private List<int> numbersValid;
    public void NumberSelect(int number)
    {
        TextNumber.text += number.ToString();
    }

    public void ButtonCall()
    {
        if (TextNumber.text.Length < 0) { return; }

        if(int.TryParse(TextNumber.text,out int number))
        {
            if (numbersValid.Contains(number))
            {
                TextNumber.text = string.Empty;
                Debug.Log("Calling: " + TextNumber.text);
            }
            else
            {
                Debug.Log("Number not valid: " + number);
            }
        }
    }

    public void QuitApp()
    {
        TextNumber.text = string.Empty;
        PanelContainerApp.SetActive(true);
        callPanel.SetActive(false);
    }
}
