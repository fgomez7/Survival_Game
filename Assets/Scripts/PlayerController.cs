using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    [Header("Attack Settings")]
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackActiveTime = 0.2f;

    private bool isAttacking = false;
    private bool attackPressed = false;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    private void OnEnable()
    {
        playerControls.Enable();

        // Listen for attack input
        playerControls.Movement.Attack.started += ctx => attackPressed = true;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();

        // Attack input
        if (attackPressed && !isAttacking)
        {
            attackPressed = false;
            StartCoroutine(DoAttack());
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator DoAttack()
    {
        isAttacking = true;

        // Play animation
        if (animator != null)
            animator.SetTrigger("Attack");

        // Enable hitbox
        if (attackHitbox != null)
            attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackActiveTime);

        // Disable hitbox
        if (attackHitbox != null)
            attackHitbox.SetActive(false);

        isAttacking = false;
    }
}
