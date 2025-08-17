using UnityEngine;

public class EffectFirtsKit : MonoBehaviour
{
    [SerializeField] private GameObject buttUseFirtsKit;
    [SerializeField] private bool IsUseFirtsKit = false;
    public PlayerHealth playerHealth; 
    public InventoryManager inventoryManager;
    // Update is called once per frame
    void Update()
    {
        if(inventoryManager.CurrentSlotSelect != null && inventoryManager.CurrentSlotSelect.NameTools == "firstkit")
        {
            buttUseFirtsKit.SetActive(true);
            IsUseFirtsKit = true;
        }
        else
        {
            buttUseFirtsKit.SetActive(false);
            IsUseFirtsKit = false;
        }
    }

    public void UseFirtsKit()
    {
        if(IsUseFirtsKit)
        {
            if (playerHealth != null)
            {
                playerHealth.IncreseHealth(15);
                inventoryManager.CurrentSlootManager.CurrentStorage--;
                inventoryManager.CurrentSlootManager.UpdateSlot();
            }
        }
    }
}
