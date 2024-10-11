using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_battlemove : MonoBehaviour
{
    private int slotnum;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && slotnum < 5)
        {
            slotnum++;
            transform.position += new Vector3(1.085f, 0f, 0f);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && slotnum > 0)
        {
            slotnum--;
            transform.position += new Vector3(-1.085f, 0f, 0f);
        }
    }
}
