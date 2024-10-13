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
    void Update()
    {
        if(ended == true && Input.GetKeyDown("return"))
        {
            returned = true;
        }
        if(generating == true && Input.GetKeyDown("return") && index != 0)
        {
            StopCoroutine("typing");
            dtext.text = current; 
            generating = false;
            ended = true;
        }
        else if(generating == true && Input.GetKeyDown("return") && index == 0 && second == false)
        {
            second = true;
        }
        else if(generating == true && Input.GetKeyDown("return") && index == 0 && second == true)
        {
            StopCoroutine("typing");
            dtext.text = current;
            generating = false;
            ended = true;
        }
    }
    public IEnumerator StartDialogue(List<string> dialogue, string name)
    {
        ntext.text = name;
        speechbox.SetTrigger("rise");
        for(int x = 0; x < dialogue.Count; x++)
        {
            index = x;
            dtext.text = "";
            current = dialogue[x];
            ended = false;
            StartCoroutine("typing");
            yield return new WaitUntil(() => returned);
            returned = false;
        }
        speechbox.SetTrigger("fall");
    }

    IEnumerator typing()
    {
        generating = true;
        foreach(char letter in current)
        {
            dtext.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        generating = false;
        ended = true;
    }
}
