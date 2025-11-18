using UnityEngine;

public class Tree : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Drop Settings")]
    public GameObject woodDropPrefab;   // what item drops
    public int woodAmount = 2;          // how many pieces of wood

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Tree damaged! HP = {currentHealth}");

        if (currentHealth <= 0)
        {
            ChopDown();
        }
    }

    private void ChopDown()
    {
        Debug.Log("🌲 Tree chopped down!");

        // Spawn wood pieces
        for (int i = 0; i < woodAmount; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-0.3f, 0.3f),
                Random.Range(-0.2f, 0.2f),
                0
            );

            Instantiate(woodDropPrefab, transform.position + offset, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
