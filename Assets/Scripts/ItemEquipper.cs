using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipper : MonoBehaviour
{
    public static ItemEquipper Singleton;

    public Transform handTransform; // where the item prefab spawns, not yet implemented
    private GameObject currentEquippedObject;

    void Awake()
    {
        Singleton = this;
        
    }

    public void EquipFromHotbar(int index)
    {
        //Debug.Log(index);
        var hotbar = Inventory.Singleton.HotbarSlots;
        var slot = hotbar[index];

        if (currentEquippedObject != null)
        {
            Destroy(currentEquippedObject);
        }

        if (slot.myItem == null)
        {
            return;
        }

        Item itemData = slot.myItem.myItem;
        Debug.Log(itemData.ToString());

        //itemData.heldPrefab = null;
    }
}
