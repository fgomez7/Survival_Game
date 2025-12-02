using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;        // Reference to the Animator component
    private Vector2 moveInput;       // Current movement input
    private Vector2 lastMoveDir;     // Last direction player was moving in

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        bool isMoving = moveInput.sqrMagnitude > 0.1f;

        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            lastMoveDir = moveInput.normalized;
        }

        // When idle, freeze MoveX/Y to last direction for stable facing
        animator.SetFloat("LastMoveX", lastMoveDir.x);
        animator.SetFloat("LastMoveY", lastMoveDir.y);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Item equipped = ItemEquipper.Singleton.CurrentEquippedItem();

            if (equipped != null && equipped.itemTag == SlotTag.Weapon)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
            }
        }

    }

}
