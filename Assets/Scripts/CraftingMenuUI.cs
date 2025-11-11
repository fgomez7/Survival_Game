using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuUI : MonoBehaviour
{
    [Header("Crafting System Reference")]
    public CraftingSystem craftingSystem;

    [Header("Available Recipes")]
    public RecipeData[] availableRecipes;

    [Header("Buttons")]
    public Button craftStickButton;
    public Button craftBoxButton;
    public Button craftSwordButton;
    public Button craftAxeButton;

    [Header("Spawn Settings")]
    public Transform spawnPoint; // drag your ItemSpawnPoint here
    public float spawnRadius = 1f; // small random offset so items don’t overlap

    private void Start()
    {
        // Link button clicks to craft attempts
        craftStickButton.onClick.AddListener(() => TryCraft(availableRecipes[0]));
        craftBoxButton.onClick.AddListener(() => TryCraft(availableRecipes[1]));
        craftSwordButton.onClick.AddListener(() => TryCraft(availableRecipes[2]));
        craftAxeButton.onClick.AddListener(() => TryCraft(availableRecipes[3]));
    }

    private void TryCraft(RecipeData recipe)
    {
        bool success = craftingSystem.Craft(recipe);

        if (success)
        {
            Debug.Log("Crafted: " + recipe.name);
            SpawnCraftedItem(recipe.outputItemPrefab);
        }
        else
        {
            Debug.Log("Not enough resources to craft " + recipe.name);
        }
    }

    private void SpawnCraftedItem(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("No prefab assigned to this recipe!");
            return;
        }

        // Small random offset around the spawn point
        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(offset.x, offset.y, 0);

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}