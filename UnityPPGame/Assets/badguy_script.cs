using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badguy_script : MonoBehaviour
{
    public AudioSource badguy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            badguy.Play();
        }
    }
}
