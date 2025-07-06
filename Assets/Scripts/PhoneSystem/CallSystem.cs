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

    [Header("Sound")]
    [SerializeField] private AudioSource Audio_1;
    [SerializeField] private AudioSource Audio_2;
    [SerializeField] private AudioSource Audio_3;

    public void NumberSelect(int number)
    {
        TextNumber.text += number.ToString();
    }

    public void ButtonCall()
    { 
        if (TextNumber.text.Length == 0) { return; }

        //check the number is valid 
        if(int.TryParse(TextNumber.text,out int number))
        {
            if (numbersValid.Contains(number))
            {
                TextNumber.text = string.Empty;
                switch(number)
                {
                    case 43545778:
                        Audio_1.Play();
                        break;
                    case 14487659:
                        Audio_2.Play();
                        break;
                    case 96806475:
                        Audio_3.Play();
                        break;
                }
            }
            else
            {
                TextNumber.text = string.Empty;
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
