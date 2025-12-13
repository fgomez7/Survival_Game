using Unity.VisualScripting;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;
    public Sword swordDurability;

    private void Start()
    {
        swordDurability = GameObject.FindGameObjectWithTag("Player").GetComponent<Sword>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hitbox touched: " + other.name);

        // USE SWITCH STATEMENT TO COMPARE ITEM EQUIPPED
        var equippedSlot = ItemEquipper.Singleton.CurrentSlot();
        if (equippedSlot == null)
        {
            Debug.Log("something happened");
        }
        var tree = other.GetComponent<Tree>();
        if (equippedSlot.myItem != null && equippedSlot.myItem.myItem.itemTag == SlotTag.Weapon)
        {
            
            //ItemEquipper.Singleton.swordDurability.TakeDamage(1);
            //sword.TakeDamage(1);
            //if (equippedSlot.myItem.myItem == null)
            //{
            //    Debug.Log("i'm not sure why this is null");
            //}
            if (tree != null)
            {
                Debug.Log("Tree found, applying damage");
                tree.TakeDamage(3);
                swordDurability.TakeDamage(equippedSlot.myItem);
                return;
            }
        }
        var enemy = other.GetComponent<MushroomChaseEnemy>();
        if (enemy != null)
        {
            Debug.Log("Enemy hit! Applying damage / triggering death.");

            enemy.Die();   // <- Call your enemy death function
            return;
        }
        // USE SWITCH STATEMENT TO COMPARE OBJECTS HIT
        //var tree = other.GetComponent<Tree>();
        if (tree != null)
        {
            Debug.Log($"Tree found, applying damage WITH HAND: {damage}");
            tree.TakeDamage(1);
        }
    }

}
