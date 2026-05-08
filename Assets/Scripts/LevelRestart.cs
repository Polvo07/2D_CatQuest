using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReiniciarNivel();
        }

    }

    void ReiniciarNivel()
    {
        Scene escenaActual = SceneManager.GetActiveScene();

        SceneManager.LoadScene(escenaActual.name);
    }
}