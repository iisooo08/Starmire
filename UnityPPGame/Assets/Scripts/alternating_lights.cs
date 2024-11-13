using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alternating_lights : MonoBehaviour
{
    public List<GameObject> circuitlights; 
    public Sprite offsprite;
    public Sprite onsprite; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(foreveranimation());
    }

    IEnumerator foreveranimation()
    {
        while(true)
        {
            for(int x = 0; x < circuitlights.Count; x++)
            {
                circuitlights[x].GetComponent<SpriteRenderer>().sprite = onsprite;
                yield return new WaitForSeconds(0.5f);
                circuitlights[x].GetComponent<SpriteRenderer>().sprite = offsprite;
            }
        }
    }
}
