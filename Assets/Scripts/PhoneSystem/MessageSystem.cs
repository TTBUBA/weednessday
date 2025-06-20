using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MessageSystem : MonoBehaviour
{

    [SerializeField] private Sprite UserIcon;
    [SerializeField] public string NameUser;
    [SerializeField] private string MessageText;
    [SerializeField] private int PriceOffering;

    [Header("UI Components")]
    [SerializeField] private Image UserIconComponent;
    [SerializeField] public TextMeshProUGUI UserNameText;
    [SerializeField] private TextMeshProUGUI MessageTextComponent;
    [SerializeField] private TextMeshProUGUI PriceText;

    public PlayerManager PlayerManager;
    private void Awake()
    {
        UserIconComponent.sprite = UserIcon;
        UserNameText.text = NameUser;
        MessageTextComponent.text = MessageText;
        PriceText.text = PriceOffering.ToString() + "$";
    }
    public void AcceptOffering()
    {
        PlayerManager.CurrentMoney += PriceOffering;
        this.gameObject.SetActive(false);
    }

    public void RejectOffering()
    {
        this.gameObject.SetActive(false);
    }
}
