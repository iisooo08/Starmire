using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(gameObject.transform.position.x <= -24f)
        {
            Destroy(gameObject);
        }
    }
}
