using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class NoticationManager : MonoBehaviour , IPointerClickHandler 
{
    [SerializeField] private AudioSource NotificationSound;


    [Header("Ui")]
    [SerializeField] private Sprite ButtOff;
    [SerializeField] private Sprite ButtOn;
    [SerializeField] private Image ImageButton;
    [SerializeField] private bool AudioActive;

    public AppSetting AppSetting;
    public void ActiveAudio()
    {
        if(AudioActive) return;
        NotificationSound.Play();
        ImageButton.sprite = ButtOff;
        StartCoroutine(AudioFinish());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AppSetting.CurrentNoticationManager = NotificationSound;
    }

    IEnumerator AudioFinish()
    {
        // Wait for the audio clip to finish playing
        yield return new WaitForSeconds(NotificationSound.clip.length);
        AudioActive = true;
        NotificationSound.Stop();
        ImageButton.sprite = ButtOn;
        AudioActive = false;
    }

}
