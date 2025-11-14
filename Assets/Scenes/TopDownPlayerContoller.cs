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
    private Vector2 moveInput;          // stores WASD input
    private Rigidbody2D rb;             // reference to Rigidbody2D
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currHealth;
    public StaminaBar stamina;
    public int maxStamina = 100;
    public int currStamina = 100;

    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currStamina = maxStamina;
        stamina.SetMaxStamina(maxStamina);
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
        if (currStamina > 0)
        {
            rb.velocity = 2 * moveInput * moveSpeed;
        }
        rb.velocity = moveInput * moveSpeed;

        if (rb.velocity != Vector2.zero)
        {
            useStamina(1);
        }
        else if (rb.velocity == Vector2.zero)
        {
            refillStamina(2);
        }
        // if (playerInRange && Input.GetKeyDown(KeyCode.E))
        // {
        //     CollectItem();
        // }

    }

    void TakeDamage(int damage)
    {
        currHealth -= damage;
        healthBar.SetHealth(currHealth); 
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
    
    // private void OnTriggerEnter2D( Collider2D collision )
    // {
    //     if (collision.CompareTag("apple"))
    //     {
    //         collision.gameObject.SetActive(false);
    //     }
    //     Debug.Log($"Collected {itemData.name}");
    // }
}
