using UnityEngine;

public class moveright : MonoBehaviour
{
    // Expose min/max if you want to tweak them in the Inspector:
    public float minSpeed = 5f;
    public float maxSpeed = 8f;

    private float speed;

    void Start()
    {
        // Now Random.Range runs at runtime, after Unity’s init
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // If you really want to move right, use Vector3.right:
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // And destroy once you get past +35 on x
        if (transform.position.x >= 35f)
            Destroy(gameObject);
    }
}
