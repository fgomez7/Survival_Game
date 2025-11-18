using UnityEngine;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
    [Header("Recipes & Inventory")]
    public List<RecipeData> availableRecipes;
    public Inventory inventory;  // Reference to the real Inventory on InventoryScreen

    public bool Craft(RecipeData recipe)
    {
        if (inventory == null)
        {
            Debug.LogError("❌ CraftingSystem has no Inventory reference!");
            return false;
        }

        if (recipe == null)
        {
            Debug.LogError("❌ No recipe provided!");
            return false;
        }

        // Check if player can craft this
        if (!CanCraft(recipe))
            return false;

        // 🔹 Remove first required item
        if (recipe.requiredItem1 != null && recipe.requiredAmount1 > 0)
            inventory.RemoveItem(recipe.requiredItem1, recipe.requiredAmount1);

        // 🔹 Remove second required item
        if (recipe.requiredItem2 != null && recipe.requiredAmount2 > 0)
            inventory.RemoveItem(recipe.requiredItem2, recipe.requiredAmount2);

        // 🔹 CraftingSystem NO LONGER spawns world items
        //     UI (CraftingMenuUI) handles all world spawning visuals

        Debug.Log($"✔ Crafted: {recipe.outputItem.name}");
        return true;
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
