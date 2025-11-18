using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hitbox touched: " + other.name);

        Tree tree = other.GetComponent<Tree>();
        if (tree != null)
        {
            Debug.Log("Tree found, applying damage");
            tree.TakeDamage(damage);
        }
    }

}
