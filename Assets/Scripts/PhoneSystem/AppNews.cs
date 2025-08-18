using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppNews : MonoBehaviour
{
    [SerializeField] private List<NewsManager> NpcDataList = new List<NewsManager>();
    [SerializeField] private NpcData NpcArrested;
    [SerializeField] private GameObject PrefabsNews;
    [SerializeField] private Transform PrefabsNewsContainer;
   

    [Header("Ui")]
    [SerializeField] private GameObject ContainerApp;
    [SerializeField] private GameObject ButtQuit;
    [SerializeField] private TextMeshProUGUI TextTitle;

    public PoliceManager PoliceManager;
    //create news when the npc is arrested
    public void CreateNews(NpcData npc)
    {
        NpcArrested = npc;
        GameObject news = Instantiate(PrefabsNews, transform);
        NewsManager newsManager = news.GetComponent<NewsManager>();
        newsManager.NpcArrested = npc;
        newsManager.SetNews(PoliceManager.TotalWeedFound);//set news and add in the news total weed found
        newsManager.transform.SetParent(PrefabsNewsContainer, false);
        NpcDataList.Add(newsManager);
    }

    public void CloseApp()
    {
        ContainerApp.SetActive(false);
        ButtQuit.SetActive(false);
        TextTitle.enabled = false;
    }
}
