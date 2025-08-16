using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppMessagingManager : MonoBehaviour
{
    public List<MessageSystem> ListMessage = new List<MessageSystem>();
    [SerializeField] private Transform ParentMessageContainer;
    [SerializeField] private GameObject MessagePrefab;
    [SerializeField] private NpcData[] Customers;
    [SerializeField] private MessageSystem lastMessage;
    [SerializeField] private float timetoSpawn;

    [Header("Ui")]
    [SerializeField] private GameObject ContainerApp;
    [SerializeField] private GameObject ButtQuit;
    [SerializeField] private GameObject ContainerMessage;
    [SerializeField] private Image Icon_App;



    public PlayerManager PlayerManager; 
    public AppSetting AppSetting;
    public NpcManager NpcManager;
    public Boat_Npc BoatNpc;
    private void OnEnable()
    {
        StartCoroutine(spawnMessage());
    }

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

            if (CheakMessage())
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            timetoSpawn = Random.Range(30, 120);
            yield return new WaitForSeconds(timetoSpawn);
            GameObject newMessage = Instantiate(MessagePrefab, transform);
            MessageSystem message = newMessage.GetComponent<MessageSystem>();
            lastMessage = message;
            message.currentNpc = Customers[Random.Range(0, Customers.Length)];
            message.NameUser = lastMessage.currentNpc.NameNpc;
            message.UserNameText.text = message.NameUser;
            message.PlayerManager = PlayerManager;
            message.AppMessagingManager = this;
            message.NpcManager = NpcManager;
            message.UserIcon = lastMessage.currentNpc.IconNpc;
            message.BoatNpc = BoatNpc;
            ListMessage.Add(message);
            newMessage.transform.SetParent(ParentMessageContainer);
            AppSetting.CurrentNoticationManager.Play();
        }
    }

    public bool CheakMessage()
    {
        foreach (var message in ListMessage)
        {
            if (message.currentNpc == lastMessage.currentNpc)
            {
                return false; // Prevent creating a message for the same NPC
            }
        }
        return true;
    }

    public void CloseApp()
    {
        ContainerApp.SetActive(true);
        ButtQuit.SetActive(false);
        ContainerMessage.SetActive(false);
        Icon_App.enabled = false;
    }
}
