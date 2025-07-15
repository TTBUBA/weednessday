using System.Text;
using TMPro;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public static WateringCan Instance { get; private set; }


    public int waterAmount = 100;
    public TextMeshProUGUI Text_CurrentWater;

    public void Awake()
    {
        Text_CurrentWater.text = waterAmount.ToString() + "%";
    }

    public void Fill(int amount)
    {
        waterAmount += amount;
    }

    public void Use(int amount)
    {
        waterAmount -= amount;
    }
}
