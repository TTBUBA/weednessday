using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMessagingManager : MonoBehaviour
{
    [SerializeField] private List<MessageSystem> ListMessage = new List<MessageSystem>();
    [SerializeField] private Transform ParentMessageContainer;
    [SerializeField] private MessageSystem MessagePrefab;

    public PlayerManager PlayerManager;
    public string[] NameUser = { "John", "Alice", "Bob", "Charlie", "Diana" };
    public void CreateNewMessage()
    {
        StartCoroutine(spawnMessage());
    }

    IEnumerator spawnMessage()
    {
        while(true)
        {
            int time = Random.Range(1, 2);
            yield return new WaitForSeconds(time);
            GameObject newMessage = Instantiate(MessagePrefab.gameObject, transform);
            MessageSystem message = newMessage.GetComponent<MessageSystem>();
            message.NameUser = NameUser[Random.Range(0, NameUser.Length)];
            message.UserNameText.text = message.NameUser;
            message.PlayerManager = PlayerManager;
            Debug.Log("New message from: " + message.NameUser);
            newMessage.transform.SetParent(ParentMessageContainer);
        }
    }
}
