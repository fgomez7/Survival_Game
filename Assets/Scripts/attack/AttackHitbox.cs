using Unity.VisualScripting;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int handDamage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var equippedSlot = ItemEquipper.Singleton.CurrentSlot();

        Tree tree = other.GetComponent<Tree>();
        if (tree != null)
        {
            // Hand damage
            if (equippedSlot == null || equippedSlot.myItem == null)
            {
                tree.TakeDamage(handDamage);
                return;
            }

            InventoryItem invItem = equippedSlot.myItem;
            Item itemData = invItem.myItem;

            if (itemData.itemTag == SlotTag.Weapon)
            {
                int damage = 1 + itemData.treeDamageBonus;
                tree.TakeDamage(damage);
                ToolUtility.UseTool(invItem);
            }
            else
            {
                tree.TakeDamage(handDamage);
            }

            return;
        }
        StoneNode stone = other.GetComponent<StoneNode>();
        if (stone != null)
        {
            int damage = handDamage;

            if (equippedSlot != null && equippedSlot.myItem != null)
            {
                InventoryItem invItem = equippedSlot.myItem;
                Item itemData = invItem.myItem;

                if (itemData.itemTag == SlotTag.Weapon)
                {
                    damage = 1 + itemData.treeDamageBonus; // reuse for now
                    ToolUtility.UseTool(invItem);
                }
            }

            stone.TakeDamage(damage);
            return;
        }


        MushroomChaseEnemy enemy = other.GetComponent<MushroomChaseEnemy>();
        if (enemy != null)
        {
            if (equippedSlot == null || equippedSlot.myItem == null)
                return;

            InventoryItem invItem = equippedSlot.myItem;
            Item itemData = invItem.myItem;

            if (itemData.itemTag == SlotTag.Weapon)
            {
                enemy.Die();
                ToolUtility.UseTool(invItem);
            }
        }
    }
}





/* public class AttackHitbox : MonoBehaviour
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
*/ 