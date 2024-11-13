using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openfriendroom_door : MonoBehaviour
{
    public static int friendroom_keys;
    public List<string> locked_d;
    public List<string> unlocked_d;
    public List<int> effects;
    public string name;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            touching = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(touching && Input.GetKeyDown(KeyCode.Return) && friendroom_keys == 2)
        {
            //trigger unlocked_d dialogue
        }
        else if(touching && Input.GetKeyDown(KeyCode.Return) && friendroom_keys != 2)
        {
            //trigger locked_d dialogue
        }
    }
}
