using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{
    public NpcData Npc;
    [SerializeField] private string NpcName;
    [SerializeField] public int TotalWeed;
    [SerializeField] public int TotalPrice;
    [SerializeField] private bool IsCollisionEnabled;
    [SerializeField] public SpriteRenderer NpcSpriteRenderer;

    [Header("Ui")]
    [SerializeField] private GameObject PanelNpc;
    [SerializeField] private TextMeshProUGUI Text_Name;
    [SerializeField] private Image BarLevelDrog;
    public TextMeshProUGUI Text_Order;


    [Header("Script")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerManager playerManager;

    public void SellDrog()
    {
        playerManager.CurrentMoney += TotalPrice;
        inventoryManager.Removeweed(TotalWeed);
        ResetData();
        PanelNpc.SetActive(false);
    }

    public void RejectOffert()
    {
        ResetData();
        PanelNpc.SetActive(false);
    }

    private void ResetData()
    {
        Npc = null;
        TotalWeed = 0;
        TotalPrice = 0;
    }

    private void OpenPanel()
    {
        BarLevelDrog.fillAmount = Npc.TotalWeedAssuming;
        PanelNpc.SetActive(true);
        IsCollisionEnabled = true;
        Text_Name.text = Npc.NameNpc.ToString();
    }

    private void ClosePanel()
    {
        PanelNpc.SetActive(false);
        IsCollisionEnabled = false;
        Text_Name.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClosePanel();
        }
    }
}
