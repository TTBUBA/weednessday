using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseBehaviour : MonoBehaviour
{
    public static HouseBehaviour Instance;

    [SerializeField] private Transform Player;
    [SerializeField] private Transform PointEntry;
    [SerializeField] private Transform PointExit;
    [SerializeField] private bool Incollision;
    [SerializeField] private bool IsEntryHouse;
    public bool IsExitHouseFirstTime;

    [Header("Ui elements")]
    [SerializeField] private GameObject ButtEntryHouse;
    [SerializeField] private GameObject ButtExitHouse;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private CanvasGroup FadeOutImage;

    private void Awake()
    {
        Instance = this;
    }

    public void EntryHouse()
    {
        if (Incollision)
        {
           AnimationFadeOut(PointEntry);
           IsEntryHouse = true;
           textButton.text = "Exit Q";
           ButtEntryHouse.SetActive(false);
           ButtExitHouse.SetActive(true);
        }
    }

    public void ExitHouse()
    {
        if (IsEntryHouse)
        {
            AnimationFadeOut(PointExit);
            textButton.text = "Entry E";
            ButtExitHouse.SetActive(false);
            IsEntryHouse = false;
        }
    }

    private void AnimationFadeOut(Transform destination)
    {
        FadeOutImage.DOFade(1f, 1f).OnComplete(() =>
        {
            // Move the player to the destination point
            Player.transform.position = destination.position;
            DOTween.Sequence()
                .AppendInterval(0.5f) //wait the 0.5 before change the alpha image
                .Append(FadeOutImage.DOFade(0f, 1f)).OnComplete(() =>
                {
                    IsExitHouseFirstTime = true;
                });
        });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Incollision = true;
            ButtEntryHouse.SetActive(true);
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Incollision = false;
            ButtEntryHouse.SetActive(false);
        }
    }

}
