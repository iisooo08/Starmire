using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class titleStoryManager : MonoBehaviour
{
    public TextMeshProUGUI centerText;
    public TextMeshProUGUI bottomText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    private List<string> dialogues = new List<string>();
    public AudioSource typing;
    private int index;
    private int corindex;
    private List<string> dualList = new List<string>();
    private List<bool> completed = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < 7; x++)
        {
            completed.Add(false);
        }

        dialogues.Add("* It is said that our universe was born from a star. ");
        dialogues.Add("placeholder");
        dialogues.Add("* Now, many more stars shine bright. And many, many more planets orbit such stars. ");
        dialogues.Add("* But stars grow old. And when a star dies, it pulls in everything within its grasp. ");
        dialogues.Add("* We will then be devoured by its strength, finally returning to our cradle. ");
        dialogues.Add("* That is an unavoidable fate. ");
        dialogues.Add("* Did you understand what I just said? ");
        dualList.Add("* A bright, burning star that exploded into a million different pieces---");
        dualList.Add("* beneath which all living beings are brothers and sisters. ");
        
        StartCoroutine(MainLoop());
    }

    IEnumerator MainLoop()
    {
        for(int x = 0; x < 7; x++)
        {
            ResetAllTexts();
            index = x;
            if(x == 0 || x == 5 || x == 6)
            {
                StartCoroutine(MonoSubtitles(centerText));
            }
            else if(x == 1)
            {
                StartCoroutine(DualSubtitles());
            }
            else if(x == 2 || x == 3 || x == 4)
            {
                StartCoroutine(MonoSubtitles(bottomText));
            }
            yield return new WaitUntil(() => completed[x]);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    void ResetAllTexts()
    {
        centerText.text = "";
        bottomText.text = "";
        leftText.text = "";
        rightText.text = "";
    }

    IEnumerator MonoSubtitles(TextMeshProUGUI textobj)
    {
        yield return new WaitForSeconds(0.3f);
        corindex = 0;
        foreach (char letter in dialogues[index])
        {
            if(corindex % 2 == 0)
            {
                typing.Play();
            }
            textobj.text += letter;
            yield return new WaitForSeconds(0.07f);
            corindex++;
        }
        yield return new WaitForSeconds(2f);
        completed[index] = true;
    }
    
    IEnumerator DualSubtitles()
    {
        yield return new WaitForSeconds(0.3f);
        corindex = 0;
        foreach (char letter in dualList[0])
        {
            if(corindex % 2 == 0 && dualList[0][corindex] != ' ')
            {
                typing.Play();
            }
            leftText.text += letter;
            yield return new WaitForSeconds(0.05f);
            corindex++;
        }
        corindex = 0;
        foreach (char letter in dualList[1])
        {
            if(corindex % 2 == 0 && dualList[1][corindex] != ' ')
            {
                typing.Play();
            }
            rightText.text += letter;
            yield return new WaitForSeconds(0.05f);
            corindex++;
        }
        yield return new WaitForSeconds(2f);
        completed[index] = true;
    }
}
