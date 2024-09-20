using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DialogueManager : MonoBehaviour
{
    public Animator animator; 
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI dialogueText; 
    public GameObject pfpbox; 
    private Queue<string> sentences; 
    public static int isopen_int; 
    public Rigidbody2D rb; 
    public GameObject temp; 
    public GameObject nametag; 
    public static bool detect = false; 
    public static string sentence; 
    public static int end_dialogue; 
    public GameObject nextarrow; 
    public static bool fromlever2; 
    public static string objname; 
    public static bool nextarrow_bool = false; 

    void Start()
    {
        sentences = new Queue<string>(); 
        GameObject.Find("choicearrow").SetActive(false); 
    }

    public void StartDialogue(Dialogue dialogue) 
    {
        end_dialogue = 0; 
        objname = dialogue.privname; 
        fromlever2 = dialogue.fromlever; 
        animator.SetBool("isopen", true); 
        nextarrow.SetActive(true); 
        if(dialogue.pfp_int == 1) 
        {
            temp = Instantiate(dialogue.pfp, new Vector2(pfpbox.transform.position.x, pfpbox.transform.position.y), Quaternion.identity, pfpbox.transform); 
            pfpbox.SetActive(true); 
            print("pfpbox set active"); 
        }
        else 
        {
            pfpbox.SetActive(false); 
        }
        if(dialogue.nametag_int == 1) 
        {
            nametag.SetActive(true); 
            nameText.enabled = true; 
            nameText.text = dialogue.name; 
            print("nametag set active"); 
        }
        else 
        {
            nametag.SetActive(false); 
            nameText.enabled = false; 
        }
        isopen_int = 1; 

        sentences.Clear(); 
        foreach(string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence(); 

    }
    public void DisplayNextSentence() 
    {
        if(sentences.Count == 0) 
        {
            end_dialogue = 1; 
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; 
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX; 
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
            Destroy(temp); 
            EndDialogue(); 
            return; 
        }
        sentence = sentences.Dequeue(); 
        StopAllCoroutines(); 
        if(sentence == "choice") 
        {
            FindObjectOfType<ChoiceManager>().Choice(); 
        }
        else 
        {
            StartCoroutine(TypeSentence(sentence)); 
        }
    }

    IEnumerator TypeSentence (string sentence) 
    {
        dialogueText.text = ""; 
        detect = true; 
        foreach (char letter in sentence.ToCharArray())
        {
            nextarrow.SetActive(false); 
            nextarrow_bool = false; 
            if(dialoguemanager_small.stopdialogue == true) 
            {
                dialoguemanager_small.stopdialogue = false; 
                break; 
            }
            dialogueText.text += letter; 
            yield return new WaitForSeconds(0.02f); 
        }
        nextarrow.SetActive(true); 
        nextarrow_bool = true; 
        detect = false; 
    }
    public void EndDialogue() 
    {
        animator.SetBool("isopen", false); 
        StartCoroutine(microsecond()); 
        isopen_int = 0; 
    }

    IEnumerator microsecond() 
    {
        yield return new WaitForSeconds(1f); 
        pfpbox.SetActive(true); 
        nametag.SetActive(true); 
        nameText.enabled = true; 
        end_dialogue = 0; 
    }
}
