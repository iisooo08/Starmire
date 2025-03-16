using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemscript : MonoBehaviour
{
    public GameObject item;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            //teleport itself to the inventory box 
            //then trigger playerprefs save 
        }
    }
}
