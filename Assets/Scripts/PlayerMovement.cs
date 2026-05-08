using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Header("Suelo")]
    public float longitudRaycast = 0.2f;
    public LayerMask capaSuelo;

    [Header("Referencias")]
    public Animator animator;

    private Rigidbody2D rb;

    private bool enSuelo;
    private bool mirandoDerecha = true;

    private float movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        movimiento = Input.GetAxisRaw("Horizontal");

        DetectarSuelo();

        Animaciones();

        GirarSprite();

        Saltar();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
    }

    void DetectarSuelo()
    {
        Vector2 origen = new Vector2(transform.position.x, transform.position.y - 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(
            origen,
            Vector2.down,
            longitudRaycast,
            capaSuelo
        );

        enSuelo = hit.collider != null;
    }

    void Saltar()
    {
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }
    }

    void GirarSprite()
    {
        if (movimiento > 0 && !mirandoDerecha)
            Girar();

        else if (movimiento < 0 && mirandoDerecha)
            Girar();
    }

    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;

        Vector3 escala = transform.localScale;
        escala.x *= -1;

        transform.localScale = escala;
    }

    void Animaciones()
    {
        if (animator == null) return;

        // Movimiento
        animator.SetBool("IsPushing \"Y\"", movimiento != 0);

        // Salto
        animator.SetBool("IsJumping", !enSuelo);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 origen = new Vector3(
            transform.position.x,
            transform.position.y - 0.5f,
            0
        );

        Gizmos.DrawLine(
            origen,
            origen + Vector3.down * longitudRaycast
        );
    }
}