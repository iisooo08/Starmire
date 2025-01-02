using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openfriendroom_door : MonoBehaviour
{
    public static int friendroom_keys;
    private bool once;

    // Update is called once per frame
    void Start()
    {
        if(friendroom_keys != 2)
        {
            gameObject.SetActive(false);
        }
    }
}
