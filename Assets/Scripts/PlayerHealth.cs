using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        // Set starting health
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // TEMP: Press H to take damage (for testing)
        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     TakeDamage(10);
        // }

        // // TEMP: Press J to heal
        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     Heal(10);
        // }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died!");
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player has died!");
        // Add death animation or respawn later
    }
}
