using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_1 : MonoBehaviour
{
    public Vector2 queenpos; 
    public List<string> dialogue; // List of dialogue lines
    public List<string> names; // List of character names corresponding to each dialogue
    public List<int> effects; // Effects for each dialogue line
    private bool ended;
    public GameObject queen;
    private Animator queenanim;
    public GameObject taskobj;

    void Start()
    {
        StartCoroutine(AnimationSeq());
    }

    IEnumerator AnimationSeq()
    {
        queen = Instantiate(queen, queenpos, Quaternion.identity);
        queenanim = queen.GetComponent<Animator>(); // Assigning to the class-level variable
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueSequence());
        yield return new WaitUntil(() => ended);
        queenanim.SetTrigger("rage");
        yield return new WaitForSeconds(0.4f);
        Destroy(queen);
    }

    private IEnumerator DialogueSequence()
    {
        print("Dialogue sequence entered");
        string prevname = ""; // To track the previous speaker's name
        List<string> currentdialogue = new List<string>(); // Temporary storage for current dialogue
        List<int> currenteffects = new List<int>(); // Temporary storage for current effects

        for (int x = 0; x < names.Count; x++)
        {
            if (x == 1)
            {
                queenanim.SetTrigger("frontmove");
                for (int i = 0; i < 3; i++) // Fixing the loop variable
                {
                    Vector3 position = queen.transform.position;
                    position.y -= 0.2f;
                    queen.transform.position = position;
                    yield return new WaitForSeconds(0.1f);
                }
                queenanim.SetTrigger("back");
            }
            if (x == 3)
            {
                queenanim.SetTrigger("rage");
                yield return new WaitForSeconds(0.4f);
                queenanim.SetTrigger("back");
            }

            // Initialize the first dialogue entry
            if (x == 0)
            {
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else if (prevname == names[x]) // Same speaker as before
            {
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else // Speaker has changed
            {
                // Trigger dialogue for the previous speaker
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));

                // Clear temporary lists and prepare for the new speaker
                currenteffects.Clear();
                currentdialogue.Clear();
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }

            // If it's the last dialogue entry, trigger it
            if (x == names.Count - 1)
            {
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
            }
        }
        GameObject.Find("playersprite_final").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(0.5f);
        taskobj.SetActive(true);
        ended = true;
    }
}
