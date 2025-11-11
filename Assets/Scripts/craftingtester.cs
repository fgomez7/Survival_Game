using UnityEngine;

public class CraftingTester : MonoBehaviour
{
    public CraftingSystem craftingSystem; // drag in Inspector
    public RecipeData recipeToCraft;      // drag a recipe asset here (e.g., WoodToStick)

    void Update()
    {
        // Press C to test crafting the selected recipe
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool crafted = craftingSystem.Craft(recipeToCraft);
            Debug.Log("Tried to craft: " + recipeToCraft.name + " -> " + (crafted ? "Success" : "Failed"));
        }
    }
}