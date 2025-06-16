using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomanim : MonoBehaviour
{
    public Sprite nextsprite;
    public Sprite currentsprite;
    private bool current = false;
    // Start is called before the first frame update
    void Start()
    {
        currentsprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(Animations());
    }

    IEnumerator Animations()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1f));
        if(current == false)
        {
            current = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = nextsprite;
        }
        else if(current == true)
        {
            current = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = currentsprite;
        }
        NextFunct();
    }
    void NextFunct()
    {
        StartCoroutine(Animations());
    }
}
