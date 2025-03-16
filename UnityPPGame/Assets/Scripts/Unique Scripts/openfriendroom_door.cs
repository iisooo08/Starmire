using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openfriendroom_door : MonoBehaviour
{
    public static int friendroom_key1;
    public static int friendroom_key2;
    private bool once;
    public GameObject nextsceneobj;

    // Update is called once per frame
    void Start()
    {
        if(PlayerPrefs.GetInt("p1a2_solved", 0) == 1 && PlayerPrefs.GetInt("p1a1_solved", 0) == 1)
        {
            nextsceneobj.SetActive(true);
        }
    }
}
