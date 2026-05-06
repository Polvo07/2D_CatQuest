using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;
    public float longitudRaycast = 0.2f;
    public LayerMask capaSuelo;
    public Animator animator;

    private bool enSuelo;
    private Rigidbody2D rb;
    private float movimiento;
    private bool mirandoDerecha = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        movimiento = Input.GetAxisRaw("Horizontal");

        // Detectar suelo con raycast
        Vector2 origen = new Vector2(transform.position.x, transform.position.y - 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(origen, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        // Animator
        if (animator != null)
        {
            animator.SetFloat("movement", Mathf.Abs(movimiento));
            animator.SetBool("isJumping", !enSuelo);
        }

        // Girar sprite
        if (movimiento > 0 && !mirandoDerecha)
            Girar();
        else if (movimiento < 0 && mirandoDerecha)
            Girar();

        // Salto SOLO si está en el suelo
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
    }

    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;

        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}