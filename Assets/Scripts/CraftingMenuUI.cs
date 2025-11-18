using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button craftStoneBoxButton;

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public float spawnRadius = 1f;


    [Header("Resource UI")]
    public TextMeshProUGUI resourceDisplayText;
    public Item woodItem;                // drag Item_Wood here
    public Item stoneItem;               // drag Item_Stone here
    public Item stickItem;               // drag Item_Stick here


    private void Start()
    {
        UpdateResourceDisplay();
        craftStickButton.onClick.AddListener(() => TryCraft(availableRecipes[0]));
        craftBoxButton.onClick.AddListener(() => TryCraft(availableRecipes[1]));
        craftSwordButton.onClick.AddListener(() => TryCraft(availableRecipes[2]));
        craftAxeButton.onClick.AddListener(() => TryCraft(availableRecipes[3]));
        craftStoneBoxButton.onClick.AddListener(() => TryCraft(availableRecipes[4]));
    }

    private void TryCraft(RecipeData recipe)
    {
        bool success = craftingSystem.Craft(recipe);

        if (success)
        {
            Debug.Log("Crafted: " + recipe.name);
            SpawnCraftedItem(recipe.outputItemPrefab);

            // Update counts after crafting
            UpdateResourceDisplay();
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

        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(offset.x, offset.y, 0);

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public void UpdateResourceDisplay()
    {
        int wood = Inventory.Singleton.GetItemCount(woodItem);
        int stone = Inventory.Singleton.GetItemCount(stoneItem);
        int sticks = Inventory.Singleton.GetItemCount(stickItem);

        resourceDisplayText.text =
            $"Wood = {wood}     Stone = {stone}     Sticks = {sticks}";

        // AUTO-DISABLE CRAFT BUTTONS BASED ON RESOURCE REQUIREMENTS

        // Craft Stick: requires 2 Wood
        craftStickButton.interactable = wood >= 2;

        // Craft Box: requires 3 Wood
        craftBoxButton.interactable = wood >= 3;

        // Craft Sword: requires 1 Stick (Plank) + 2 Stone
        craftSwordButton.interactable = (sticks >= 1) && (stone >= 2);

        // Craft Axe: requires 1 Stick (Plank) + 3 Stone
        craftAxeButton.interactable = (sticks >= 1) && (stone >= 3);

        // Craft Stone Box: requires 3 Stone + 1 Wood
        craftStoneBoxButton.interactable = (stone >= 3) && (wood >= 1);

        Debug.Log("WOOD COUNT = " + Inventory.Singleton.GetItemCount(woodItem));
        Debug.Log("STONE COUNT = " + Inventory.Singleton.GetItemCount(stoneItem));
        Debug.Log("STICK COUNT = " + Inventory.Singleton.GetItemCount(stickItem));

    }


}
