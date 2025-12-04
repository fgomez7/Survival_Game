using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipper : MonoBehaviour
{
    public static ItemEquipper Singleton;

    public Transform handTransform; // where the item prefab spawns, not yet implemented
    private GameObject currentEquippedObject;
    private InventorySlot currentInvSlot;
    Item itemData;
    public Animator animator;

    void Awake()
    {
        Singleton = this;
        currentInvSlot = Inventory.Singleton.hotbarslots[0];
        animator.SetBool("HasWeapon", false);
        //currentInvSlot.SetHighlight(true);
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
            //itemData = null;
            //animator.SetBool("HasWeapon", false);
            return;
        }
        if (currentInvSlot == null)
        {
            currentInvSlot = Inventory.Singleton.hotbarslots[index];
        }
        currentInvSlot.SetHighlight(false);
        slot.SetHighlight(true);
        currentInvSlot = slot;

        itemData = currentInvSlot.myItem.myItem;
        Debug.Log(itemData.ToString());

        //itemData.heldPrefab = null;

        //player has a weapon -> enable attack animations
        bool isWeapon = (itemData.itemTag == SlotTag.Weapon);
        animator.SetBool("HasWeapon", isWeapon);
    }

    //public void ResetEquipped()
    //{
    //    currentInvSlot.SetHighlight(false);
    //    currentInvSlot = null;
    //    itemData = null;
    //}
    public Item CurrentEquippedItem()
    {
        return itemData;
    }
    public InventorySlot CurrentSlot()
    {
        return currentInvSlot;
    }
}
