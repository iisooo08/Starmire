using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class titlemenuManager : MonoBehaviour
{
    private int selectionnum = 0;
    public GameObject canvas;
    public TextMeshProUGUI continuetext;
    public GameObject starmark;
    public AudioSource arrowsfx;
    public AudioSource confirmsfx;
    public Transform continuepos;
    public Transform newgamepos; 
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("hasSaveFile"))
        {
            continuetext.color = new Color(0.3f, 0.4f, 0.55f);
            starmark.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && selectionnum < 2)
        {
            arrowsfx.Play();
            transform.position = new Vector2(transform.position.x, transform.position.y + (newgamepos.position.y - continuepos.position.y));
            selectionnum++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && selectionnum > 0)
        {
            arrowsfx.Play();
            transform.position = new Vector2(transform.position.x, transform.position.y - (newgamepos.position.y - continuepos.position.y));
            selectionnum--;
        }
        if(Input.GetKeyDown(KeyCode.Return) && selectionnum == 0)
        {
            if (SaveData.hasSaveFile == true)
            {
                confirmsfx.Play();
                Continue();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Return) && selectionnum == 1)
        {
            confirmsfx.Play();
            StartCoroutine(NewGame());
        }
        else if(Input.GetKeyDown(KeyCode.Return) && selectionnum == 2)
        {
            Quit();
        }
    }

    void Continue()
    {
        StartCoroutine(Transition(SaveData.currentScene));
    }

    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(0.1f);
        if(PlayerPrefs.HasKey("hasSaveFile"))
        {
            //ask if you really wanna reset 
        }
        SaveData.hasSaveFile = true;
        SaveData.currentScene = 3;
        PlayerPrefs.Save();
        StartCoroutine(Transition(1));
    }

    void Quit()
    {
        //content 
    }

    IEnumerator Transition(int scenenum)
    {
        canvas.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(scenenum);
    }
}
