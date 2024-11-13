using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleonce : MonoBehaviour
{
    private bool once;
    public GameObject nextscene;
    public int puzzlescene;
    public GameObject puzzlereward;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            puzzleinstructions.puzzle1scene = puzzlescene;
            once = true;
        }
    }
    void Start()
    {
        if(once == true)
        {
            nextscene.SetActive(false);
            puzzlereward.SetActive(true);
        }
    }
}
