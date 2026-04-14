using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class eventbook_script : MonoBehaviour
{
    private static string task;
    public GameObject textbox;
    public TextMeshProUGUI tasktext;
    private static bool once = false;
    public static bool currentevent;
    public static bool isChecked;

    void Start()
    {
        if (currentevent == true && isChecked == false)
        {
            gameObject.GetComponent<Animator>().SetBool("unchecked", true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && once == false && currentevent == true)
        {
            isChecked = true;
            gameObject.GetComponent<Animator>().SetBool("unchecked", false);
            once = true;
            textbox.SetActive(true);
            tasktext.text = task;
        }
        else if (Input.GetKeyDown(KeyCode.I) && once == false && currentevent == false)
        {
            once = true;
            textbox.SetActive(true);
            tasktext.text = "There is no task...as of now.";
        }
        else if (Input.GetKeyDown(KeyCode.I) && once == true)
        {
            tasktext.text = "";
            once = false;
            textbox.SetActive(false);
        }
    }

    public void AddTask(string text)
    {
        isChecked = false;
        currentevent = true;
        task = text;
        gameObject.GetComponent<Animator>().SetBool("unchecked", true);
    }

    public void RemoveTask()
    {
        isChecked = false;
        currentevent = false;
        task = "";
        gameObject.GetComponent<Animator>().SetBool("unchecked", false);
    }
}
