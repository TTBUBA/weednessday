using UnityEngine;

public class PoliceHealth : MonoBehaviour
{    
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
