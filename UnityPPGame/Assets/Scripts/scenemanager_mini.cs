using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenemanager_mini : MonoBehaviour
{
    public int pos; //next scene index 

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            GameObject.Find("scenemanager").GetComponent<scenemanager>().next = pos;
            StartCoroutine(GameObject.Find("scenemanager").GetComponent<scenemanager>().NextScene());
        }
    }
}
