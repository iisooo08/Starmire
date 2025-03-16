using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battle_trigger : MonoBehaviour
{
    public int battlescene;
    public bool next;
    public GameObject nextobj;
    public GameObject warningsign;
    public int battleind;
    public List<GameObject> blockage = new List<GameObject>();
    public Transform battletransform;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            stat_manager.battlenum = battleind;
            stat_manager.prevscene = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("battletriggered" + battleind, 1);
        }
        SceneManager.LoadScene(battlescene);
    }
    void Start()
    {
        if(PlayerPrefs.GetInt("battletriggered" + battleind, 0) == 1)
        {
            warningsign.SetActive(false);
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
