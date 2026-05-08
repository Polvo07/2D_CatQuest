using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuración")]
    public float speed = 10f;
    public float lifeTime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(float direction)
    {
        rb.velocity = new Vector2(
            direction * speed,
            0
        );

        // Voltear sprite
        if (direction < 0)
        {
            Vector3 escala = transform.localScale;

            escala.x *= -1;

            transform.localScale = escala;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemigos
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        // Suelo o paredes
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}