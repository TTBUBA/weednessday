using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MessageSystem : MonoBehaviour
{

    [SerializeField] private Sprite UserIcon;
    [SerializeField] public string NameUser;
    [SerializeField] private string MessageText;
    [SerializeField] private int PriceOffering;
    [SerializeField] private int PriceTotal;
    [SerializeField] private int TotalWeed;

    [Header("UI Components")]
    [SerializeField] private Image UserIconComponent;
    [SerializeField] public TextMeshProUGUI UserNameText;
    [SerializeField] private TextMeshProUGUI MessageTextComponent;
    [SerializeField] private TextMeshProUGUI PriceText;
    public NpcData currentNpc;


    public PlayerManager PlayerManager;
    public AppMessagingManager AppMessagingManager;
    public NpcManager NpcManager;
    private void Awake()
    {
        TotalWeed = Random.Range(1, 10);
        PriceOffering = Random.Range(40, 60);
        PriceTotal = PriceOffering * TotalWeed;
        UserIconComponent.sprite = UserIcon;
        UserNameText.text = NameUser;
        MessageTextComponent.text = TotalWeed + "-" + MessageText.ToString();
        PriceText.text = PriceTotal.ToString() + "$";
    }
    public void AcceptOffering()
    {
        if(NpcManager.Npc == null)
        {
            NpcManager.TotalWeed = TotalWeed;
            NpcManager.TotalPrice = PriceTotal;
            //PlayerManager.CurrentMoney += PriceTotal;
            AppMessagingManager.ListMessage.Remove(this);
            //InventoryManager.Instance.Removeweed(TotalWeed);
            NpcManager.Npc = currentNpc;
            Destroy(this.gameObject);
        }
    }

    public void RejectOffering()
    {
        this.gameObject.SetActive(false);
        AppMessagingManager.ListMessage.Remove(this);
        Destroy(this.gameObject);
    }
}
