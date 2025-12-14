using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public static Sword Singleton;
    //public int maxHealth = 2;
    //private int currentHealth;

    //private void Start()
    //{
    //    Singleton = this;
    //    currentHealth = maxHealth;
    //}

    //public void TakeDamage(int amount)
    //{
    //    Debug.Log($"{currentHealth}");
    //    currentHealth -= amount;
    //    if (currentHealth <= 0)
    //    {
    //        var currslot = ItemEquipper.Singleton.CurrentSlot();
    //        Inventory.Singleton.ConsumeItem(currslot.myItem);
    //    }
    //}
    public void TakeDamage(InventoryItem weapon)
    {
        //Debug.Log($"BEFORE: {weapon.currentDurability}");
        weapon.currentDurability -= 1;
        //Debug.Log($"AFTER: {weapon.currentDurability}");
        if (weapon.currentDurability <= 0)
        {
            var currslot = ItemEquipper.Singleton.CurrentSlot();
            Inventory.Singleton.ConsumeItem(currslot.myItem);
        }
    }

    //public void ResetDurability()
    //{
    //    currentHealth = maxHealth;
    //}
}
