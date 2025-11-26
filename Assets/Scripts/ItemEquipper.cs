using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipper : MonoBehaviour
{
    public static ItemEquipper Singleton;

    public Transform handTransform; // where the item prefab spawns, not yet implemented
    private GameObject currentEquippedObject;
    InventorySlot currentInvSlot;
    public Item itemData;

    void Awake()
    {
        Singleton = this;
        currentInvSlot = Inventory.Singleton.hotbarslots[0];
        currentInvSlot.SetHighlight(true);
    }

    public void EquipFromHotbar(int index)
    {
        //Debug.Log(index);
        //var hotbar = Inventory.Singleton.HotbarSlots;
        var hotbar = Inventory.Singleton.hotbarslots;
        var slot = hotbar[index];
        //slot.SetHighlight(false);


        if (currentEquippedObject != null)
        {
            Destroy(currentEquippedObject);
        }

        if (slot.myItem == null)
        {
            return;
        }

        currentInvSlot.SetHighlight(false);
        slot.SetHighlight(true);
        currentInvSlot = slot;

        itemData = slot.myItem.myItem;
        Debug.Log(itemData.ToString());

        //itemData.heldPrefab = null;
    }

    public Item CurrentEquippedItem()
    {
        return itemData;
    }
}
