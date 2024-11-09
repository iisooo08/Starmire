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
            inventory_script.itemslist.Add(item);
            gameObject.SetActive(false);
        }
    }
}
