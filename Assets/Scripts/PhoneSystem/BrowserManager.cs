using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrowserManager : MonoBehaviour
{
    [SerializeField] private List<string> nameSite = new List<string>();
   // public string CurrentSiteWrite;

    [Header("Site")]
    [SerializeField] private GameObject site_adust;

    [Header("Ui")]
    [SerializeField] private Button searchButton;
    [SerializeField] private TMP_InputField inputField;


    private void Update()
    {
        //SearchSite(inputField.text);
    }
    public void SearchSite(string site)
    {
        site = inputField.text;
        if(nameSite.Contains(site))
        {
            switch (site)
            {
                case "adust":
                    site_adust.SetActive(true);
                    break;
            }
        }
    }
}
