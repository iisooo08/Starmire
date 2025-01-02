using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
 PlayerPrefs.SetInt("dc1_hasTriggered", 1);
         PlayerPrefs.SetInt("dc2_hasTriggered", 1);
    }

    // Update is called once per frame
void Update()
{
    if (Input.GetKeyDown(KeyCode.R)) // Press R to reset the key
    {
        PlayerPrefs.DeleteAll();
    }
}
}
