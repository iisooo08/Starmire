using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openfriendroom : MonoBehaviour
{
    private bool touching;
    public bool type;
    private bool once = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && touching && !once)
        {
            if(type == true)
            {
                openfriendroom_door.friendroom_key1++;
            }
            else
            {
                openfriendroom_door.friendroom_key2++;
            }
            once = true;
            gameObject.SetActive(false);
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
