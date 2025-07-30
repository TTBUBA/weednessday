using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MessageSystem : MonoBehaviour
{

    [SerializeField] private Sprite UserIcon;
    [SerializeField] public string NameUser;
    [SerializeField] private string MessageText;
    [SerializeField] private int PriceOffering;
    [SerializeField] private int TotalWeed;

    [Header("UI Components")]
    [SerializeField] private Image UserIconComponent;
    [SerializeField] public TextMeshProUGUI UserNameText;
    [SerializeField] private TextMeshProUGUI MessageTextComponent;
    [SerializeField] private TextMeshProUGUI PriceText;

    public PlayerManager PlayerManager;
    public AppMessagingManager AppMessagingManager;
    private void Awake()
    {
        TotalWeed = Random.Range(1, 5);
        UserIconComponent.sprite = UserIcon;
        UserNameText.text = NameUser;
        MessageTextComponent.text = TotalWeed + "-" + MessageText.ToString();
        PriceText.text = PriceOffering.ToString() + "$";
        AppMessagingManager.CreateNewMessage();
    }
    public void AcceptOffering()
    {
        PlayerManager.CurrentMoney += PriceOffering;
        AppMessagingManager.ListMessage.Remove(this);
        InventoryManager.Instance.Removeweed(TotalWeed);
        Destroy(this.gameObject);
    }

    public void RejectOffering()
    {
        this.gameObject.SetActive(false);
        AppMessagingManager.ListMessage.Remove(this);
        Destroy(this.gameObject);
    }
}
