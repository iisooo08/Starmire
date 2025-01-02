using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowanim : MonoBehaviour
{
    public GameObject snow1;
    public GameObject snow2;
    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(once == false)
        {
            once = true;
            StartCoroutine(SnowAnim());
        }
    }
    IEnumerator SnowAnim()
    {
        for(int x = 0; x < 20; x++)
        {
            float randomx1 = Random.Range(-25f, 25f);
            Instantiate(snow1, new Vector2(transform.position.x + randomx1, transform.position.y + 11), Quaternion.identity);
        }
        for(int x = 0; x < 20; x++)
        {
            float randomx2 = Random.Range(-25f, 25f);
            Instantiate(snow2, new Vector2(transform.position.x + randomx2, transform.position.y + 11), Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        once = false;
    }
}
