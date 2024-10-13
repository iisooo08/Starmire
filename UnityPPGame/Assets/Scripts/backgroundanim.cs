using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundanim : MonoBehaviour
{
    private bool once = false;
    public Transform position;
    public GameObject bg;
    public bool start = false;
    // Start is called before the first frame update
    void Update()
    {
        if(once == false && start == true)
        {
            once = true;
            StartCoroutine(GenerateBackground());
        }
    }
    IEnumerator GenerateBackground()
    {
        Instantiate(bg, position.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        once = false;
    }
}
