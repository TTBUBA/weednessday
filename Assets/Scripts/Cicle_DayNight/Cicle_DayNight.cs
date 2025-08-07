using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cicle_DayNight : MonoBehaviour
{ 
    [SerializeField] private Light2D light2D;
    [SerializeField] private float LightNight;
    public float CurrentHours;
    public float CurrentMinutes;
    public float IncreseTime = 2f;
    public bool ActiveCicledayNight = true;


    private void Awake()
    {
        StartCoroutine(CicleDay());
    }
    

    IEnumerator CicleDay()
    {
        while (ActiveCicledayNight)
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
                IncreseTime = 0.5f; // Increase speed of time at night
                if (light2D.intensity > LightNight) {

                    yield return new WaitForSeconds(0.5f);
                    light2D.intensity -= 0.1f;
                }
            }

            if (CurrentHours == 8)
            {
                IncreseTime = 1f; 
                if (light2D.intensity >= 1f) {
                    yield return new WaitForSeconds(0.5f);
                    light2D.intensity += 0.1f;
                }
            }
        }
    }
}
