using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowanim : MonoBehaviour
{
    public GameObject snow1;
    public GameObject snow2;
    private bool once;
    public int freq;
    public float wait;
    
    void Start()
    {
        StartCoroutine(SnowAnim());
    }

    IEnumerator SnowAnim()
    {
        for(int x = 0; x < freq; x++)
        {
            float randomx1 = Random.Range(-25f, 25f);
            Instantiate(snow1, new Vector2(transform.position.x + randomx1, transform.position.y + 11), Quaternion.identity);
        }
        for(int x = 0; x < freq; x++)
        {
            float randomx2 = Random.Range(-25f, 25f);
            Instantiate(snow2, new Vector2(transform.position.x + randomx2, transform.position.y + 11), Quaternion.identity);
        }
        yield return new WaitForSeconds(wait);
        StartCoroutine(SnowAnim());
    }
}
