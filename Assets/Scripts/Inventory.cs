using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;

//using System.Threading.Tasks.Dataflow;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;
    //public List<Item> storedItems = new List<Item>();
    public Item[] storedItems;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items; // assign Wood (0) and Stone (1) in Inspector

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    public HungerBar hunger;

    public InventorySlot[] hotbarslots;

    public int maxInventorySize = 28;

    void Awake()
    {
        Debug.Log($"[Inventory] Awake on: {gameObject.name}");

        Singleton = this;

        storedItems = new Item[maxInventorySize];

        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            //inventorySlots = GetComponentsInChildren<InventorySlot>(true);
            Debug.Log($"[Inventory] Auto-linked {inventorySlots.Length} inventory slots on {gameObject.name}");
        }

        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }


    void Start()
    {
        Debug.Log("🔍 Inventory.Start() called!");

        if (items != null && items.Length > 0)
        {
            Item wood = items[0];
            Item stone = items.Length > 1 ? items[1] : null;

            for (int i = 0; i < 10; i++)
                SpawnInventoryItem(wood);

            if (stone != null)
            {
                for (int i = 0; i < 10; i++)
                    SpawnInventoryItem(stone);
            }

            Debug.Log($"Starting resources added: 10 {wood.itemName}, 10 {(stone != null ? stone.itemName : "none")}");
            Debug.Log($"After setup: {CountAllItems()} items in inventory.");
        }

        // ⭐ FIX: Refresh crafting UI AFTER items have been spawned
        FindObjectOfType<CraftingMenuUI>()?.UpdateResourceDisplay();
    }


    // Spawns an inventory item into the next empty slot
    public void SpawnInventoryItem(Item item = null)
    {
        //Item _item = item;

        //if (_item == null && items.Length > 0)
        //    _item = items[Random.Range(0, items.Length)];

        //for (int i = 0; i < inventorySlots.Length; i++)
        //{
        //    if (inventorySlots[i].myItem == null)
        //    {
        //        //Debug.Log($"{inventorySlots[i].myItem == null}");
        //        var newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
        //        newItem.Initialize(_item, inventorySlots[i]);

        //        // // 👇 Add this right here
        //        Debug.Log($"Spawned {_item.itemName} into slot {i}. myItem set? {inventorySlots[i].myItem != null}");
        //        break;
        //        // Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
        //        // break;
        //    }
        //}

        // pick random item if none provided
        Item _item = item ?? (items.Length > 0 ? items[Random.Range(0, items.Length)] : null);
        if (_item == null)
        {
            Debug.LogWarning("[Inventory] spawninventoryItem called with no availabe item.");
            return;
        }

        //if (storedItems.Count >= maxInventorySize)
        //{
        //    Debug.Log("[Inventory] Cannot add item - inventory is full!");
        //    return;
        //}

        //storedItems.Add(_item);
        //Debug.Log($"[Inventory] Stored item: {_item.itemName}. Total stored: {storedItems.Count}");

        //only build ui if inventory panel is visible

        //if (InventoryUI.Singleton != null && InventoryUI.Singleton.isInventoryOpen)
        //{
        //    CreateUIItem(item);
        //}
        if (InventoryUI.Singleton != null)
        {
            CreateUIItem(item);
        }
    }

    public void RefreshInventoryUI()
    {
        //clear all ui
        foreach(var slot in inventorySlots)
        {
            if (slot.myItem != null)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
            }
        }

        //recreate ui

        for (int i = 0; i < storedItems.Length;i++)
        {
            //var item = inventorySlots[i];
            if (storedItems[i] != null)
            {
                CreateUIItem(storedItems[i], i);
            }
        }

        //foreach (var item in storedItems)
        //    item.
        //    CreateUIItem(item, item.activeSlot);
    }

    private void CreateUIItem(Item item, int indexSlot = -1)
    {
        if (indexSlot == -1)
        {
            Debug.Log("In created ui item");
            for (int i = 0; i < 28; i++)
            {
                Debug.Log($"{i}");
                if (storedItems[i] == null)
                {

                    //var newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                    //newItem.Initialize(item, inventorySlots[i]);
                    //Debug.Log($"{i}");
                    storedItems[i] = item;
                    //Debug.Log("hello there");
                    return;
                }
            }
        }
        else
        {
            var newItem = Instantiate(itemPrefab, inventorySlots[indexSlot].transform);
            newItem.Initialize(item, inventorySlots[indexSlot]);
            storedItems[indexSlot] = item;
        }
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
            carriedItem = item;
            carriedItem.canvasGroup.blocksRaycasts = false;
            item.transform.SetParent(draggablesTransform);
            return;
        }

        //if (item.activeSlot.myTag != SlotTag.None){
        //    EquipEquipment(item.activeSlot.myTag, null);
        //}
        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
        carriedItem.activeSlot.SetHighlight(false);
    }
    
    public void DropItem( InventoryItem item, Transform playerTransform )
    {
        if (item == null)
        {
            return;
        }

        item.activeSlot.SetHighlight(false);
        //ItemEquipper.Singleton.ResetEquipped();
        item.activeSlot.myItem = null;

        RemoveStoredItem(item.myItem, item.activeSlot.slotIndex);
        Destroy(item.gameObject);

        Item itemData = item.myItem;
        if ( itemData != null && itemData.worldPrefab != null )
        {
            UnityEngine.Vector3 dropPosition = playerTransform.position + playerTransform.right * 1.0f;
            Instantiate(itemData.worldPrefab, dropPosition, UnityEngine.Quaternion.identity);
        }
    }

    public void ConsumeItem(InventoryItem item)
    {
        if (item == null)
        {
            return;
        }

        Item itemData = item.myItem;
        if (itemData == null || itemData.itemTag != SlotTag.Consumable)
        {
            Debug.LogWarning("Tried to consume a non-consumable item.");
            return;
        }
        item.activeSlot.SetHighlight(false);
        //ItemEquipper.Singleton.ResetEquipped();
        item.activeSlot.myItem = null;
        RemoveStoredItem(item.myItem, item.activeSlot.slotIndex);

        Destroy(item.gameObject);

        //hunger.SetHunger(hunger.returnCurrHunger() + 5);
        hunger.updateHunger(33);


    }
    public void RemoveStoredItem(Item item, int itemindex)
    {
        //if (storedItems.Contains(item))
        //{
        //    storedItems.Remove(item);
        //}
        storedItems[itemindex] = null;
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
    Debug.Log($"🔍 [HasItem] Checking for: {item.name} (ID: {item.GetInstanceID()})");

    foreach (var slot in inventorySlots)
    {
        if (slot.myItem != null)
        {
            Item slotItem = slot.myItem.myItem;
            Debug.Log($"   → Slot has {slotItem.name} (ID: {slotItem.GetInstanceID()})");

            // Compare by reference (same object in memory)
            if (slotItem == item)
                count++;
        }
        else
        {
            Debug.Log("   → Slot empty");
        }
    }

    Debug.Log($"[HasItem] Result: Found {count}, Need {quantity}");
    return count >= quantity;
    }
    // ✅ Returns how many of a specific Item the player has
    public int GetItemCount(Item item)
    {
        int count = 0;

        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem == item)
            {
                count++;
            }
        }

        return count;
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
    public Item GetItemAsset(int index)
    {
        return items[index];
    }

}