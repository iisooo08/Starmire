using System.Collections;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Check if there's already an instance of musicplayer in the scene
        if (FindObjectsOfType<musicplayer>().Length > 1)
        {
            Destroy(gameObject); // Prevent duplicates
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
            audioSource = GetComponent<AudioSource>(); // Assign AudioSource once in Awake
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip) // Avoid reloading the same clip
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

    // Coroutine to fade out the audio
    public IEnumerator FadeOut(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    // Coroutine to fade in the audio
    public IEnumerator FadeIn(float fadeDuration)
    {
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 1;
    }

    // Public method to start fading out
    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    // Public method to start fading in
    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }
}
