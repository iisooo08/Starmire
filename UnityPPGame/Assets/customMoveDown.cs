using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customMoveDown : MonoBehaviour
{
    public float speed;  
    public bool notrandom;

    void Start()
    {
        if(!notrandom)
        {
            speed = Random.Range(3f, 7f);
        }
    }

    void Update()
    {
        Vector3 movement = new Vector3(0, -speed * Time.deltaTime, 0);
        transform.position += movement;
        if(gameObject.transform.position.y <= -3.5)
        {
            speed = 0;
            StartCoroutine(DestroyCoroutine());
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
