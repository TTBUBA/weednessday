using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Image BarLife;

    private void Update()
    {
        BarLife.fillAmount = (health / 100f);
    }
    public int DecreseHealth(int life)
    {
        if(health > 1)
        {
           health -= life;

        }
        return health;
    }

    public int IncreseHealth(int life)
    {
        if (health < 100)
        {
            health += life;
        }
        return health;
    }
}
