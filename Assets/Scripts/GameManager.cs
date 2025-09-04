using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour,ISaveable
{
    public static GameManager Instance;
    public bool PlayFirstTime = false;
    public int TimeSaveGame;
    [SerializeField] private GameObject PanelReword;
    [SerializeField] private SlootData seedWeed;
    [SerializeField] private PlayerManager Playermanager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveSystem.Instance.saveables.Add(this);
        if (PanelReword.gameObject != null)
        {
            PanelReword.SetActive(true);

        }
        else
        {
            PanelReword.SetActive(true);
        }
        StartCoroutine(SaveGameTimer());
    }

    public void CollectReward()
    {
        if (PlayFirstTime)
        {
            PanelReword.SetActive(false);
            PlayFirstTime = false;
            Playermanager.CurrentMoney += 500;
            InventoryManager.Instance.AddItem(seedWeed, 10);
            SaveSystem.Instance.SaveGame();
        }
    }

    IEnumerator SaveGameTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeSaveGame);
            SaveSystem.Instance.SaveGame();
            //Debug.Log("Game Saved Automatically");
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
