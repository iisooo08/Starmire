using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CutsceneScript3 : MonoBehaviour
{
    public AudioClip newMusicClip; 
    public AudioClip oldMusicClip; 
    private bool triggered = false;

    public Transform focuspos;
    public CinemachineVirtualCamera vcam;
    public Transform player;

    public List<string> dialogues;
    public List<string> names;
    public List<int> effects;

    public List<string> dialogues2;
    public List<string> names2;
    public List<int> effects2;

    public List<string> dialogues3;
    public List<string> names3;
    public List<int> effects3;

    private List<string> tempdialogue = new List<string>();
    private string tempname;
    private List<int> tempeffects = new List<int>();


    public string currentstr;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            if ((!SaveData.cutsceneStorage.flags.TryGetValue(currentstr, out bool isDone) || !isDone))
            {
                SaveData.cutsceneStorage.flags[currentstr] = true;
                StartCoroutine(BeginCutscene());
            }
        }
    }

    IEnumerator BeginCutscene()
    {
        musicplayer musicPlayer = FindObjectOfType<musicplayer>();
    if (musicPlayer != null)
    {
        musicPlayer.ChangeMusic(newMusicClip);
    }
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        vcam.Follow = focuspos;

        //CCTV bot notices Eve 

        yield return StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues, names, effects));

        //key is obtained 
        
        yield return StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues2, names2, effects2));

        vcam.Follow = player;
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (musicPlayer != null)
    {
        musicPlayer.ChangeMusic(oldMusicClip);
    }
    }
}
