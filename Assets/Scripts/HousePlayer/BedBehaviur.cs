using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BedBehaviur : MonoBehaviour
{

    [SerializeField] private GameObject ButtSleep;
    [SerializeField] private bool IsCollision;
    [SerializeField] private bool IsSleeping;
    [SerializeField] private CanvasGroup FadeOutImage;
    [SerializeField] private float timeSleep;
    public Cicle_DayNight cicledayNight;
    public void Sleep()
    {
        if (IsCollision && cicledayNight.CurrentHours >= 19)
        {
            IsSleeping = true;
            AnimationFadeOut();
            StartCoroutine(ActiveSleep());
        }
    }

    IEnumerator ActiveSleep()
    {
        yield return new WaitForSeconds(5f);
        cicledayNight.CurrentHours = 8;

    }

    private void AnimationFadeOut()
    {
        FadeOutImage.DOFade(1f, 1f).OnComplete(() =>
        {

            DOTween.Sequence()
                .AppendInterval(timeSleep) //wait the 0.5 before change the alpha image
                .Append(FadeOutImage.DOFade(0f, 1f));
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IsCollision = true;
            ButtSleep.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollision = false;
            ButtSleep.SetActive(false);
        }
    }
}
