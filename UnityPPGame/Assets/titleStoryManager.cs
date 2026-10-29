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

        dialogues.Add("* Long ago, a lone planet is born from the death of a large star, and soon, a kingdom settles in its plains.");
        dialogues.Add("* One day, astronomers observe four shooting stars crash onto the kingdom’s land.");
        dialogues.Add("* Then, the royal prophet awakens from her slumber, and informs everyone the following prophecy: ");
        dialogues.Add("* Four stars have fallen from the heavens, and three shall be beacons of hope in our land.");
        dialogues.Add("* But the fourth is a curse; for it will engulf its surroundings and bring doom to our people.");
        dialogues.Add("* An age of darkness will follow, until a chosen hero, born with a star on her back, collects the three stars of hope to banish the curse of the fourth.");
        
        StartCoroutine(MainLoop());
    }

    IEnumerator MainLoop()
    {
        for(int x = 0; x < 6; x++)
        {
            ResetAllTexts();
            index = x;
            StartCoroutine(MonoSubtitles(bottomText));
            yield return new WaitUntil(() => completed[x]);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
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
