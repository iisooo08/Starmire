using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeelement_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(GameObject.Find("battlemanager").GetComponent<battlemanager_script>().speed * 3);
        gameObject.GetComponent<Animator>().SetTrigger("bug");
    }
}
