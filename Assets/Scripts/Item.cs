using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public SlotTag itemTag = SlotTag.None;
}

public enum SlotTag { None, Head, Body, Weapon }
