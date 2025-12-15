using UnityEngine;

public class SkeletonChaseEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Combat")]
    public int damageAmount = 10;
    public float damageCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isDead = false;
    private float lastDamageTime;
    private int health;

    void Start()
    {
        health = 6;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        // Prevent spinning
        rb.freezeRotation = true;

        // Safety reset
        sr.color = Color.white;
    }

    void Update()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("IsRunning", false);
            return;
        }

        if (player == null)
        {
            anim.SetBool("IsRunning", false);
            rb.velocity = Vector2.zero;
            return;
        }

        ChasePlayer();
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position);

        // If extremely close, stop jittering
        if (direction.magnitude < 0.1f)
        {
            anim.SetBool("IsRunning", false);
            rb.velocity = Vector2.zero;
            return;
        }

        direction.Normalize();

        anim.SetBool("IsRunning", true);
        rb.velocity = direction * moveSpeed;

        // Flip horizontally only
        sr.flipX = direction.x < 0;
    }

    // Damage player while touching
    void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player") &&
            Time.time > lastDamageTime + damageCooldown)
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }

    // Called by weapon hitbox
    public void Die(int damage)
    {
        if (isDead) return;
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        }
        else
        {
            return;
        }
        Debug.Log($"skeleton {damage} and current {health}");

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        anim.SetBool("IsRunning", false);
        anim.SetTrigger("Die");

        Destroy(gameObject, 1f); // match die animation length
    }
}
