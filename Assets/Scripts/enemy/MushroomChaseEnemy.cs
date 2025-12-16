using JetBrains.Annotations;
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
    private int health;

    void Start()
    {
        health = 6;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        health = 6;
    }

    void Update()
    {
        if (isDead) return;

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
        Vector2 direction = (player.position - transform.position).normalized;

        // If too small to move (very close to player)
        if (direction.magnitude < 0.1f)
        {
            anim.SetBool("IsRunning", false);
            rb.velocity = Vector2.zero;
            return;
        }

        anim.SetBool("IsRunning", true);
        rb.velocity = direction * moveSpeed;

        // Flip sprite
        sr.flipX = direction.x > 0;
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

    // CALLED WHEN PLAYER ATTACK HITS ENEMY
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

        // Stop physics
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // optional

        // Stop run animation
        anim.SetBool("IsRunning", false);

        // 🔥 Trigger death animation
        anim.SetTrigger("Die");

        // Destroy after animation
        Destroy(gameObject, 1f); // match animation length
        TopDownPlayerController.Singleton.ObjectiveList(2);
    }
}
