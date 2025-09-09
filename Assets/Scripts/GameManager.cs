using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour,ISaveable
{
    public static GameManager Instance;

    public bool PlayFirstTime = false;
    [SerializeField] private int TimeSaveGame;
    [SerializeField] private bool AutoSaveGame;

    [Header("Panel-Reword")]
    [SerializeField] private GameObject PanelReword;
    [SerializeField] private SlootData seedWeed;
    [SerializeField] private PlayerManager Playermanager;

    [Header("Panel-FinishDemo")]
    [SerializeField] private bool Demo;
    [SerializeField] private GameObject PanelDemo;
    [SerializeField] private cycleDayNight cycleDayNight;

    public GameData GameData;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ActiveDemo();
    }

    private void Start()
    {
        SaveSystem.Instance.saveables.Add(this);
        if(GameData.TutorialCompleted == true && GameData.PlantFirstTime)
        {
            SaveSystem.Instance.DeleteData();
        }
        if (PanelReword.gameObject != null && PlayFirstTime)
        {
            PanelReword.SetActive(true);

        }
        else
        {
            PanelReword.SetActive(false);
        }
        StartCoroutine(SaveGameTimer());
        //SaveSystem.Instance.LoadGame();
    }

    public void CollectReward()
    {
        if (PlayFirstTime)
        {
            PanelReword.SetActive(false);
            PlayFirstTime = false;
            Playermanager.CurrentMoney += 500;
            InventoryManager.Instance.AddItem(seedWeed, 10);
            SaveSystem.Instance.LoadGame();
            //SaveSystem.Instance.SaveGame();
        }
        else
        {
            PanelReword.SetActive(false);
        }
    }

    public void ActiveDemo()
    {
        if (!Demo) return;
        if (cycleDayNight.CurrentDay >= 3)
        {

            PanelDemo.SetActive(true);
        }
        else
        {
            PanelDemo.SetActive(false);
        }
    }
    public void QuitApplication()
    {
        SaveSystem.Instance.DeleteData();
        Application.Quit();
    }
    IEnumerator SaveGameTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeSaveGame);
            if (AutoSaveGame)
            {
                SaveSystem.Instance.SaveGame();
            }
        }
    }

    public void save(GameData data)
    {
        data.PlayFirstTime = PlayFirstTime;
    }

    public void load(GameData data)
    {
        PlayFirstTime = data.PlayFirstTime;
    }
}
