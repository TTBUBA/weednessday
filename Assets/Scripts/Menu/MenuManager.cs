using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private Image FadeOut;

    public GameData GameData;

    private void Start()
    {
        if (!GameData.TutorialCompleted)
        {
            SaveSystem.Instance.DeleteData();
            Debug.Log("New Game, No Save Data");
        }
        else
        {
            SaveSystem.Instance.LoadGame();
            Debug.Log("Load Save Data");
        }
    }
    public void ButtPlayGame()
    {
        StartCoroutine(LoadingScene());
        FadeOut.enabled = true;
    }

    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(0.5f);
        FadeOut.DOFade(0f, 1f).OnComplete(() =>
        {
            FadeOut.DOFade(1f, 1f).OnComplete(() =>
            {
                if (GameData.TutorialCompleted == true)
                {
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    SceneManager.LoadScene("Tutorial");
                }
                FadeOut.DOFade(0, 1f).OnComplete(() =>
                {
                    Debug.Log("Load Scene Complete");
                });
            });
        });
    }

    public void ButtOpenSetting()
    {

    }

    public void ButtQuitGame()
    {
        Application.Quit();
    }
}
