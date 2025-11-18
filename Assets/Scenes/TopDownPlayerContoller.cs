using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Debug = UnityEngine.Debug;

public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Health & Stamina")]
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currHealth;
    public StaminaBar stamina;
    public int maxStamina = 100;
    public int currStamina = 100;

    [Header("Attack Settings")]
    public GameObject attackHitbox;
    public float attackActiveTime = 0.2f;

    private bool isAttacking = false;
    private Animator animator;

    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currStamina = maxStamina;
        stamina.SetMaxStamina(maxStamina);

        animator = GetComponent<Animator>();

        // Ensure hitbox is OFF at the start
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // === MOVEMENT INPUT ===
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        // Testing damage (your old debug)
        if (moveInput.y > 0)
        {
            TakeDamage(5);
        }
    }

    // === ATTACK INPUT ===
    void OnAttack(InputValue value)
    {
        if (!isAttacking)
            StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        isAttacking = true;

        // play attack animation if you have one
        if (animator != null)
            animator.SetTrigger("Attack");

        // enable hitbox
        if (attackHitbox != null)
            attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackActiveTime);

        // disable hitbox
        if (attackHitbox != null)
            attackHitbox.SetActive(false);

        isAttacking = false;
    }

    private void FixedUpdate()
    {
        // Movement AND stamina logic
        if (currStamina > 0)
        {
            rb.velocity = 2 * moveInput * moveSpeed;
        }
        rb.velocity = moveInput * moveSpeed;

        if (rb.velocity != Vector2.zero)
        {
            useStamina(1);
        }
        else
        {
            refillStamina(2);
        }
    }

    // === HEALTH ===
    void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }

    // === STAMINA ===
    void useStamina(int stam)
    {
        if (currStamina > 0)
        {
            currStamina -= stam;
            stamina.SetStamina(currStamina);
        }
    }

    void refillStamina(int stam)
    {
        if (currStamina < maxStamina)
        {
            currStamina += stam;
            stamina.SetStamina(currStamina);
        }
    }
}
