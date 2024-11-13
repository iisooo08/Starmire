using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openfriendroom : MonoBehaviour
{
    private bool touching;
    private bool once = true;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && touching && once)
        {
            once = false;
            openfriendroom_door.friendroom_keys++;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            touching = true;
        }
    }
}
