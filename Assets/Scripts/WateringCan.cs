using TMPro;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public static WateringCan Instance { get; private set; }
    public int waterAmount = 100;
    [SerializeField] private TextMeshProUGUI Text_CurrentWater;

    public void Fill(int amount)
    {
        waterAmount += amount;
    }

    public void Use(int amount)
    {
        waterAmount -= amount;
    }

    private void Update()
    {
        Text_CurrentWater.text = waterAmount.ToString() + "%";
    }
}
