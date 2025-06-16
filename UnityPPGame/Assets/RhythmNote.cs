using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNote : MonoBehaviour
{
    private KeyCode laneKey;
    private bool canJudge = false;
    private bool isInPerfect = false;
    private bool isInOkay = false;

    public GameObject perfectEffect;
    public GameObject okayEffect;
    public GameObject missEffect;
    public int laneNum;

    private Transform effectSpawnPoint;

    void Start()
    {
        if(laneNum == 1)
        {
            laneKey = KeyCode.D;
        }
        else if(laneNum == 2)
        {
            laneKey = KeyCode.F;
        }
        else if(laneNum == 3)
        {
            laneKey = KeyCode.J;
        }
        else if(laneNum == 4)
        {
            laneKey = KeyCode.K;
        }

        // Get effect spawn point
        GameObject spawnObj = GameObject.Find("pos" + laneNum);
        if (spawnObj != null)
        {
            effectSpawnPoint = spawnObj.transform;
        }
        else
        {
            Debug.LogError("Missing effect spawn point for lane " + laneNum);
        }
    }

    void Update()
    {
                if(transform.position.y < -3.6f)
        {
            GameObject.Find("rhythmmanager").GetComponent<RhythmManager>().iscombo = false;
            GameObject.Find("rhythmmanager").GetComponent<RhythmManager>().isperfect = false;
            GameObject.Find("rhythmmanager").GetComponent<RhythmManager>().LowerHearts();
            Instantiate(missEffect, effectSpawnPoint.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (!canJudge) return;

        if (Input.GetKeyDown(laneKey))
        {
            if (isInPerfect)
            {
                GameObject.Find("perfectAudio").GetComponent<AudioSource>().Play();
                Instantiate(perfectEffect, effectSpawnPoint.position, Quaternion.identity);
            }
            else if (isInOkay)
            {
                GameObject.Find("rhythmmanager").GetComponent<RhythmManager>().isperfect = false;
                GameObject.Find("okayAudio").GetComponent<AudioSource>().Play();
                Instantiate(okayEffect, effectSpawnPoint.position, Quaternion.identity);
            }
            Destroy(gameObject); 
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("judgezone"))
        {
            canJudge = true;
        }
        else if (other.CompareTag("perfect"))
        {
            isInPerfect = true;
        }
        else if (other.CompareTag("ok1") || other.CompareTag("ok2"))
        {
            isInOkay = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("judgezone"))
        {
            canJudge = false;
        }
        else if (other.CompareTag("perfect"))
        {
            isInPerfect = false;
        }
        else if (other.CompareTag("ok1") || other.CompareTag("ok2"))
        {
            isInOkay = false;
        }
    }
}
