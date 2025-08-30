using InfinityCode.UltimateEditorEnhancer.HierarchyTools;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialState tutorialState;

    [Header("Ui")]
    [SerializeField] private Image Icon_ArrowHoe;
    [SerializeField] private Image Icon_ArrowWateringCan;
    [SerializeField] private Image Icon_ArrowBacket;
    [SerializeField] private Image Icon_ArrowSeed;
    [SerializeField] private Image Icon_ArrowSmartPhone;
    [SerializeField] private Image Icon_ArrowAppHercules;
    [SerializeField] private Image Icon_ArrowOrderSeed;
    [SerializeField] private Image Icon_ArrowEffectOrder;


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
        MakeOrder,
        useSeed,
        useWateringCan,
        End
    }

    // Update is called once per frame
    void Update()
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
                if(UseOrderSeed())
                {
                    tutorialState = TutorialState.MakeOrder;
                    Icon_ArrowOrderSeed.gameObject.SetActive(false);
                    Icon_ArrowEffectOrder.gameObject.SetActive(true);
                }
                break;
            case TutorialState.MakeOrder:
                if (MakefirstOrder())
                {
                    tutorialState = TutorialState.useSeed;
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
                Debug.Log("Tutorial End");
                break;
            default:
                break;
        }
    }

    private bool IsExitHouse() => HouseBehaviour.Instance.IsExitHouseFirstTime;
    private bool IsUseHoe() => PlantManager.Instance.UseHoeFirstTime;
    private bool IsUseSeed() => PlantManager.Instance.PlantFirstTime;
    private bool IsUseWateringCan() => PlantManager.Instance.UseWaterCanFirstTime;
    private bool IsUseBacket() => PlantManager.Instance.UseBacketFirstTime;
    private bool IsUseSmartPhone() => phonemanager.openedPhoneFirstTime;
    private bool OpenAppHercules() => MarketManager.OpenAppFirstTime;
    private bool UseOrderSeed() => MarketManager.AddFirstObjetInCart;
    private bool MakefirstOrder() => MarketManager.EffectFirstOrder;
}
