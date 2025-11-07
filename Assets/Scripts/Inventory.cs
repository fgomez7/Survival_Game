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
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    void Awake()
    {
        Singleton = this;

        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }


    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;

        // ✅ Fix: Only pick a random item if no item was passed in
        if (_item != null)
        {
            int random = Random.Range(0, items.Length);
            _item = items[random];
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                //var newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                //RectTransform rect = newItem.GetComponent<RectTransform>();
                //rect.anchoredPosition = Vector2.zero;
                //rect.offsetMin = Vector2.zero;
                //rect.offsetMax = Vector2.zero;
                //rect.localScale = Vector3.one;
                //newItem.Initialize(_item, inventorySlots[i]);
                //break;
            }
        }
    }


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
            if (carriedItem == null) return;
            carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null )
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.myTag != SlotTag.None)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }
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
    // Check if player has enough of a specific item (by name)
    // Check if player has enough of a specific item (by ScriptableObject)
    public bool HasItem(Item item, int quantity)
    {
        int count = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null && slot.myItem.myItem.itemName == item.itemName)
            {
                count++;
            }
        }
        return count >= quantity;
    }

    // Remove items from slots
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

    // Add crafted item to next available slot
    public void AddItem(Item item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            for (int j = 0; j < inventorySlots.Length; j++)
            {
                if (inventorySlots[j].myItem == null)
                {
                    // Instantiate prefab
                    var newItem = Instantiate(itemPrefab, inventorySlots[j].transform);

                    // ✅ Center and scale properly inside the slot
                    //RectTransform rect = newItem.GetComponent<RectTransform>();
                    //rect.anchoredPosition = Vector2.zero;
                    //rect.offsetMin = Vector2.zero;
                    //rect.offsetMax = Vector2.zero;
                    //rect.localScale = Vector3.one;

                    // Initialize item visuals
                    newItem.Initialize(item, inventorySlots[j]);
                    break;
                }
            }
        }
    }
    void Start()
    {
        // Make sure you have items assigned in the Inspector list
        //if (items.Length > 0)
        //{
        //    // This assumes Wood is the first item in your list
        //    SpawnInventoryItem(items[0]);
        //    SpawnInventoryItem(items[1]);
        //    SpawnInventoryItem(items[2]);
        //}
    }

}
