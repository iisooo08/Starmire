using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialoguemanager : MonoBehaviour
{
    public TextMeshProUGUI dtext;
    public TextMeshProUGUI ntext; 
    private bool returned = false;
    private bool ended = false;
    private string current; 
    private bool generating = false;
    private int index;
    private bool second = false;
    public Animator speechbox;
    public GameObject nameobj;
    public bool finish = false;
    public Rigidbody2D player;
    public AudioSource audio;
    private Coroutine typingCoroutine;
    private int typingindex;

    void Start()
    {
        //audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        finish = true;
    }

    void Update()
    {
        if (ended && Input.GetKeyDown(KeyCode.Return))
        {
            returned = true;
        }
        else if (generating && Input.GetKeyDown(KeyCode.Return))// && index != 0)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            dtext.text = current; 
            generating = false;
            ended = true;
        }
        // else if (generating && Input.GetKeyDown(KeyCode.Return) && index == 0 && !second)
        // {
        //     second = true;
        // }
        // else if (generating && Input.GetKeyDown(KeyCode.Return) && index == 0 && second)
        // {
        //     second = false;
        //     StopCoroutine(typingCoroutine);
        //     typingCoroutine = null;
        //     print("typing is stopped");
        //     dtext.text = current;
        //     generating = false;
        //     ended = true;
        // }
    }

    public IEnumerator StartDialogue(List<string> dialogue, List<string> name, List<int> effects)
    {
        typingindex = 0;
        player.constraints = RigidbodyConstraints2D.FreezeAll;
        finish = false;
        returned = false;
        ended = false;
        generating = false;
        second = false;
        index = 0;

        speechbox.SetTrigger("rise");

        for (int x = 0; x < dialogue.Count; x++)
        {
            speechbox.SetBool("bounce", false);
            speechbox.SetBool("shake", false);
            speechbox.SetBool("tremble", false);

            index = x;
            dtext.text = "";
            current = dialogue[x];
            ntext.text = name[x];

            if (effects[index] == 0) 
            {
                speechbox.SetBool("bounce", true);
            }
            else if (effects[index] == 1)
            {
                speechbox.SetBool("shake", true);
            }
            else if (effects[index] == 2)
            {
                speechbox.SetBool("tremble", true);
            }

            ended = false;
            typingCoroutine = StartCoroutine(typing());
            yield return new WaitUntil(() => returned);
            returned = false;
        }

        speechbox.SetBool("bounce", false);
        speechbox.SetBool("shake", false);
        speechbox.SetBool("tremble", false);
        speechbox.SetTrigger("fall");

        player.constraints = RigidbodyConstraints2D.None;
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
        finish = true;
    }

    IEnumerator typing()
    {
        print("typing");
        generating = true;
        foreach (char letter in current)
        {
            typingindex++;
            if(typingindex%2 == 1)
            {
                audio.Play();
            }
            dtext.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        generating = false;
        ended = true;
    }
}
