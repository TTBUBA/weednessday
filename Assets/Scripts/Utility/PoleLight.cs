using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//Active and disactive Light in determined hour in the day
public class PoleLight : MonoBehaviour
{
    [SerializeField] private Light2D poleLight;

    [SerializeField] private cycleDayNight Clock;

    private void Start()
    {
        StartCoroutine(ActiveLight());
    }

    IEnumerator ActiveLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(Clock != null)
            {
                if (Clock.CurrentHours >= 19)
                {
                    poleLight.enabled = true;
                }
                if (Clock.CurrentHours == 8)
                {
                    poleLight.enabled = false;
                }
            }
        }
    }

    public void SetData(cycleDayNight cycleDayNight)
    {
        Clock = cycleDayNight;
    }
}

