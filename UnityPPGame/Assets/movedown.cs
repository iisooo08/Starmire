using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed;  

    void Start()
    {
        speed = Random.Range(3f, 7f);
    }

    void Update()
    {
        Vector3 movement = new Vector3(0, -speed * Time.deltaTime, 0);
        transform.position += movement;
        if(gameObject.transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }
    }
}