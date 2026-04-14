using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTrigger : MonoBehaviour
{
    public int uniqueNumber;
    private bool touching = false;
    public bool task = false;
    public string taskname;

    // Update is called once per frame
    void Update()
    {
        if(touching && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        GeneralAnimationManager.prevscene = SceneManager.GetActiveScene().buildIndex;
        if (task && !SaveData.taskStorage.flags.ContainsKey(taskname))
        {
            Debug.Log("task changed");
            GameObject.Find("taskmanager").GetComponent<taskmanager>().AddTask(taskname);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    void OnTriggerEnter2D()
    {
        touching = true;
    }
    void OnTriggerExit2D()
    {
        touching = false;
    }
}
