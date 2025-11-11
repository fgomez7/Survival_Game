using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items; // assign Wood (0) and Stone (1) in Inspector

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    void Awake()
    {
    Singleton = this;

    // Auto-fill if not set in Inspector
    if (inventorySlots == null || inventorySlots.Length == 0)
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>(true);
        Debug.Log($"[Inventory] Auto-populated inventorySlots: {inventorySlots.Length}");
    }

    giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }

    void Start()
    {
        Debug.Log("🔍 Inventory.Start() called!");

        if (items != null && items.Length > 0)
        {
            // Give 10 Wood (items[0]) and 10 Stone (items[1])
            Item wood = items[0];
            Item stone = items.Length > 1 ? items[1] : null;

            for (int i = 0; i < 10; i++)
                SpawnInventoryItem(wood);

            if (stone != null)
            {
                for (int i = 0; i < 10; i++)
                    SpawnInventoryItem(stone);
            }

            Debug.Log($"✅ Starting resources added: 10 {wood.itemName}, 10 {(stone != null ? stone.itemName : "none")}");
            Debug.Log($"📦 After setup: {CountAllItems()} items in inventory.");
        }
        else
        {
            Debug.LogWarning("⚠️ Inventory 'items' array is empty. Please assign your items (Wood, Stone, etc.) in the Inspector!");
        }
    }

    // Spawns an inventory item into the next empty slot
    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;

        if (_item == null && items.Length > 0)
            _item = items[Random.Range(0, items.Length)];

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                var newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                newItem.Initialize(_item, inventorySlots[i]);
                break;
            }
        }
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != SlotTag.None &&
                item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.myTag != SlotTag.None)
            EquipEquipment(item.activeSlot.myTag, null);

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.None:
                break;
        }
    }

    // ✅ Check if player has enough of a specific item
    public bool HasItem(Item item, int quantity)
    {
        int count = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem.itemName == item.itemName)
                count++;
        }

        Debug.Log($"🔍 Checking inventory for {item.itemName}: found {count}, need {quantity}");
        Debug.Log($"🔍 Checking for {item.itemName}: Found {count} / Need {quantity}");
        return count >= quantity;
    }

    // ✅ Remove items from slots
    public void RemoveItem(Item item, int quantity)
    {
        int removed = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem.itemName == item.itemName)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
                removed++;
                if (removed >= quantity) return;
            }
        }
    }

    // ✅ Add crafted item to next available slot
    public void AddItem(Item item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            for (int j = 0; j < inventorySlots.Length; j++)
            {
                if (inventorySlots[j].myItem == null)
                {
                    var newItem = Instantiate(itemPrefab, inventorySlots[j].transform);
                    newItem.Initialize(item, inventorySlots[j]);
                    break;
                }
            }
        }
    }

    // ✅ Debug helper
    public int CountAllItems()
    {
        int count = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null)
                count++;
        }
        return count;
    }

    void Update()
    {
        if (carriedItem == null) return;
        carriedItem.transform.position = Input.mousePosition;
    }
}