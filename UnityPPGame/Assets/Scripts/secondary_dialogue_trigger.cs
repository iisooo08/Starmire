using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondary_dialogue_trigger : MonoBehaviour
{
    public List<string> dialoguelist;
    public List<string> secondarylist;
    public string name; 
    private bool touching = false;
    private bool once = false;
    public List<int> effectlist;
    public List<int> effectsecond;
    public GameObject nextobj;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(touching == true && Input.GetKeyDown("return") && once == false && nextobj.GetComponent<battle_nextobj>().triggered == false)
        {
            once = true;
            print("dialogue");
            StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialoguelist, name, effectlist));
        }
        else if(touching == true && Input.GetKeyDown("return") && once == false && nextobj.GetComponent<battle_nextobj>().triggered == true)
        {
            once = true;
            print("dialogue");
            StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(secondarylist, name, effectsecond));
        }
        if(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish == true)
        {
            once = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            touching = true;
        }
    }
    void OnCollisionExit2D()
    {
        touching = false;
    }
}
