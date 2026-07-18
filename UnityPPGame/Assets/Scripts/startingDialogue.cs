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

    IEnumerator Start()
    {
        if (SaveData.startDialogueStorage.flags.TryGetValue(currentstr, out bool isDone) && isDone)
        {
            yield break;
        }

        dialoguemanager dm = GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>();

        yield return StartCoroutine(dm.StartDialogue(dialogues, names, effects));

        SaveData.startDialogueStorage.flags[currentstr] = true;

        if (task && !SaveData.taskStorage.flags.ContainsKey(taskname))
        {
            GameObject.Find("taskmanager").GetComponent<taskmanager>().AddTask(taskname);
        }
    }
}