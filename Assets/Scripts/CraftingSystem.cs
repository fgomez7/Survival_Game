using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<RecipeData> availableRecipes;
    public Inventory inventory; // Reference to your inventory system

    public void CraftItem(RecipeData recipe)
    {
        if (CanCraft(recipe))
        {
            // Remove ingredients
            foreach (var ingredient in recipe.ingredients)
            {
                inventory.RemoveItem(ingredient.item, ingredient.quantity);
            }

            // Add crafted item
            inventory.AddItem(recipe.outputItem, recipe.outputQuantity);
            Debug.Log($"Crafted {recipe.outputQuantity}x {recipe.outputItem.itemName}");
        }
        else
        {
            Debug.Log("Not enough resources!");
        }
    }

    private bool CanCraft(RecipeData recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            if (!inventory.HasItem(ingredient.item, ingredient.quantity))
                return false;
        }
        return true;
    }
}
