using UnityEngine;

public class BoxPush : MonoBehaviour
{
    public Transform holdPoint;

    private GameObject nearbyBox;
    private bool carryingBox = false;

    void Update()
    {
        // Presionar E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Agarrar caja
            if (!carryingBox && nearbyBox != null)
            {
                GrabBox();
            }
            // Soltar caja
            else if (carryingBox)
            {
                DropBox();
            }
        }

        // Mover caja junto al jugador
        if (carryingBox && nearbyBox != null)
        {
            nearbyBox.transform.position = holdPoint.position;
        }
    }

    void GrabBox()
    {
        carryingBox = true;

        Rigidbody2D rb = nearbyBox.GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void DropBox()
    {
        carryingBox = false;

        Rigidbody2D rb = nearbyBox.GetComponent<Rigidbody2D>();

        rb.gravityScale = 1;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            nearbyBox = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            nearbyBox = null;
        }
    }
}
