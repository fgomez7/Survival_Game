using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuUI : MonoBehaviour
{
    public CraftingSystem craftingSystem;
    public RecipeData[] availableRecipes;

    [Header("Buttons")]
    public Button craftStickButton;
    public Button craftBoxButton;
    public Button craftSwordButton;
    public Button craftAxeButton;

    void Start()
    {
        craftStickButton.onClick.AddListener(() => CraftItem(availableRecipes[0]));
        craftBoxButton.onClick.AddListener(() => CraftItem(availableRecipes[1]));
        craftSwordButton.onClick.AddListener(() => CraftItem(availableRecipes[2]));
        craftAxeButton.onClick.AddListener(() => CraftItem(availableRecipes[3]));
    }

    void CraftItem(RecipeData recipe)
    {
        if (craftingSystem != null && recipe != null)
        {
            craftingSystem.CraftItem(recipe);
        }
        else
        {
            Debug.LogWarning("Crafting system or recipe not assigned!");
        }
    }
}
