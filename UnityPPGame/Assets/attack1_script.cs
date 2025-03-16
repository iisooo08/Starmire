using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack1_script : MonoBehaviour
{
    public List<GameObject> eggs;
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0,5);
        eggs[random].SetActive(false);
    }
}
