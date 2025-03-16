using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antierror_detector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("antierror"))
        {
            GameObject.Find("antierror_manager").GetComponent<antierror_manager>().AddAntierror();
        }
    }
}
