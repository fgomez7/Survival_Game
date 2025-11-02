using UnityEngine;

public class CraftingTester : MonoBehaviour
{
    public CraftingSystem craftingSystem;
    public RecipeData recipeToCraft;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftingSystem != null && recipeToCraft != null)
            {
                craftingSystem.CraftItem(recipeToCraft);
                Debug.Log("Craft button pressed!");
            }
            else
            {
                Debug.LogWarning("Crafting system or recipe not assigned!");
            }
        }
    }
}
