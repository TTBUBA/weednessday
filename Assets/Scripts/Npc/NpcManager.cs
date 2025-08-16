using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{
    public NpcData Npc;
    public int TotalWeed;
    public int TotalPrice;
    public bool ActiveBoatReturn;
    [SerializeField] private bool IsCollisionEnabled;
    [SerializeField] public SpriteRenderer NpcSpriteRenderer;

    [Header("Ui")]
    [SerializeField] private GameObject PanelNpc;
    [SerializeField] private TextMeshProUGUI Text_Name;
    [SerializeField] private Image BarLevelDrog;
    [SerializeField] private GameObject Butt_OpenPanel;
    [SerializeField] private TextMeshProUGUI Text_Button;
    public TextMeshProUGUI Text_Order;


    [Header("Script")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Boat_Npc boatNpc;

    private void OnEnable()
    {
        ActiveBoatReturn = false;
    }
    public void SellDrog()
    {
        playerManager.CurrentMoney += TotalPrice;
        inventoryManager.Removeweed(TotalWeed);
        ResetData();
        PanelNpc.SetActive(false);
        ActiveBoatReturn = true;
    }

    public void RejectOffert()
    {
        ResetData();
        PanelNpc.SetActive(false);
        this.gameObject.SetActive(false);
        ActiveBoatReturn = true;
    }

    private void ResetData()
    {
        Npc = null;
        TotalWeed = 0;
        TotalPrice = 0;
    }

    public void OpenPanelNpc()
    {
        BarLevelDrog.fillAmount = Npc.TotalWeedAssuming;
        PanelNpc.SetActive(true);
        IsCollisionEnabled = true;
        Text_Name.text = Npc.NameNpc.ToString();
        Text_Button.text = "Close Q";
    }

    public void ClosePanelNpc()
    {
        PanelNpc.SetActive(false);
        IsCollisionEnabled = false;
        Text_Button.text = "Open E";
        Text_Name.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Butt_OpenPanel.SetActive(true);
            Text_Button.text = "Open E";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Butt_OpenPanel.SetActive(false);
            Text_Button.text = "Open E";
            PanelNpc.SetActive(false);
            IsCollisionEnabled = false;
            Text_Name.text = "";
        }
    }
}
