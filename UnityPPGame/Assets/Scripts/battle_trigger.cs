using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battle_trigger : MonoBehaviour
{
    public int battlescene;
    public static bool battletriggered;
    public bool next;
    public GameObject nextobj;
    public GameObject warningsign;
    public GameObject bugpiece;
    public int battleind;
    public List<GameObject> blockage = new List<GameObject>();
    public Transform battletransform;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            battle_static.battleindex = battleind;
            battle_static.prevscene = SceneManager.GetActiveScene().buildIndex;
            battletriggered = true;
        }
        SceneManager.LoadScene(battlescene);
    }
    void Start()
    {
        if(battletriggered == true)
        {
            battletriggered = false;
            warningsign.SetActive(false);
            //bugpiece.SetActive(true);
            if(next)
            {
                nextobj.SetActive(true);
            }
            for(int x = 0; x < blockage.Count; x++)
            {
                blockage[x].SetActive(false);
            }
            GameObject.Find("playersprite_final").transform.position = battletransform.position;
            gameObject.SetActive(false);
        }
    }
}
