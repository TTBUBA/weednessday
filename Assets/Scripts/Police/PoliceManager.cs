using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    [Header("Police Npc")]
    [SerializeField] private NpcData[] Npc;
    [SerializeField] private float Probability_Controll = 0.75f;
    [SerializeField] private List<NpcData> NpcArrested = new List<NpcData>();
    public int TotalWeedFound = 0;

    public AppNews AppNews;
    public SpawnPolice SpawnPolice;
    public cycleDayNight CycleDayNight;

    private void Start()
    {
       StartCoroutine(cheakNpc());
    }

    //Send news when the npc is arrested
    public void SendNews(NpcData currentNpc)
    {
        AppNews.CreateNews(currentNpc);
    }

    //cheack npc and active police in case of found weed
    IEnumerator cheakNpc()
    {
        while (true)
        {
            if(CycleDayNight.CurrentDay >= 3)
            {
                float randomTime = Random.Range(60f, 120f);
                yield return new WaitForSeconds(randomTime);
                foreach (var npc in Npc)
                {
                    if (npc.IsHome)
                    {
                        if (Random.value < Probability_Controll)
                        {
                            if (Random.value > npc.TotalWeedAssuming)
                            {
                                var probability_free = npc.AbilityNpc - npc.TotalWeedAssuming;
                                if (Random.value < probability_free)
                                {
                                    //Debug.Log($"{npc.NameNpc} is free");
                                }
                                else
                                {
                                    //Debug.Log($"{npc.NameNpc} is found of drog");

                                    if (Random.value > npc.loyaltyNpc && !npc.IsArrested)
                                    {
                                        SendNews(npc);
                                        float TimeRelease = Random.Range(10, 20f);
                                        TotalWeedFound = Random.Range(1, 15);
                                        StartCoroutine(TimeReleaseNpc(TimeRelease, npc));
                                        npc.IsArrested = true;
                                        NpcArrested.Add(npc);

                                        //the npc talk by police and activePolice
                                        if (Random.value < npc.ProbabilityTalkPolice)
                                        {
                                            SpawnPolice.ActiveRandomBoatPolice();
                                            //Debug.Log("Npc talk");
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
    } 
    IEnumerator TimeReleaseNpc(float time,NpcData npc)
    {
        yield return new WaitForSeconds(time);
        npc.IsArrested = false;
        NpcArrested.Remove(npc);
    }
}
