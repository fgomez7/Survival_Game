using UnityEngine;

public class StoneNode : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Drops")]
    public GameObject stoneDropPrefab;
    public int minDrops = 2;
    public int maxDrops = 4;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            BreakStone();
        }
    }

    private void BreakStone()
    {
        int drops = Random.Range(minDrops, maxDrops + 1);

        for (int i = 0; i < drops; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.5f;
            Instantiate(stoneDropPrefab, (Vector2)transform.position + offset, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
