using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTrigger : MonoBehaviour
{
    public int uniqueNumber;
    private bool touching = false;
    private bool triggered = false;

    public bool task = false;
    public string taskname;
    public bool justtouch = false;
    public string currentstr;

    void Update()
    {
        bool shouldActivate = touching && (justtouch || Input.GetKeyDown(KeyCode.Return));

        bool alreadyDone = SaveData.animationStorage.flags.TryGetValue(currentstr, out bool isDone) && isDone;

        if (shouldActivate && !alreadyDone && !triggered)
        {
            triggered = true;
            SaveData.animationStorage.flags[currentstr] = true;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
            touching = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
            touching = false;
    }
}