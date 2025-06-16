using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RhythmText : MonoBehaviour
{
    public List<string> possibletexts;
    public TextMeshProUGUI bubbletext;
    public GameObject bubble;
    private int temp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedTexts());
    }

    // Update is called once per frame
    IEnumerator RepeatedTexts()
    {
        bubbletext.text = "";
        bubbletext.enabled = false;
        bubble.GetComponent<Animator>().ResetTrigger("start");
        bubble.GetComponent<Animator>().SetTrigger("end");

        yield return new WaitForSeconds(0.5f);

        bubble.GetComponent<Animator>().ResetTrigger("end");
        bubble.GetComponent<Animator>().SetTrigger("start");
        yield return new WaitForSeconds(0.25f);
        bubbletext.enabled = true;
        temp = Random.Range(0,3);
        bubbletext.text = possibletexts[temp];
        yield return new WaitForSeconds(5f);
        Repeat();
    }

    void Repeat()
    {
        StartCoroutine(RepeatedTexts());
    }
}
