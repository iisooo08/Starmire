using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug : MonoBehaviour
{
    void Start()
    {
Debug.Log(GameObject.Find("taskbook").GetComponent<taskbookScript>().taskActive);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            SaveData.ResetAll();
            Debug.Log("reset");
        }
    }
}
