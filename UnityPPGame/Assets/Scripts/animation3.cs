using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animation3 : MonoBehaviour
{
    public List<string> dialogue;
    public List<string> names;
    public List<int> effects;
    private bool ended;
    public GameObject soldier;
    private Animator soldieranim;
    private bool panned = false;
    public Transform player;
    private bool checkcollider = false;
    public int battlescene;
    public static bool battletriggered;
    public int battleind;
    public Transform playerpos;
    public GameObject errorsoldier;

    // Use a unique key for this animation
    private const string AnimationPlayedKey2 = "AnimationPlayed2";

    void Start()
    {
        if (PlayerPrefs.GetInt(AnimationPlayedKey2, 0) == 1)
        {
            checkcollider = false;
            return; // Exit if the animation has already played
        }
        else
        {
            checkcollider = true;
        }

    }
    void Update()
    {
        if(battletriggered == true)
        {
            battletriggered = false;
            player.position = playerpos.position;
            errorsoldier.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    IEnumerator AnimationSeq()
    {
        soldieranim = soldier.GetComponent<Animator>();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(DialogueSequence());
        yield return new WaitUntil(() => ended);

        // Mark the animation as played
        PlayerPrefs.SetInt(AnimationPlayedKey2, 1);
        PlayerPrefs.Save();
    }

    private IEnumerator DialogueSequence()
    {
        string prevname = "";
        List<string> currentdialogue = new List<string>();
        List<int> currenteffects = new List<int>();

        for (int x = 0; x < names.Count; x++)
        {
            if (x == 3)
            {
                yield return new WaitForSeconds(0.5f);
                soldieranim.SetTrigger("glitch");
            }

            if (x == 0)
            {
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else if (prevname == names[x])
            {
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else
            {
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
                currenteffects.Clear();
                currentdialogue.Clear();
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }

            if (x == names.Count - 1)
            {
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
            }
        }
        GameObject.Find("playersprite_final").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(0.5f);
        battle_static.battleindex = battleind;
            battle_static.prevscene = SceneManager.GetActiveScene().buildIndex;
            battletriggered = true;
            SceneManager.LoadScene(battlescene);
        ended = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") )//&& checkcollider)
        {
            StartCoroutine(AnimationSeq());
        }
    }
}
