using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Vida")]
    public int vida = 2;

    [Header("Movimiento")]
    public float velocidad = 2f;

    public Transform puntoA;
    public Transform puntoB;

    private Rigidbody2D rb;
    private Animator animator;

    private bool yendoDerecha = true;
    private bool muerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (muerto) return;

        Patrullar();
    }

    void Patrullar()
    {
        if (yendoDerecha)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);

            if (transform.position.x >= puntoB.position.x)
            {
                yendoDerecha = false;

                Girar();
            }
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);

            if (transform.position.x <= puntoA.position.x)
            {
                yendoDerecha = true;

                Girar();
            }
        }
    }

    void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;

        transform.localScale = escala;
    }

    public void RecibirDaño()
    {
        if (muerto) return;

        vida--;

        animator.SetTrigger("Hurt");

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        muerto = true;

        rb.velocity = Vector2.zero;

        animator.SetTrigger("Death");

        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDeath player = collision.gameObject.GetComponent<PlayerDeath>();

            if (player != null)
            {
                player.ReiniciarNivel();
            }
        }
    }
}