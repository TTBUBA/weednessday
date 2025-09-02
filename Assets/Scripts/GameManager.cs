using UnityEngine;

public class GameManager : MonoBehaviour, ISaveable
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
        SaveSystem.Instance.saveables.Add(this);
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

    public void save(GameData data)
    {
        data.TutorialCompleted = TutorialCompleted;
    }

    public void load(GameData data)
    {
        TutorialCompleted = data.TutorialCompleted;
    }
}
