using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectWeed : MonoBehaviour
{
    [SerializeField] private Volume volume; // Reference to the Volume component
    public float ValueEffectchromatic = 0.3f;
    public float ValueEffectDistortion = -0.3f;
    private VolumeProfile profile; 
    public bool ActiveDrog = false;

    public PlayerManager PlayerManager;
    private void Start()
    {
        profile = volume.profile;
    }
    public IEnumerator EffectCane()
    {
        yield return new WaitForSeconds(1f); 

        if (ActiveDrog && profile.TryGet(out ChromaticAberration chromaticAberration)  && profile.TryGet(out LensDistortion Distortion))
        {
            yield return new WaitForSeconds(0.5f);
            chromaticAberration.intensity.value = ValueEffectchromatic;
            Distortion.intensity.value = ValueEffectDistortion;
        }
        yield return new WaitForSeconds(10f);
        EffectFinish();
    }


    private void EffectFinish()
    {
        PlayerManager.ActiveButtun = true;
        PlayerManager.TotalCaneSmoke = 0;
        foreach (var image in PlayerManager.ImageEye)
        {
            image.gameObject.SetActive(false);
        }
        if (profile.TryGet(out ChromaticAberration chromaticAberration) && profile.TryGet(out LensDistortion Distortion))
        {
            chromaticAberration.intensity.value = 0f;
            Distortion.intensity.value = 0f;
        }
    }
}
