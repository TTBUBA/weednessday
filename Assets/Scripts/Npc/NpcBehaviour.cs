using System.Collections;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    [SerializeField] private NpcData[] npcData;
    [SerializeField] private cycleDayNight cycleDayNight;
    private void Start()
    {
        StartCoroutine(WeedAssumingNpc());
    }

    IEnumerator WeedAssumingNpc()
    {
        while (true)
        {
            if(cycleDayNight.CurrentDay < 3)
            {
                yield return null;
                continue;
            }
            yield return new WaitForSeconds(60f);
            foreach (var npc in npcData)
            {
                if(Random.value < npc.ProbabilitySmoke)
                {
                    if(npc.TotalWeedAssuming <= 1)
                    {
                        float randomValue = Random.Range(0.05f, 0.1f);
                        npc.TotalWeedAssuming += randomValue;
                        Debug.Log(npc.NameNpc + " is smoking");
                    }
                }
                {
                    if (npc.TotalWeedAssuming >= 1)
                    {
                        float randomValue = Random.Range(0.05f, 0.1f);
                        npc.TotalWeedAssuming -= randomValue;
                        Debug.Log(npc.NameNpc + " is not smoking");
                    }
                }

            }
        }
    }
}
