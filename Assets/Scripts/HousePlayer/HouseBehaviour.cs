using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HouseBehaviour : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform PointEntry;
    [SerializeField] private Transform PointExit;
    [SerializeField] private bool Incollision;

    [Header("Ui elements")]
    [SerializeField] private GameObject ButtEntryHouse;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private CanvasGroup FadeOutImage;

    public MovementCamera camera;
    public void EntryHouse()
    {
        if (Incollision)
        {
           AnimationFadeOut(PointEntry);
           textButton.text = "Exit Q";
           camera.SmoothCam = 0;
        }
    }

    public void ExitHouse()
    {
        if (Incollision)
        {
            AnimationFadeOut(PointExit);
            textButton.text = "Entry E";
            camera.SmoothCam = 0f;
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
