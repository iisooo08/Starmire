using UnityEngine;

public class diagonalmovement : MonoBehaviour
{
    public float speed;  
    public float speed2;

    void Start()
    {
        speed = Random.Range(1f, 3f);
        speed2 = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        Vector3 movement = new Vector3(-speed2 * Time.deltaTime, -speed * Time.deltaTime, 0);
        transform.position += movement;
        if(gameObject.transform.position.y <= -21f)
        {
            Destroy(gameObject);
        }
    }
}