using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
   public NpcData NpcArrested;
   public TextMeshProUGUI Text_Message;
   public Image IconNpc;

    private void Update()
    {
        if(NpcArrested != null && NpcArrested.IsArrested == false)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetNews(int weedFound)
    {
        Text_Message.text = $"{NpcArrested.name} because found {weedFound} in the pocket";
        IconNpc.sprite = NpcArrested.IconNpc;
    }

    public void ReadNews()
    {
        Destroy(this.gameObject);
    }
}
