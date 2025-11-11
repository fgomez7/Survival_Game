using UnityEngine;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    public List<RecipeData> availableRecipes;
    public Inventory inventory; // Reference to your inventory system

    public bool Craft(RecipeData recipe)
    {
        if (CanCraft(recipe))
        {
            // Remove required materials
            if (recipe.requiredItem1 != null && recipe.requiredAmount1 > 0)
                inventory.RemoveItem(recipe.requiredItem1, recipe.requiredAmount1);

            if (recipe.requiredItem2 != null && recipe.requiredAmount2 > 0)
                inventory.RemoveItem(recipe.requiredItem2, recipe.requiredAmount2);

            // Add crafted item
            inventory.AddItem(recipe.outputItem, 1); // assuming 1 crafted per recipe
            Debug.Log("Crafted: " + recipe.outputItem.name);
            return true;
        }

        Debug.Log("Not enough resources to craft " + recipe.name);
        return false;
    }

    private bool CanCraft(RecipeData recipe)
    {
        bool hasFirst = true;
        bool hasSecond = true;

        if (recipe.requiredItem1 != null && recipe.requiredAmount1 > 0)
            hasFirst = inventory.HasItem(recipe.requiredItem1, recipe.requiredAmount1);

        if (recipe.requiredItem2 != null && recipe.requiredAmount2 > 0)
            hasSecond = inventory.HasItem(recipe.requiredItem2, recipe.requiredAmount2);

        return hasFirst && hasSecond;
    }
}