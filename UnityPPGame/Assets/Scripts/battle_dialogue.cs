using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class battle_dialogue : MonoBehaviour
{
    public TextMeshProUGUI dtext;
    private bool returned = false;
    public bool ended = false;
    private string current; 
    public bool generating = false;
    private int index;
    public bool finish = false;
    public AudioSource audio;
    private Coroutine typingCoroutine;
    public bool turn = false;

    void Start()
    {
        //audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        finish = true;
    }

    void Update()
    {
        if (turn && generating && Input.GetKeyDown(KeyCode.Return))
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            dtext.text = current;
            generating = false;
            ended = true;
        }
    }

    public IEnumerator StartDialogue(List<string> dialogue)
    {
        finish = false;
        returned = false;
        ended = false;
        generating = false;
        index = 0;

        for (int x = 0; x < dialogue.Count; x++)
        {

            index = x;
            dtext.text = "";
            current = dialogue[x];

            ended = false;
            typingCoroutine = StartCoroutine(typing());
            yield return new WaitUntil(() => returned);
            returned = false;
        }
        finish = true;
    }

    IEnumerator typing()
    {
        print("typing");
        generating = true;
        foreach (char letter in current)
        {
            dtext.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        generating = false;
        ended = true;
    }
}
