using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class animationtrigger : MonoBehaviour
{
    private static bool once = false; 
    public CinemachineVirtualCamera vcam; 
    public Transform newfocus; public List<string> dialogue;
    public List<string> names;
    private List<string> currentdialogue = new List<string>();
    private string prevname;
    public List<int> effects;
    private List<int> currenteffects = new List<int>();
    public Transform dialoguepos;
    public GameObject textbox;
    public Transform player;
    public bool next;
    public GameObject nextobj;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player") && once == false)
        {
            once = true;
            StartCoroutine(StartAnim());
        }
    }

    IEnumerator StartAnim()
    {
        vcam.Follow = newfocus;
        for(int x = 0; x < names.Count; x++)
        {
            if(x == 0)
            {
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else if(prevname == names[x])
            {
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else 
            {
                StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
                yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish);
                currenteffects.Clear();
                currentdialogue.Clear();
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
                prevname = names[x];
            }
            if(x == names.Count - 1)
            {
                prevname = names[x];
                StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
                yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish);
            }
        }
        vcam.Follow = GameObject.Find("playersprite_final").transform;
        if(next == true)
        {
            nextobj.SetActive(true);
        }
    }
}
