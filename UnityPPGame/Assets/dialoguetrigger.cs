using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialoguetrigger : MonoBehaviour
{
    public Dialogue dialogue; 
    private bool touching = false; 
    public Rigidbody2D rb; 
    public static string name; 

    void Start() 
    {
        name = gameObject.name; 
    }

    void Update() 
    {

        if(touching == true && Input.GetKeyDown("return") && DialogueManager.isopen_int == 0)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue); 
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; 
        }
    }

    void OnCollisionEnter2D() 
    {
        touching = true; 
    }

    void OnCollisionExit2D() 
    {
        touching = false; 
    }
}
