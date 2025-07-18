using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrowserManager : MonoBehaviour
{
    [SerializeField] private List<string> nameSite = new List<string>();


    [Header("Site")]
    [SerializeField] private GameObject site_adust;
    [SerializeField] private GameObject site_hercules;
   

    [Header("Ui")]
    [SerializeField] private Button searchButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI Text_NameSite;
    [SerializeField] private GameObject Bar_Search;
    [SerializeField] private GameObject Obj_NameSite;
    public void SearchSite(string site)
    {
        site = inputField.text;
        if(nameSite.Contains(site))
        {
            switch (site)
            {
                case "adust":
                    site_adust.SetActive(true);
                    Bar_Search.SetActive(false);
                    Obj_NameSite.SetActive(true);
                    Text_NameSite.text = site;
                    break;
                case "hercules":
                    site_hercules.SetActive(true);
                    Bar_Search.SetActive(false);
                    Obj_NameSite.SetActive(true);
                    Text_NameSite.text = site;
                    break;
            }
        }
    }
}
