using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Tree tree = other.GetComponent<Tree>();
        if (tree != null)
        {
            tree.TakeDamage(damage);
        }
    }
}
