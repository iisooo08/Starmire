using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class titleanimation : MonoBehaviour
{
    public GameObject title;
    public GameObject quote;
    public Animator image; 
    public AudioSource song;
    private bool nextscene = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }
    void Update()
    {
        if(nextscene == true && Input.GetKeyDown("return"))
        {
            StartCoroutine(GameObject.Find("scenemanager").GetComponent<scenemanager>().NextScene());
        }
    }

    IEnumerator Sequence()
    {
        GameObject.Find("bganimationmanager").GetComponent<backgroundanim>().start = true;
        yield return new WaitForSeconds(0.5f);
        quote.SetActive(true);
        yield return new WaitForSeconds(4f);
        quote.SetActive(false);
        image.SetTrigger("disappear");
        yield return new WaitForSeconds(0.5f);
        title.SetActive(true);
        song.Play();
        nextscene = true;
    }
}
