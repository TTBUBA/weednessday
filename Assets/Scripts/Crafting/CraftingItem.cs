using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingItem : MonoBehaviour, IPointerClickHandler 
{
    public CraftingRecipe ItemCraft;
    public Image IconItem;
    public List<TextMeshProUGUI> TextItem;
    public TextMeshProUGUI NameItem;

    [Header("References")]
    public CraftingManager CraftingManager;

    private void Start()
    {
        IconItem.sprite = ItemCraft.IconItem;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CraftingManager.CurrentItemCraft = ItemCraft;
        NameItem.text = ItemCraft.NameRecipe;
        foreach (var item in ItemCraft.ingredients)
        {
            for (int i = 0; i < TextItem.Count; i++)
            {
                if (i < ItemCraft.ingredients.Count)
                {
                    TextItem[i].text = item.slootData.name + " x " + item.amount;
                }
                else
                {
                    TextItem[i].text = "";
                }
            }
            Debug.Log(item.slootData.name + " " + item.amount);
        }
    }

}
