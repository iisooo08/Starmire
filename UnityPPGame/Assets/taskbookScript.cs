using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class taskbookScript : MonoBehaviour
{
    public bool taskActive = false;
    private string taskText;
    public GameObject taskBox;
    public TextMeshProUGUI displayText;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        UpdateTaskText();
    }

    void Start()
    {
        displayText.text = taskText;
    }

    void Update()
    {
        if(!taskActive && Input.GetKeyDown(KeyCode.E) && GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish)
        {
            taskActive = true;
            taskBox.SetActive(true);
        }
        else if(taskActive && Input.GetKeyDown(KeyCode.E))
        {
            taskActive = false;
            taskBox.SetActive(false);
        }
    }

    public void UpdateTaskText()
    {
        taskText = GetCurrentTask();
        displayText.text = taskText;
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
