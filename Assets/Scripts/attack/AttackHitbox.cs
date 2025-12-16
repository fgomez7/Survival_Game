using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int handDamage = 1;
    public Sword swordDurability;
    private bool hashit = false;
    private Collider2D hitbox;

    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
        //hitbox.enabled = false;
    }
    private void Start()
    {
        swordDurability = GameObject.FindGameObjectWithTag("Player")
                                    .GetComponent<Sword>();
    }

    private void OnEnable()
    {
        hashit = false;
    }

    //public void EnableHitBox()
    //{
    //    hashit = false;
    //    hitbox.enabled = true;
    //}

    //public void DisableHitbox()
    //{
    //    hitbox.enabled = false;
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hashit) return;
        hashit = true;
        var equippedSlot = ItemEquipper.Singleton.CurrentSlot();

        // ---------------- SAFETY CHECK ----------------
        //if (equippedSlot == null && equippedSlot.myItem == null)
        //    return;

        string currHand = "";
        if (equippedSlot != null && equippedSlot.myItem != null)
        {
            currHand = equippedSlot.myItem.myItem.name;
        }

        Tree tree = other.GetComponent<Tree>();
        StoneNode stone = other.GetComponent<StoneNode>();
        MushroomChaseEnemy mushroom = other.GetComponentInParent<MushroomChaseEnemy>();
        SkeletonChaseEnemy skeleton = other.GetComponentInParent<SkeletonChaseEnemy>();

        switch (currHand) {
            case "Sword":
                if (tree != null)
                {
                    tree.TakeDamage(2);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
                else if (stone != null)
                {
                    stone.TakeDamage(2);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
                else if (mushroom != null)
                {
                    mushroom.Die(3);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
                else if (skeleton != null)
                {
                    skeleton.Die(3);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
             break;
            case "Axe":
                if (tree != null)
                {
                    tree.TakeDamage(3);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
                else if (stone != null)
                {
                    stone.TakeDamage(2);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
                else if (mushroom != null)
                {
                    mushroom.Die(2);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
                else if (skeleton != null)
                {
                    skeleton.Die(2);
                    swordDurability.TakeDamage(equippedSlot.myItem);
                    return;
                }
             break;
            default:
                if (tree != null)
                {
                    tree.TakeDamage(1);
                    return;
                }
                else if (stone != null)
                {
                    stone.TakeDamage(1);
                    return;
                }
                else if (mushroom != null)
                {
                    mushroom.Die(1);
                    return;
                }
                else if (skeleton != null)
                {
                    skeleton.Die(1);
                    return;
                }
            break;
        }

        //    InventoryItem invItem = equippedSlot.myItem;
        //    Item itemData = invItem.myItem;

        //    // Only weapons trigger this hitbox
        //    if (itemData.itemTag != SlotTag.Weapon)
        //        return;

        //    // ---------------- TREE ----------------
        //    //Tree tree = other.GetComponent<Tree>();
        //    if (tree != null)
        //    {
        //        tree.TakeDamage(3);
        //        swordDurability.TakeDamage(invItem);
        //        return;
        //    }

        //    // ---------------- STONE ----------------
        //    //StoneNode stone = other.GetComponent<StoneNode>();
        //    if (stone != null)
        //    {
        //        stone.TakeDamage(2);              // stone-specific damage
        //        swordDurability.TakeDamage(invItem);
        //        return;
        //    }

        //    // ---------------- MUSHROOM ----------------
        //    //MushroomChaseEnemy mushroom = other.GetComponentInParent<MushroomChaseEnemy>();

        //    if (mushroom != null)
        //    {
        //        mushroom.Die();
        //        return;
        //    }

        //    // ---------------- SKELETON ----------------
        //    //SkeletonChaseEnemy skeleton = other.GetComponentInParent<SkeletonChaseEnemy>();

        //    if (skeleton != null)
        //    {
        //        skeleton.Die();
        //        return;
        //    }
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