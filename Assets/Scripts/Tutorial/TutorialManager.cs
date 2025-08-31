using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialState tutorialState;
    [SerializeField] private bool TutorialActive;
    [SerializeField] private string NameSceneLoad;

    [Header("Ui")]
    [SerializeField] private Image Icon_ArrowHoe;
    [SerializeField] private Image Icon_ArrowWateringCan;
    [SerializeField] private Image Icon_ArrowBacket;
    [SerializeField] private Image Icon_ArrowSeed;
    [SerializeField] private Image Icon_ArrowSmartPhone;
    [SerializeField] private Image Icon_ArrowAppHercules;
    [SerializeField] private Image Icon_ArrowOrderSeed;
    [SerializeField] private Image Icon_ArrowOpenCart;
    [SerializeField] private Image Icon_ArrowEffectOrder;
    [SerializeField] private TextMeshProUGUI Text_TutorialComplete;
    [SerializeField] private TextMeshProUGUI Text_WelcomeMessage;
    [SerializeField] private CanvasGroup FadeOutImage;

    [Header("Managers")]
    [SerializeField] private PhoneManager phonemanager;
    [SerializeField] private MarketManager MarketManager;
    enum TutorialState
    {
        ExitHouse,
        useHoe,
        useBacket,
        UseSmartPhone,
        OpenAppHercules,
        UseOrderSeed,
        OpenCart,
        MakeOrder,
        useSeed,
        useWateringCan,
        End
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialActive)
        {
            switch (tutorialState)
            {
                case TutorialState.ExitHouse:
                    if (IsExitHouse())
                    {
                        tutorialState = TutorialState.useHoe;
                        Icon_ArrowHoe.gameObject.SetActive(true);
                    }
                    break;
                case TutorialState.useHoe:
                    if (IsUseHoe())
                    {
                        tutorialState = TutorialState.useWateringCan;
                        Icon_ArrowWateringCan.gameObject.SetActive(true);
                        Icon_ArrowHoe.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.useWateringCan:
                    if (IsUseWateringCan())
                    {
                        tutorialState = TutorialState.UseSmartPhone;
                        Icon_ArrowSmartPhone.gameObject.SetActive(true);
                        Icon_ArrowWateringCan.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.UseSmartPhone:
                    if (IsUseSmartPhone())
                    {
                        tutorialState = TutorialState.OpenAppHercules;
                        Icon_ArrowAppHercules.gameObject.SetActive(true);
                        Icon_ArrowSmartPhone.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.OpenAppHercules:
                    if (OpenAppHercules())
                    {
                        tutorialState = TutorialState.UseOrderSeed;
                        Icon_ArrowAppHercules.gameObject.SetActive(false);
                        Icon_ArrowOrderSeed.gameObject.SetActive(true);
                    }
                    break;
                case TutorialState.UseOrderSeed:
                    if (UseOrderSeed())
                    {
                        tutorialState = TutorialState.OpenCart;
                        Icon_ArrowOrderSeed.gameObject.SetActive(false);
                        Icon_ArrowOpenCart.gameObject.SetActive(true);
                    }
                    break;
                case TutorialState.OpenCart:
                    if (OpenCart())
                    {
                        tutorialState = TutorialState.MakeOrder;
                        Icon_ArrowEffectOrder.gameObject.SetActive(true);
                        Icon_ArrowOpenCart.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.MakeOrder:
                    if (MakefirstOrder())
                    {
                        tutorialState = TutorialState.useSeed;
                        Icon_ArrowSeed.gameObject.SetActive(true);
                        Icon_ArrowEffectOrder.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.useSeed:
                    if (IsUseSeed())
                    {
                        tutorialState = TutorialState.useBacket;
                        Icon_ArrowBacket.gameObject.SetActive(true);
                        Icon_ArrowSeed.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.useBacket:
                    if (IsUseBacket())
                    {
                        tutorialState = TutorialState.End;
                        Icon_ArrowBacket.gameObject.SetActive(false);
                    }
                    break;
                case TutorialState.End:
                    StartCoroutine(TutorialEnd());
                    TutorialActive = false;
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator TutorialEnd()
    {
        yield return new WaitForSeconds(0.5f);
        Text_TutorialComplete.DOFade(0f, 1f).OnComplete(() =>
        {
            Text_TutorialComplete.DOFade(1f, 1f).OnComplete(() =>
            {
                Text_TutorialComplete.DOFade(0, 1f).OnComplete(() =>
                {
                    AnimationFadeOut();
                });
            });
        });
    }

    private void AnimationFadeOut()
    {
        FadeOutImage.DOFade(1f, 3f).OnComplete(() =>
        {
            Text_WelcomeMessage.enabled = true;
            SceneManager.LoadScene(NameSceneLoad);
            DOTween.Sequence()
                .AppendInterval(0.5f) //wait the 3 before change the alpha image
                .Append(FadeOutImage.DOFade(0f, 3f)).OnComplete(() =>
                {
                    Text_WelcomeMessage.enabled = false;
                });
        });
    }
    //===Check Player Stats Tutorial===//
    private bool IsExitHouse() => HouseBehaviour.Instance.IsExitHouseFirstTime;
    private bool IsUseHoe() => PlantManager.Instance.UseHoeFirstTime;
    private bool IsUseSeed() => PlantManager.Instance.PlantFirstTime;
    private bool IsUseWateringCan() => PlantManager.Instance.UseWaterCanFirstTime;
    private bool IsUseBacket() => PlantManager.Instance.UseBacketFirstTime;
    private bool IsUseSmartPhone() => phonemanager.openedPhoneFirstTime;
    private bool OpenAppHercules() => MarketManager.OpenAppFirstTime;
    private bool UseOrderSeed() => MarketManager.AddFirstObjetInCart;
    private bool OpenCart() => MarketManager.OpenCartFirstTime;
    private bool MakefirstOrder() => MarketManager.EffectFirstOrder;
    
}
