using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipper : MonoBehaviour
{
    public static ItemEquipper Singleton;
    //public Sword swordDurability;

    public Transform handTransform; // where the item prefab spawns, not yet implemented
    private GameObject currentEquippedObject;
    public InventorySlot currentInvSlot;
    public Item itemData;
    public Animator animator;

    IEnumerator Start()
    {
        Debug.Log("itemequipper started");
        Singleton = this;
        yield return null;
        currentInvSlot = Inventory.Singleton.hotbarslots[0];
        itemData = currentInvSlot.myItem.myItem;
        //EquipFromHotbar(0);
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

        //if (currentInvSlot == null)
        //{
        //    currentInvSlot = slot;
        //    if (slot != null) {
        //        Debug.Log("yeah, it's not null");
        //    }
        //}
        if (slot.myItem == null)
        {
            //itemData = null;
            //animator.SetBool("HasWeapon", false);
            return;
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
        //if (isWeapon)
        //{
        //    swordDurability.ResetDurability();
        //}
    }

    public void ResetEquipped(int itemSlot = -1)
    {
        if (currentInvSlot.slotIndex == itemSlot)
        {
            currentInvSlot.SetHighlight(false);
            itemData = null;
            animator.SetBool("HasWeapon", false);
        }
        //currentInvSlot = null;
        //itemData = null;
        //if (itemData != null)
        //{
        //    Debug.Log("it is not null");
        //}
    }
    public Item CurrentEquippedItem()
    {
        return itemData;
    }
    public InventorySlot CurrentSlot()
    {
        return currentInvSlot;
    }
}
