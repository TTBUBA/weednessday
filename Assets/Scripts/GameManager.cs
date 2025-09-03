using UnityEngine;

public class GameManager : MonoBehaviour,ISaveable
{
    public static GameManager Instance;
    public bool SceneTutorialActive;
    public bool MenuOpen;
    public bool TutorialCompleted;
    public bool PlayFirstTime;
    [SerializeField] private GameObject PanelReword;
    [SerializeField] private SlootData seedWeed;
    [SerializeField] private PlayerManager Playermanager;

    private void Awake()
    {
        Instance = this;
        SaveSystem.Instance.saveables.Add(this);
    }

    private void Start()
    {
        if(PanelReword.gameObject != null && SceneTutorialActive || MenuOpen)
        {
            if (TutorialCompleted && !MenuOpen)
            {
                PanelReword.SetActive(true);
            }
            else
            {
                PanelReword.SetActive(false);
            }
        }
        SaveSystem.Instance.LoadGame();
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
        data.PlayFirstTime = PlayFirstTime;
        data.SceneTutorialActive = SceneTutorialActive;
    }

    public void load(GameData data)
    {
        TutorialCompleted = data.TutorialCompleted;
        PlayFirstTime = data.PlayFirstTime;
        SceneTutorialActive = data.SceneTutorialActive;
    }
}
