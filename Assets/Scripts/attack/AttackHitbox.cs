using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;

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
            if (tree != null)
            {
                Debug.Log("Tree found, applying damage");
                tree.TakeDamage(3);
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
