using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public SlotTag itemTag = SlotTag.None;

    [Header("World (optional)")]
    [Tooltip("If assigned, this prefab will be instantiated when the item is dropped into the world")]
    public GameObject worldPrefab;
}

public enum SlotTag { None, Head, Body, Weapon, Consumable }
