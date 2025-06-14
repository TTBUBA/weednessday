using TMPro;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public static WateringCan Instance { get; private set; }
    public float waterAmount = 1f;
    [SerializeField] private TextMeshProUGUI Text_CurrentWater;

    public void Fill(float amount)
    {
        waterAmount += amount;
    }

    public void Use(float amount)
    {
        waterAmount -= amount;
    }

    private void Update()
    {
        Text_CurrentWater.text = waterAmount.ToString();
    }
}
