using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool TutorialCompleted;
    [SerializeField] private bool PlayFirstTime;
    [SerializeField] private GameObject PanelReword;
    [SerializeField] private SlootData seedWeed;
    [SerializeField] private PlayerManager Playermanager;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectReward()
    {
        if (PlayFirstTime)
        {
            PanelReword.SetActive(false);
            PlayFirstTime = false;
            Playermanager.CurrentMoney += 500;
            InventoryManager.Instance.AddItem(seedWeed, 10);
        }
    }
}
