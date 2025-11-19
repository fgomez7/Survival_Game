using System;
using UnityEngine;
using UnityEngine.InputSystem; // needed for InputValue
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    private float currentSpeed;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Health - Stamina - Hunger")]
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currHealth;
    public StaminaBar stamina;
    public int maxStamina = 100;
    public int currStamina = 100;
    public HungerBar hunger;
    public int currHunger = 100;
    public int maxHunger = 100;

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

        currHunger = maxHunger;
        hunger.SetMaxHunger(maxHunger);

        currentSpeed = moveSpeed;
        //____________CLOCK__________
        GlobalClock.Instance.OnTick += OnClockTick;

        animator = GetComponent<Animator>();

        // Ensure hitbox is OFF at the start
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    private void OnDestroy()
    {
        GlobalClock.Instance.OnTick -= OnClockTick;
    }
    void OnClockTick()
    {
        useHunger(1);
        Debug.Log("Tick recieved");
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
        if (moveInput != Vector2.zero)
        {
            if (currStamina > 0)
            {
                useStamina(1);
                currentSpeed = sprintSpeed;
            }
            else
            {
                currentSpeed = moveSpeed;
            }
        }
        else
        {
            refillStamina(2);
            currentSpeed = moveSpeed;
        }
        rb.velocity = moveInput * currentSpeed;
    }

    // === HEALTH ===
    void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        //currHunger -= damage;
        //hunger.SetHunger(currHunger);
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

    void useHunger(int hun)
    {
        if (currHunger > 0)
        {
            currHunger -= hun;
            currHunger = Mathf.Clamp(currHunger, 0, maxHunger);
            hunger.SetHunger(currHunger);
        }
        if (currHunger == 0)
        {
            TakeDamage(1);
        }
    }
}
