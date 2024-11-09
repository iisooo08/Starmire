using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puzzle1_textbubble : MonoBehaviour
{
    public TextMeshProUGUI bubbletext;
    public List<string> dialogues;
    private string current;
    private bool generating;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dialoguesequence());
    }

    IEnumerator dialoguesequence()
    {
        gameObject.GetComponent<Animator>().SetTrigger("rise");
        yield return new WaitForSeconds(0.5f);
        for(int x = 0; x < dialogues.Count; x++)
        {
            bubbletext.text = "";
            current = dialogues[x];
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(typing());
            yield return new WaitUntil(() => generating == false);
            yield return new WaitForSeconds(1f);
        }
        bubbletext.enabled = false;
        gameObject.GetComponent<Animator>().SetTrigger("fall");
    }
        IEnumerator typing()
    {
        generating = true;
        foreach (char letter in current)
        {
            bubbletext.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        generating = false;
    }
}
