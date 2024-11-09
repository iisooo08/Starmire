using UnityEngine;

public class changemusic : MonoBehaviour
{
    public AudioClip newMusicClip; // Assign in the Inspector

    private void Start()
    {
musicplayer musicPlayer = FindObjectOfType<musicplayer>();
if (musicPlayer != null)
{
    musicPlayer.ChangeMusic(newMusicClip);
}

    }
}
