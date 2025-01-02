using System.Collections;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<musicplayer>().Length > 1)
        {
            Destroy(gameObject); 
        }
        else
        {
            DontDestroyOnLoad(gameObject); 
            audioSource = GetComponent<AudioSource>(); 
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip) 
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

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

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }
}
