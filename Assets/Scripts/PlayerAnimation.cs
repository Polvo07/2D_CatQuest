using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Salto
        animator.SetBool("isJumping", rb.velocity.y != 0);
    }
}