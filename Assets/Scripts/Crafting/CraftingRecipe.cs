using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Scriptable Objects/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public string NameRecipe;
    public Sprite IconItem;
    public List<string> IngredientsList;
    public List<CraftingIngredient> ingredients;
    public SlootData resultItem;
}

[System.Serializable]
public class CraftingIngredient
{
    public SlootData slootData;
    public int amount;
    public CraftingIngredient(SlootData slootData, int amount)
    {
        this.slootData = slootData;
        this.amount = amount;
    }
}
