using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate_particles2 : MonoBehaviour
{
    private bool once = false;
    public List<GameObject> particles = new List<GameObject>();
    // Start is called before the first frame update
    void Update()
    {
        if(once == false)
        {
            once = true;
            StartCoroutine(InstantiateSequence());
        }
    }

    IEnumerator InstantiateSequence()
    {
        InstantiateParticles();
        yield return new WaitForSeconds(1f);
        once = false;
    }

    void InstantiateParticles()
    {
        for(int x = 0; x < 4; x++)
        {
            float randomx1 = Random.Range(50f, -50f);
            int randomshape = Random.Range(0,2);
            Instantiate(particles[randomshape], new Vector3(transform.position.x + randomx1, -10, 0), Quaternion.identity);
        }
        for(int x = 0; x < 4; x++)
        {
            float randomx2 = Random.Range(50f, -50f); 
            int randomshape = Random.Range(0,2);
            Instantiate(particles[randomshape], new Vector3(transform.position.x + randomx2, -10, 0), Quaternion.identity);
        }
    }
}
