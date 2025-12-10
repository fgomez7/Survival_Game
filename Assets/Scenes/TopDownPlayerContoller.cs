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

    private int currentclock;

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
        currentclock = 0;
    }

    private void OnDestroy()
    {
        GlobalClock.Instance.OnTick -= OnClockTick;
    }
    void OnClockTick()
    {
        //useHunger(-1);
        hunger.updateHunger(-1);
        //Debug.Log("Tick recieved");

        if (hunger.returnCurrHunger() == 0 && healthBar.returnCurrHealth() > 20)
        {
            healthBar.UpdateHealth(-1);
        } else if (hunger.returnCurrHunger() > 50)
        {
            healthBar.UpdateHealth(3);
        }

        if (currentclock >= 60)
        {
            currentclock = 0;
            Inventory.Singleton.RemoveAll();
        }
        else
        {
            currentclock += 1;
        }
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
        //When moving, stamina is used and player moves 2x sspeed
        //when stamina depleted, player moves default speed
        //when player is not moving and stamina depleted, bar refills
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
}
