using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnscript : MonoBehaviour
{
    private bool temp = true;
    public GameObject obj1; 
    public float time;
    // Update is called once per frame
    void Update()
    {
        if(temp)
        {
            StartCoroutine(InstantiateObj());
        }
    }
    IEnumerator InstantiateObj()
    {
        temp = false;
        Instantiate(obj1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(time);
        temp = true;
    }
}
