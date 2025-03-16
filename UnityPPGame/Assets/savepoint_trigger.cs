using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class savepoint_trigger : MonoBehaviour
{
    void Start()
    {
        print(PlayerPrefs.GetInt("currentpoint", 0));
        PlayerPrefs.SetInt("currentpoint", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }
}
