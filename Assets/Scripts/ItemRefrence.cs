using UnityEngine;

[DisallowMultipleComponent]
public class ItemReference : MonoBehaviour
{
    [Header("Linked Item Data")]
    public Item item; // Drag your Item or ItemData ScriptableObject here

    // Optional helper function
    public string GetItemName()
    {
        return item != null ? item.itemName : "Unnamed Item";
    }
}