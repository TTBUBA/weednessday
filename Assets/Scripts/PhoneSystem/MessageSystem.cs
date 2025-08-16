using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MessageSystem : MonoBehaviour
{

    public Sprite UserIcon;
    public string NameUser;
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
    public Boat_Npc BoatNpc;
    private void Start()
    {
        TotalWeed = Random.Range(1, 10);
        PriceOffering = Random.Range(40, 60);
        PriceTotal = PriceOffering * TotalWeed;
        UserIconComponent.sprite = UserIcon;
        UserNameText.text = NameUser;
        MessageTextComponent.text = TotalWeed + "-" + MessageText.ToString();
        PriceText.text = PriceTotal.ToString() + "$";

        //TEST
        float randomValue = Random.Range(0f, 1f);
        currentNpc.TotalWeedAssuming = randomValue;
    }

    //Accetp and Reject Offering 
    public void AcceptOffering()
    {
        if(NpcManager.Npc == null)
        {
            NpcManager.TotalWeed = TotalWeed;
            NpcManager.TotalPrice = PriceTotal;
            NpcManager.NpcSpriteRenderer.sprite = currentNpc.NpcImage;
            NpcManager.Text_Order.text = MessageTextComponent.text;
            AppMessagingManager.ListMessage.Remove(this);
            NpcManager.Npc = currentNpc;
            BoatNpc.Text_timedelivery.gameObject.SetActive(true);//Active text when the boat is active
            BoatNpc.StartCoroutine(BoatNpc.ActiveBoat());//Start the coroutine to active the boat
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
