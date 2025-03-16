using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class save_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("currentpoint", 0) != SceneManager.GetActiveScene().buildIndex)
        {
SceneManager.LoadScene(PlayerPrefs.GetInt("currentpoint", 0));
        }
        
    }

}
