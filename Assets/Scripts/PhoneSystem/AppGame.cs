using System.Collections;
using TMPro;
using UnityEngine;

public class AppGame : MonoBehaviour
{
    [SerializeField] private GameObject[] pointerSpawn;
    [SerializeField] private int TotalPoint;
    [SerializeField] private TextMeshProUGUI CounterClicker;
    [SerializeField] private AudioSource Ballon_Bop;


    private void OnEnable()
    {
        StartCoroutine(spawnobj());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator spawnobj()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int randomIndex = Random.Range(0, pointerSpawn.Length);
            for (int i = 0; i < pointerSpawn.Length; i++)
            {
                if (i == randomIndex)
                {
                    pointerSpawn[i].SetActive(true);
                }
                else
                {
                    pointerSpawn[i].SetActive(false);
                }
            }
        }
    }

    public void buttonClicker(int amount)
    {
        TotalPoint += amount;
        CounterClicker.text = TotalPoint.ToString();
        Ballon_Bop.Play();
    }
}
