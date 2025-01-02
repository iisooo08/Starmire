using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfdestruct : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfD());
    }

    IEnumerator SelfD()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
