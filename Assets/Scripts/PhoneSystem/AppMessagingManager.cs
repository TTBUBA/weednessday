using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMessagingManager : MonoBehaviour
{
    public List<MessageSystem> ListMessage = new List<MessageSystem>();
    [SerializeField] private Transform ParentMessageContainer;
    [SerializeField] private MessageSystem MessagePrefab;
    string[] Customers = new string[]
    {
        "Bonghilda",
        "Erbinator",
        "Mr.Always",
        "Lemon",
        "Lady",
        "Dopephano",
        "Puff",
        "Jointzilla",
        "Captain",
        "Mary",
        "Crystal",
        "Greenburn",
        "Ganjalf",
        "Cheech",
        "Sativar"
    };



    public PlayerManager PlayerManager;
    public AppManager AppManager;   
    public AppSetting AppSetting;

    public void CreateNewMessage()
    {
        StartCoroutine(spawnMessage());
    }

    IEnumerator spawnMessage()
    {
        while (true)
        {
            if (ListMessage.Count >= 10)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            if (!AppManager.IsActiveApp)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            int time = Random.Range(1, 2);
            yield return new WaitForSeconds(time);
            GameObject newMessage = Instantiate(MessagePrefab.gameObject, transform);
            MessageSystem message = newMessage.GetComponent<MessageSystem>();
            message.NameUser = Customers[Random.Range(0, Customers.Length)];
            message.UserNameText.text = message.NameUser;
            message.PlayerManager = PlayerManager;
            message.AppMessagingManager = this;
            ListMessage.Add(message);
            newMessage.transform.SetParent(ParentMessageContainer);
            AppSetting.CurrentNoticationManager.Play();
            //AppSetting.CurrentNoticationManager.volume = 0.4f;
        }
    }

}
