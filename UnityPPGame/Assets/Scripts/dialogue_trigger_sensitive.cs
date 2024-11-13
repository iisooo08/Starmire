using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_trigger_sensitive : MonoBehaviour
{
    public List<string> dialoguelist;
    public string name; 
    private bool touching = false;
    private bool once = false;
    public List<int> effectlist;
    public Transform playerpos;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(touching == true && once == false)
        {
            once = true;
            print("dialogue");
            StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialoguelist, name, effectlist));
            GameObject.Find("playersprite_final").transform.position = playerpos.position;
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
