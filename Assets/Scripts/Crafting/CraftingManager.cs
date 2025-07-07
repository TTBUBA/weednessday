using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public CraftingRecipe CurrentItemCraft;

    public InventoryManager InventoryManager;


    public void Craft()
    {
        foreach (var Sloot in InventoryManager.slootManager)
        {
            foreach (var ingrediet in CurrentItemCraft.ingredients)
            {
                if(Sloot.slootData == ingrediet.slootData)
                {
                    Sloot.CurrentStorage -= ingrediet.amount;
                    Sloot.UpdateSlot();
                    InventoryManager.AddItem(CurrentItemCraft.resultItem, 1);
                    Debug.Log(Sloot.name + ingrediet.slootData.name);
                }
            }
        }
    }
}
