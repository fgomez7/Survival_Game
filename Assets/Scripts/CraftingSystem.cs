using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CraftingSystem : MonoBehaviour
{
    [Header("Recipe & Inventory Links")]
    public List<RecipeData> availableRecipes;
    public Inventory inventory; // Reference to the real Inventory on InventoryScreen

    [Header("World Drop Settings")]
    public Transform dropPoint; // Where crafted items appear (drag a Transform near the house)

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

        if (CanCraft(recipe))
        {
            // ✅ Remove required materials
            if (recipe.requiredItem1 != null && recipe.requiredAmount1 > 0)
                inventory.RemoveItem(recipe.requiredItem1, recipe.requiredAmount1);

            if (recipe.requiredItem2 != null && recipe.requiredAmount2 > 0)
                inventory.RemoveItem(recipe.requiredItem2, recipe.requiredAmount2);


            // ✅ Spawn prefab in world (next to house)
            if (recipe.outputItemPrefab != null)
            {
                Vector3 spawnPos = dropPoint != null ? dropPoint.position : transform.position;
                GameObject newItem = Instantiate(recipe.outputItemPrefab, spawnPos, Quaternion.identity);
                Debug.Log($"🌲 Crafted {recipe.outputItem.name} and spawned in world at {spawnPos}");
            }
            else
            {
                Debug.Log($"🪓 Crafted {recipe.outputItem.name} (no prefab to spawn)");
            }

            return true;
        }

        Debug.Log($"⚠️ Not enough resources to craft {recipe.recipeName}");
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