using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public Animator image;
    public int next;
    void Start()
    {
        image.SetTrigger("start");
    }
    public IEnumerator NextScene()
    {
        image.SetTrigger("end");
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(next);
    }
}
