using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyItemScript : MonoBehaviour
{
    public int num;
    public int animationnum;
    public AudioSource pickup;
    public GameObject player;
    public static Vector3 playerpos;
    public static bool obtained = false;
    private List<string> keydialogue = new List<string>() 
    {
        "You obtained a Key Item!"
    };
    private List<string> keyname = new List<string>()
    {
        ""
    };
    private List<int> keyeffects = new List<int>()
    {
        0
    };

    void Start()
    {
        if(obtained)
        {
            player.transform.position = playerpos;
            obtained = false;
        }
        if(SaveData.HasItem("item_"+num))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            obtained = true;
            StartCoroutine(ClaimItem());
        }
    }

    IEnumerator ClaimItem()
    {
        pickup.Play();
        GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(keydialogue, keyname, keyeffects);
        yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish == true);
        SaveData.GetItem("item_"+num);
        GeneralAnimationManager.animationNumber = animationnum;
        playerpos = player.transform.position;
        GeneralAnimationManager.prevscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(3);
    }
}
