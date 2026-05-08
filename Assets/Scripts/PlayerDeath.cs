using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [Header("Altura de muerte")]
    public float limiteCaida = -10f;

    void Update()
    {
        // Caer al vacío
        if (transform.position.y < limiteCaida)
        {
            ReiniciarNivel();
        }
    }

    public void ReiniciarNivel()
    {
        Scene escenaActual = SceneManager.GetActiveScene();

        SceneManager.LoadScene(escenaActual.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            ReiniciarNivel();
        }
    }
}