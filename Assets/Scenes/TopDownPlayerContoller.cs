using System;
using UnityEngine;
using UnityEngine.InputSystem; // needed for InputValue
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour
{
    public static TopDownPlayerController Singleton;
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    private float currentSpeed;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Health - Stamina - Hunger - Reset")]
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currHealth;
    public StaminaBar stamina;
    public int maxStamina = 100;
    public int currStamina = 100;
    public HungerBar hunger;
    public int currHunger = 100;
    public int maxHunger = 100;
    public ResetMechanic resetBar;
    public int currResetBar;

    [Header("Attack Settings")]
    public GameObject attackHitbox;
    public float attackActiveTime = 0.2f;

    private bool isAttacking = false;
    private Animator animator;

    private int currentclock;

    private int objApple;
    private int currentLevel;
    void Start()
    {
        Singleton = this;
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currStamina = maxStamina;
        stamina.SetMaxStamina(maxStamina);

        currHunger = maxHunger;
        hunger.SetMaxHunger(maxHunger);

        currResetBar = 300;
        resetBar.SetReset(currResetBar);

        currentSpeed = moveSpeed;
        //____________CLOCK__________
        GlobalClock.Instance.OnTick += OnClockTick;

        animator = GetComponent<Animator>();

        // Ensure hitbox is OFF at the start
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
        currentclock = 0;

        currentLevel = InventoryPersistentStorage.currentLevel;
        objApple = 0;
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
        }
        else if (hunger.returnCurrHunger() > 50)
        {
            healthBar.UpdateHealth(3);
        }

        if (currentclock >= 300)
        {
            currentclock = 0;
            //Inventory.Singleton.RemoveAll();
            ResetWorld();
        }
        else
        {
            currentclock += 1;
            resetBar.UpdateReset(-1);
        }
    }

    void ResetWorld()
    {
        Time.timeScale = 1f; // forces unity editor to unpause
        Inventory.Singleton.SavePersistentItems();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        //if (moveInput.y > 0)
        //{
        //    TakeDamage(5);
        //}
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
    public void TakeDamage(int damage)
    {
        //currHealth -= damage;
        healthBar.UpdateHealth(-damage);
        
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

    public void ObjectiveList(int enemy)
    {
        if (currentLevel == 1 && enemy == 1)
        {
            objApple += 1;
            if (objApple >= 1)
            {
                Inventory.Singleton.SavePersistentItems();
                InventoryPersistentStorage.currentLevel = currentLevel + 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (currentLevel == 2 && enemy == 2)
        {
            objApple += 1;
            if (objApple >= 2)
            {
                Inventory.Singleton.SavePersistentItems();
                InventoryPersistentStorage.currentLevel = currentLevel + 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
