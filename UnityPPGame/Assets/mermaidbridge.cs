using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermaidbridge : MonoBehaviour
{
    public static int pillarcount;
    private bool touching;
    public bool collider;
    public GameObject bridge;
    public static bool inspector = false;
    public bool scholar;
    public bool pillar;

    // Update is called once per frame
    void Start()
    {
        pillarcount = PlayerPrefs.GetInt("pillarcount", 0);
        inspector = PlayerPrefs.GetInt("inspector", 0) == 1;
    }
    void Update()
    {
        if(scholar && touching && Input.GetKeyDown(KeyCode.Return))
        {
            inspector = true;
            PlayerPrefs.SetInt("inspector", 1);
        }
        if(pillar && inspector && touching && Input.GetKeyDown(KeyCode.Return))
        {
            pillarcount++;
            PlayerPrefs.SetInt("pillarcount", pillarcount);
            gameObject.SetActive(false);
        }
        if(collider && pillarcount == 2)
        {
            bridge.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            touching = true;
        }
    }
    void OnCollisionExit2D()
    {
        touching = false;
    }
}
