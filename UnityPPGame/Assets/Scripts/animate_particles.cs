using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate_particles : MonoBehaviour
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
        yield return new WaitForSeconds(0.7f);
        once = false;
    }

    void InstantiateParticles()
    {
        for(int x = 0; x < 5; x++)
        {
            float randomx1 = Random.Range(-9.2f, -4.5f);
            int randomshape = Random.Range(0,2);
            Instantiate(particles[randomshape], new Vector3(randomx1, 7, 0), Quaternion.identity);
        }
        for(int x = 0; x < 5; x++)
        {
            float randomx2 = Random.Range(4.5f, 9.2f); 
            int randomshape = Random.Range(0,2);
            Instantiate(particles[randomshape], new Vector3(randomx2, 7, 0), Quaternion.identity);
        }
    }
}
