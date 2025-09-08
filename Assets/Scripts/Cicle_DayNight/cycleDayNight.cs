using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class cycleDayNight : MonoBehaviour, ISaveable
{
    [SerializeField] private float LightNight;
    public Light2D light2D;
    public float CurrentHours;
    public float CurrentMinutes;
    public float IncreseTime = 2f;
    public bool ActiveCicledayNight = true;
    public int CurrentDay = 0;

    private void Awake()
    {
        StartCoroutine(CicleDay());
    }

    private void Start()
    {
        SaveSystem.Instance.saveables.Add(this);
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
            if(CurrentHours >= 24f)
            {
                CurrentHours = 0f; // Reset hours after 24
                CurrentDay += 1; // Increment day count
            }
            if (CurrentHours == 8)
            {
                IncreseTime = 1f; 
                if (light2D.intensity > 1f) {
                    yield return new WaitForSeconds(0.5f);
                    light2D.intensity += 0.1f;
                }
            }
        }
    }

    public void save(GameData data)
    {
        data.currentHour = CurrentHours;
        data.currentMinute = CurrentMinutes;
        data.currentDay = CurrentDay;
    }

    public void load(GameData data)
    {
        CurrentHours = data.currentHour;
        CurrentMinutes = data.currentMinute;
        CurrentDay = data.currentDay;
    }
}
