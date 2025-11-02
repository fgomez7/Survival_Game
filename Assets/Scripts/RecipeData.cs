using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Recipe Data")]
public class RecipeData : ScriptableObject
{
    public Item outputItem;
    public int outputQuantity = 1;

    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int quantity;
    }

    public Ingredient[] ingredients;
}
