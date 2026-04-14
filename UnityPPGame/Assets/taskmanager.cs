using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskmanager : MonoBehaviour
{
    public GameObject notification;

    void Start()
    {
        foreach (KeyValuePair<string, bool> task in SaveData.taskStorage.flags)
        {
            //add to starting 
        }
    }

    public void AddTask(string taskName)
    {
        if(GetCurrentTask() != null)
        {
            SaveData.taskStorage.flags[GetCurrentTask()] = false;
        }
        SaveData.taskStorage.flags[taskName] = true;
        GameObject.Find("taskbook").GetComponent<taskbookScript>().UpdateTaskText();
        
        StartCoroutine(Notification());
    }

    IEnumerator Notification()
    {
        notification.SetActive(true);
        yield return new WaitForSeconds(1f);
        notification.SetActive(false);
    }
        
    string GetCurrentTask()
    {
        foreach (var task in SaveData.taskStorage.flags)
        {
            if (task.Value)
            {
                return task.Key;
            }
        }

        return null;
    }
}
