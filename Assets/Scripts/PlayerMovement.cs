using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask capaSuelo;

    [Header("Referencias")]
    public Animator animator;
    public AudioSource audioSource;

    [Header("Ataque")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Rigidbody2D rb;

    private bool enSuelo;
    private bool mirandoDerecha = true;

    private float movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        movimiento = Input.GetAxisRaw("Horizontal");

        DetectarSuelo();

        Animaciones();

        GirarSprite();

        Saltar();

        Ataque();
    }

    void FixedUpdate()
    {
        float velocidadActual = velocidad;

        // Correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual *= 1.5f;
        }

        rb.velocity = new Vector2(
            movimiento * velocidadActual,
            rb.velocity.y
        );
    }

    void DetectarSuelo()
    {
        enSuelo = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            capaSuelo
        );
    }

    void Saltar()
    {
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                fuerzaSalto
            );
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
        bool estaMoviendose = movimiento != 0;

        animator.SetBool("IsMoving", estaMoviendose);

        // Correr
        bool corriendo =
            Input.GetKey(KeyCode.LeftShift) &&
            estaMoviendose;

        animator.SetBool("IsRunning", corriendo);

        // Salto
        animator.SetBool("IsJumping", !enSuelo);

        // Meow
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Meow");

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        float direccion = mirandoDerecha ? 1f : -1f;

        projectileScript.SetDirection(direccion);
    }

    // Detectar púas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            ReiniciarNivel();
        }
    }

    // Reiniciar escena
    void ReiniciarNivel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // Visualizar Ground Check
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundRadius
        );
    }
}