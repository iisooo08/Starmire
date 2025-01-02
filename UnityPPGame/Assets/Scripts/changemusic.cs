using UnityEngine;

public class changemusic : MonoBehaviour
{
    public AudioClip newMusicClip; 

    private void Start()
    {
        musicplayer musicPlayer = FindObjectOfType<musicplayer>();
if (musicPlayer != null)
{
    musicPlayer.ChangeMusic(newMusicClip);
}

    }
}
