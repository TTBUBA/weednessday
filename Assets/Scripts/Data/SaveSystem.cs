using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public string filepath => Application.persistentDataPath + "/savefile.json";
    public List<ISaveable> saveables = new List<ISaveable>();
    public GameData gameData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //LoadGame();
    }

    public void SaveGame()
    {
        var data = new GameData();
        foreach (ISaveable saveable in saveables)
        {
            saveable.save(data);
        }
        gameData = data;

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filepath, json);
        Debug.Log($"Game Saved: {json}");
    }

    public void LoadGame()
    {
        if (File.Exists(filepath))
        {
            string FileJson = File.ReadAllText(filepath);
            gameData = JsonUtility.FromJson<GameData>(FileJson);

            foreach (ISaveable saveable in saveables)
            {
                saveable.load(gameData);
            }

            Debug.Log($"Game Loaded: {FileJson}");
        }
        else
        {
            gameData = new GameData();
            Debug.Log("No save file found, starting new game");
        }
    }

    public void DeleteData()
    {
        gameData = new GameData();
        SaveGame();
    }

}
