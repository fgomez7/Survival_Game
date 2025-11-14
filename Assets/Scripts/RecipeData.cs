using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/RecipeData")]
public class RecipeData : ScriptableObject
{
    public string recipeName;

    public Item requiredItem1;
    public int requiredAmount1;

    public Item requiredItem2;
    public int requiredAmount2;

    public Item outputItem;
    public GameObject outputItemPrefab; // drag Stick prefab here
}