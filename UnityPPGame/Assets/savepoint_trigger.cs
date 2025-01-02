using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savepoint_trigger : MonoBehaviour
{
    public List<string> dialoguelist;
    public string name; 
    private bool touching = false;
    private bool once = false;
    public List<int> effectlist;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(touching == true && Input.GetKeyDown("return") && once == false)
        {
            once = true;
            SaveGame(transform.position);
            print("dialogue");
            StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialoguelist, name, effectlist));
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
    void SaveGame(Vector2 playerPosition)
    {
        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);

        PlayerPrefs.Save();
    }
}
