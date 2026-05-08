using UnityEngine;

public class CatSound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayMeow()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}