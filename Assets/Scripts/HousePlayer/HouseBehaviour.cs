using DG.Tweening;
using TMPro;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform PointEntry;
    [SerializeField] private Transform PointExit;
    [SerializeField] private bool Incollision;
    [SerializeField] private bool IsEntryHouse;


    [Header("Ui elements")]
    [SerializeField] private GameObject ButtEntryHouse;
    [SerializeField] private GameObject ButtExitHouse;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private CanvasGroup FadeOutImage;


    public MovementCamera camera;
    public void EntryHouse()
    {
        if (Incollision)
        {
           AnimationFadeOut(PointEntry);
           IsEntryHouse = true;
           textButton.text = "Exit Q";
           ButtEntryHouse.SetActive(false);
           ButtExitHouse.SetActive(true);
           camera.SmoothCam = 0;
           PlantManager.Instance.ActivePlant = false; // Disable plantSystem when entering the house
        }
    }

    public void ExitHouse()
    {
        if (!Incollision && IsEntryHouse)
        {
            AnimationFadeOut(PointExit);
            textButton.text = "Entry E";
            camera.SmoothCam = 0f;
            ButtExitHouse.SetActive(false);
            PlantManager.Instance.ActivePlant = true; // active plantSystem when entering the house
        }
    }

    private void AnimationFadeOut(Transform destination)
    {
        FadeOutImage.DOFade(1f, 1f).OnComplete(() =>
        {
            // Move the player to the destination point
            Player.transform.position = destination.position;
            camera.SmoothCam = 0.6f;
          
            DOTween.Sequence()
                .AppendInterval(0.5f) //wait the 0.5 before change the alpha image
                .Append(FadeOutImage.DOFade(0f, 1f));
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
