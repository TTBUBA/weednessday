using System.Collections;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    [SerializeField] private NpcData[] Npc;
    [SerializeField] private float Probability_Controll = 0.75f;

    private void Start()
    {
       StartCoroutine(cheakNpc());
    }

    public void SendNews(NpcData currentNpc)
    {
        //Debug.Log($"{currentNpc.name} isArrested");
    }
    IEnumerator cheakNpc()
    {
        while (true)
        {
            float randomTime = Random.Range(4f, 5f);
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
                            if (Random.value > probability_free)
                            {
                                Debug.Log($"{npc.NameNpc} is free");
                            }
                            else
                            {
                                Debug.Log($"{npc.NameNpc} is found of drog");

                                if (Random.value > npc.loyaltyNpc)
                                {
                                    SendNews(npc);
                                    float TimeRelease = Random.Range(3f, 6f);
                                    StartCoroutine(TimeReleaseNpc(TimeRelease,npc));
                                    npc.IsArrested = true;
                                }
                                else
                                {
                                    //Debug.Log($"{npc.NameNpc} is not arrested");
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
    }
}
