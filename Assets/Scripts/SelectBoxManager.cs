using UnityEngine;

public class SelectBoxManager : MonoBehaviour
{
    public PlantManager PlantManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Plant"))
        {
            PlantManager.plant = collision.GetComponent<Plant>();
        }
        else
        {
            PlantManager.plant = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            PlantManager.plant = null;
        }
    }
}

