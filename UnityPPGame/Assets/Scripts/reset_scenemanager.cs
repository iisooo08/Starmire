using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset_scenemanager : MonoBehaviour
{
    public static int puzzlescene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(puzzlescene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
