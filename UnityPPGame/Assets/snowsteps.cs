using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowsteps : MonoBehaviour
{
    public GameObject steps;
    private bool onsnow;
    private bool temptime;
    private bool temptime2;
    public GameObject playerobj;

    // Update is called once per frame
    void Update()
    {
        if(onsnow && temptime == false && (playerobj.GetComponent<playermove>().movement.x != 0 || playerobj.GetComponent<playermove>().movement.y != 0))
        {
            StartCoroutine(InstantiateSnow());
        }
        if(onsnow && temptime2 == false && playerobj.GetComponent<playermove>().movement.x == 0 && playerobj.GetComponent<playermove>().movement.y == 0)
        {
            StartCoroutine(StaticSnow());
        }
    }
    IEnumerator InstantiateSnow()
    {
        temptime = true;
        Instantiate(steps, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        temptime = false;
    }
    IEnumerator StaticSnow()
    {
        temptime2 = true;
        Instantiate(steps, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        temptime2 = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider has the tag "snow"
        if (other.CompareTag("snow"))
        {
            onsnow = true;
        }
    }
        void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider has the tag "snow"
        if (other.CompareTag("snow"))
        {
            onsnow = false;
        }
    }
}
