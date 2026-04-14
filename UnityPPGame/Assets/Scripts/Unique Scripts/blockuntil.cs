using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockuntil : MonoBehaviour
{
    public List<string> dialogue = new List<string>();
    public List<string> name;
    public List<int> effects = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("player"))
    {
        StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogue, name, effects));
    }
}

}
