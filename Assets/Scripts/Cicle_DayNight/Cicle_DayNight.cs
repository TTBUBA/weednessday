using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cicle_DayNight : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    public float CurrentHours;
    public float CurrentMinutes;
    [SerializeField] private float LightNight;
    [SerializeField] private float IncreseTime = 2f;

    private void Awake()
    {
        StartCoroutine(CicleDay());
    }
    

    IEnumerator CicleDay()
    {
        while (true)
        {
            yield return new WaitForSeconds(IncreseTime);
            CurrentMinutes += 1f;
            if (CurrentMinutes >= 60f)
            {
                CurrentMinutes = 0f;
                CurrentHours += 1f;
            }
            if (CurrentHours == 19)
            {
                if (light2D.intensity > LightNight) {

                    yield return new WaitForSeconds(0.5f);
                    light2D.intensity -= 0.1f;
                }

            }

            if (CurrentHours == 8)
            {
                if (light2D.intensity >= 1f) {
                    yield return new WaitForSeconds(0.5f);
                    light2D.intensity += 0.1f;
                }
            }
        }
    }
}
