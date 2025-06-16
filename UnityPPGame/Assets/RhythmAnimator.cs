using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmAnimator : MonoBehaviour
{
    public Sprite nextsprite;
    private Sprite ogsprite;
    public float beats;
    private bool track = false;
    // Start is called before the first frame update
    void Start()
    {
        ogsprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(BeatAnim());
    }

    IEnumerator BeatAnim()
    {
        yield return new WaitForSeconds(beats);
        if(track == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = nextsprite;
            track = true;
        }
        else 
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ogsprite;
            track = false;
        }
        Repeat();
    }

    void Repeat()
    {
        StartCoroutine(BeatAnim());
    }
}
