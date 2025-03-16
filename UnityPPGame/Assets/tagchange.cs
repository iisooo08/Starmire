using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagchange : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TagChange());
    }
    IEnumerator TagChange()
    {
        yield return new WaitForSeconds(time);
        gameObject.tag = "enemy";
    }
}
