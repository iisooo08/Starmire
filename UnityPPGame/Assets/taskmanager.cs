using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskmanager : MonoBehaviour
{
    public GameObject notification;
    void Start()
    {
        SaveData.LoadTasks();
        foreach (KeyValuePair<string, bool> task in SaveData.taskStorage.flags)
        {
            //add to starting 
        }
    }

    public void AddTask(string taskName)
    {
        SaveData.taskStorage.flags[taskName] = true;
        GameObject.Find("taskbook").GetComponent<taskbookScript>().UpdateTaskText();
        StartCoroutine(Notification());
    }

    public void ResolveTask(string taskName)
    {
        SaveData.taskStorage.flags.Remove(taskName);
        //remove from display
    }

    IEnumerator Notification()
    {
        notification.SetActive(true);
        yield return new WaitForSeconds(1f);
        notification.SetActive(false);
    }
}
