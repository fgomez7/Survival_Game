using UnityEngine;

public class MushroomChaseEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2.5f;
    private Transform player;

    [Header("Damage Settings")]
    public int damageAmount = 10;
    public float damageCooldown = 1f;
    private float lastDamageTime = 0f;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isDead = false;

    void Start()
    {
        // Find player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();   // Animator is on Graphics child
        sr = GetComponentInChildren<SpriteRenderer>(); // SpriteRenderer is also on child
    }

    void Update()
    {
        if (isDead) return;
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (player == null) return;

        anim.Play("Mushroom_Run");

        // Direction to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move toward player
        rb.velocity = direction * moveSpeed;

        // Flip based on direction
        if (direction.x > 0)
            sr.flipX = false;
        else if (direction.x < 0)
            sr.flipX = true;
    }

    // DAMAGE PLAYER WHEN TOUCHING
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time > lastDamageTime + damageCooldown)
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                lastDamageTime = Time.time;
            }
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Stop movement
        rb.velocity = Vector2.zero;

        // Play death animation
        anim.Play("MushroomDie");

        // Destroy after animation
        Destroy(gameObject, 0.8f);
    }
}
