using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class roomCutscene : MonoBehaviour
{
    public string idString;
    public AudioClip defaultMusic;
    public AudioClip cutsceneMusic;
    public CinemachineVirtualCamera vcam;
    public Transform focusPos;
    public Transform playerPos;
    public List<string> dialogues;
    public List<string> names;
    public List<int> effects;
    public string taskName;
    
    // Start is called before the first frame update
    void Start()
    {
        if ((!SaveData.cutsceneStorage.flags.TryGetValue(idString, out bool isDone) || !isDone))
        {
            SaveData.cutsceneStorage.flags[idString] = true;
            StartCoroutine(BeginCutscene());
        }
    }

    IEnumerator BeginCutscene()
    {
        playerPos.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        musicplayer musicPlayer = FindObjectOfType<musicplayer>();
        if (musicPlayer != null)
        {
            musicPlayer.ChangeMusic(cutsceneMusic);
        }
        vcam.Follow = focusPos;
        
        yield return StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues, names, effects));

        //add task 

        //revert to previous conditions 
        vcam.Follow = playerPos;
        musicPlayer.ChangeMusic(defaultMusic);
        playerPos.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
