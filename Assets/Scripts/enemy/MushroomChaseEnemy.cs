using UnityEngine;

public class MushroomChaseEnemy : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    private Transform player;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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

        // Direction toward player
        Vector2 direction = (player.position - transform.position).normalized;

        // Movement
        rb.velocity = direction * moveSpeed;

        // Flip based on horizontal direction
        if (direction.x > 0)
            sr.flipX = false;   // face right
        else if (direction.x < 0)
            sr.flipX = true;    // face left
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        rb.velocity = Vector2.zero;
        anim.Play("MushroomDie");

        Destroy(gameObject, 0.8f);
    }
}
