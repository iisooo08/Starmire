using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{
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

    public Transform animpos;
    public Sprite front1;
    public Sprite front2;

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
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        vcam.Follow = focuspos;
        yield return StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues, names, effects));

        //robot pulls itself together 
        
        yield return StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues2, names2, effects2));
        
        player.position = animpos.position;
        player.gameObject.GetComponent<Animator>().SetTrigger("front");
        player.gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);

        //player moves forward 
        for(int x = 0; x < 3; x++)
        {
            player.position = new Vector2(player.position.x, player.position.y - 0.3f);
            yield return new WaitForSeconds(0.4f);
        }
        //robots turn around 

        yield return StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues3, names3, effects3));

        player.gameObject.GetComponent<Animator>().ResetTrigger("front");
        player.gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        vcam.Follow = player;
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
