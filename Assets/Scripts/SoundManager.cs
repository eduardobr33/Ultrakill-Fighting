using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton para acceso global

    public AudioSource musicSource;      // Fuente de audio para la música
    public AudioSource effectsSource;    // Fuente de audio para los efectos de sonido

    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reproducir música de fondo
    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Reproducir efecto de sonido
    public void PlayEffect(AudioClip clip, float volume = 1.0f)
    {
        effectsSource.PlayOneShot(clip, volume);
    }

    // Cambiar el volumen global de la música
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Cambiar el volumen global de los efectos
    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }
}
