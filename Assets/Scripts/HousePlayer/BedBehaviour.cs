using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class BedBehaviur : MonoBehaviour
{

    [SerializeField] private GameObject ButtSleep;
    [SerializeField] private bool IsCollision;
    [SerializeField] private bool IsSleeping;
    [SerializeField] private CanvasGroup FadeOutImage;
    [SerializeField] private TextMeshProUGUI Text_Message;
    [SerializeField] private float timeSleep;
    public cycleDayNight cycleDayNight;

    private void Update()
    {
        if(cycleDayNight != null && cycleDayNight.CurrentHours < 19 && cycleDayNight.CurrentHours > 7)
        {
            Text_Message.gameObject.SetActive(true);
        }
        else
        {
            Text_Message.gameObject.SetActive(false);
        }
    }
    public void Sleep()
    {
        if (cycleDayNight != null && IsCollision && cycleDayNight.CurrentHours >= 19)
        {
            Debug.Log("Sleep active");
            IsSleeping = true;
            AnimationFadeOut();
            StartCoroutine(ActiveSleep());
        }
    }

    IEnumerator ActiveSleep()
    {
        yield return new WaitForSeconds(5f);
        cycleDayNight.CurrentHours = 8;
        cycleDayNight.CurrentMinutes = 0f;
        cycleDayNight.light2D.intensity = 1f; 
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
