using TMPro;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public NpcData Npc;
    [SerializeField] private string NpcName;
    [SerializeField] public int TotalWeed;
    [SerializeField] public int TotalPrice;
    [SerializeField] private bool IsCollisionEnabled;


    [Header("Ui")]
    [SerializeField] private GameObject PanelNpc;
    [SerializeField] private TextMeshProUGUI Text_Name;



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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PanelNpc.SetActive(true);
            IsCollisionEnabled = false;
            Text_Name.text = Npc.NameNpc.ToString();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelNpc.SetActive(false);
            IsCollisionEnabled = true;
            Text_Name.text = "";
        }
    }
}
