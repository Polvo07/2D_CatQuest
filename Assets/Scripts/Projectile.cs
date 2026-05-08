using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuración")]
    public float speed = 10f;
    public float lifeTime = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(float direction)
    {
        if (rb == null)
        {
            Debug.LogError("Falta Rigidbody2D en el proyectil");
            return;
        }

        rb.velocity = new Vector2(
            direction * speed,
            0
        );

        // Girar sprite
        if (direction < 0)
        {
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemigo
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        // Suelo
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}