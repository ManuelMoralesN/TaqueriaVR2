using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // Referencia estática al singleton
    private AudioSource audioSource;

    private void Awake()
    {
        // Configura el Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados
            return;
        }

        // Configura el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No se encontró un AudioSource en el MusicManager.");
        }
    }

    // Reproducir una pista de música
    public void PlayMusic(AudioClip newClip)
    {
        if (audioSource == null || newClip == null) return;

        if (audioSource.clip != newClip)
        {
            audioSource.Stop(); // Detiene la música actual
            audioSource.clip = newClip; // Asigna la nueva pista
            audioSource.Play(); // Reproduce la nueva música
        }
    }

    // Detener la música
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // Cambiar volumen
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume); // Asegura que el volumen esté entre 0 y 1
        }
    }
}
