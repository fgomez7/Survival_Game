using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    Vector2 moveInput;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        bool isMoving = moveInput.sqrMagnitude > 0;
        animator.SetBool("IsMoving", isMoving);
    }
}
