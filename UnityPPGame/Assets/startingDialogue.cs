using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingDialogue : MonoBehaviour
{
    public List<string> dialogues;
    public List<string> names; 
    public List<int> effects;
    public string currentstr;
    // Start is called before the first frame update
    void Start()
    {
        if ((!SaveData.startDialogueStorage.flags.TryGetValue(currentstr, out bool isDone) || !isDone))
            {
                SaveData.startDialogueStorage.flags[currentstr] = true;
                StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogues, names, effects));
            }
    }
}
