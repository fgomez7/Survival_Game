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
    // [SerializeField] private Item itemData;
    // private bool playerInRange = false;


    public float moveSpeed = 5f;        // movement speed
    public float sprintSpeed = 10f;
    private float currentSpeed;
    private Vector2 moveInput;          // stores WASD input
    private Rigidbody2D rb;             // reference to Rigidbody2D
    //__________________________________________________________________________
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currHealth;
    public StaminaBar stamina;
    public int maxStamina = 100;
    public int currStamina = 100;
    public HungerBar hunger;
    public int currHunger = 100;
    public int maxHunger = 100;

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
    }

    private void OnDestroy() { 
        GlobalClock.Instance.OnTick -= OnClockTick;
    }
    void OnClockTick() {
        useHunger(1);
        Debug.Log("Tick recieved");
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // This function is automatically called by the PlayerInput component
    void OnMove(InputValue value)
    {
        //testing damage
        moveInput = value.Get<Vector2>();
        if ( moveInput.y > 0) //up arrow 
        {
            TakeDamage(5);
        }
        //if (currStamina > 0 & moveInput != Vector2.zero) {
        //    useStamina(1);
        //} else if ( moveInput == Vector2.zero )
        //{
        //    refillStamina(2);
        //}
    }

    private void FixedUpdate()
    {
        // Apply movement every physics frame
        //if (currStamina > 0)
        //{
        //    rb.velocity = 2 * moveInput * moveSpeed;
        //}
        //rb.velocity = moveInput * moveSpeed;

        //if (rb.velocity != Vector2.zero)
        //{
        //    useStamina(1);
        //}
        //else if (rb.velocity == Vector2.zero)
        //{
        //    refillStamina(2);
        //}

        // if (playerInRange && Input.GetKeyDown(KeyCode.E))
        // {
        //     CollectItem();
        // }
        //_________________________________________________________
        if (moveInput != Vector2.zero)
        {
            if (currStamina > 0)
            {
                useStamina(1);
                currentSpeed = sprintSpeed;
            } else
            {
                currentSpeed = moveSpeed;
            }
        } else
        {
            refillStamina(2);
            currentSpeed = moveSpeed;
        }
        rb.velocity = moveInput * currentSpeed;
    }

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth); 
        //currHunger -= damage;
        //hunger.SetHunger(currHunger);
    }

    void useStamina(int stam)
    {
        if ( currStamina > 0)
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
        if (currHunger >0)
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
    
    // private void OnTriggerEnter2D( Collider2D collision )
    // {
    //     if (collision.CompareTag("apple"))
    //     {
    //         collision.gameObject.SetActive(false);
    //     }
    //     Debug.Log($"Collected {itemData.name}");
    // }
}
