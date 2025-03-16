using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puzzleonce_a1 : MonoBehaviour
{
    //public static bool a1_solved;
    public GameObject nextscene;
    public GameObject puzzlereward;
    public int puzzlescene;
    private bool once;
    public GameObject puzzleorb;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("p1a1_solved", 0) == 1)
        {
            nextscene.SetActive(false);
            puzzlereward.SetActive(true);
            puzzleorb.SetActive(false);
        }
    }
        void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            puzzleinstructions.puzzle1scene = puzzlescene;
            once = true;
        }
    }
}
