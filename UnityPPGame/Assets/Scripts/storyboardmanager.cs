using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class storyboardmanager : MonoBehaviour
{
    public List<GameObject> storylist = new List<GameObject>();
    public List<string> dialoguelist = new List<string>();
    public Transform position;
    public TextMeshProUGUI dialoguetext;
    private int index;
    private int starting;
    public static int nextscene;
    public int stopmusic; 
    public AudioSource music;
    public AudioSource typing;

    void Update()
    {
        if (starting == 0)
        {
            starting = 1;
            NextStory();
        }
        if (Input.GetKeyDown(KeyCode.Return) && index < storylist.Count)
        {
            NextStory();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && index >= storylist.Count)
        {
            SceneManager.LoadScene(nextscene);
        }
        if(stopmusic != 0 && stopmusic == index)
        {
            music.Stop();
        }
    }

    void NextStory()
    {
        StopCoroutine("Subtitles");
        foreach (Transform child in position)
        {
            Destroy(child.gameObject);
        }
        dialoguetext.text = "";
        if (index < storylist.Count)
        {
            Instantiate(storylist[index], position.position, Quaternion.identity, position);
            StartCoroutine("Subtitles");
            index++;
        }
    }

    IEnumerator Subtitles()
    {
        if (index < dialoguelist.Count)
        {
            foreach (char letter in dialoguelist[index])
            {
                if(letter + "" != " ")
                {
                    typing.Play();
                }
                dialoguetext.text += letter;
                yield return new WaitForSeconds(0.07f);
            }
        }
    }
}
