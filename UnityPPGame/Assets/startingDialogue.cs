using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingDialogue : MonoBehaviour
{
    public List<string> dialogues;
    public List<string> names; 
    public List<int> effects;
    public string currentstr;
    public bool task = false;
    public string taskname;
    private bool taskonce = false;
    // Start is called before the first frame update
    void Start()
    {
        if ((!SaveData.startDialogueStorage.flags.TryGetValue(currentstr, out bool isDone) || !isDone))
            {
                SaveData.startDialogueStorage.flags[currentstr] = true;
                StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues, names, effects));
            }
    }
    void Update()
    {
        if(taskonce == false && task == true && (SaveData.startDialogueStorage.flags.TryGetValue(currentstr, out bool isDone) || !isDone) && GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish == true)
        {
            taskonce = true;
            GameObject.Find("taskmanager").GetComponent<taskmanager>().AddTask(taskname);
        }
    }
}
