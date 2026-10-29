using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class nameManager : MonoBehaviour
{
    public TextMeshProUGUI[,] alphabets = new TextMeshProUGUI[4, 7];
    public List<TextMeshProUGUI> temp;
    private string alphabetstring = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private int xind = 0;
    private int yind = 0;
    public TextMeshProUGUI currentname;
    public GameObject arrow;
    public GameObject checkcanvas;
    public GameObject alphabetcanvas;
    private int arrownum;
    private bool checking;
    private bool animended;
    public Transform arrowpos;
    public AudioSource arrowsfx;
    public AudioSource confirmsfx;
    public Animator canvas;

    void Start()
    {
        canvas.SetTrigger("start");
        currentname.text = "";
        int index = 0;
        for(int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 7; y++)
            {
                alphabets[x, y] = temp[index];
                index++;
            }
        }
        UpdateLetters();
    }
    
    void Update()
    {
        ColorUtility.TryParseHtmlString("#D2EEF2", out Color paleBlue);
        if(Input.GetKeyDown(KeyCode.LeftArrow) && !checking)
        {
            alphabets[xind, yind].color = paleBlue;
            if(yind > 0)
            {  
                yind--;
            }
            else
            {
                yind = 6;
            }
            arrowsfx.Play();
            UpdateLetters();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) && !checking)
        {
            alphabets[xind, yind].color = paleBlue;
            if(yind < 6)
            {
                yind++;
            }
            else
            {
                yind = 0;
            }
            arrowsfx.Play();
            UpdateLetters();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && !checking)
        {
            alphabets[xind, yind].color = paleBlue;
            if(xind > 0)
            {
                xind--;
            }
            else
            {
                xind = 3;
            }
            arrowsfx.Play();
            UpdateLetters();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && !checking)
        {
            alphabets[xind, yind].color = paleBlue;
            if(xind < 3)
            {
                xind++;
            }
            else
            {
                xind = 0;
            }
            arrowsfx.Play();
            UpdateLetters();
        }
        if(Input.GetKeyDown(KeyCode.Return) && !checking)
        {
            if(xind == 3 && yind == 5 && currentname.text.Length > 0)
            {
                confirmsfx.Play();
                currentname.text = currentname.text.Substring(0, currentname.text.Length - 1); 
            }
            else if(xind == 3 && yind == 6 && currentname.text.Length > 0)
            {
                confirmsfx.Play();
                alphabetcanvas.SetActive(false);
                checking = true;
                StartCoroutine(FinalizeAnim());
            }
            else if(currentname.text.Length < 7)
            {
                confirmsfx.Play();
                currentname.text = currentname.text + (alphabetstring[xind * 7 + yind]);
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) && checking && arrownum == 0)
        {
            arrowsfx.Play();
            arrownum++;
            arrow.transform.position = new Vector2(arrow.transform.position.x + 6f, arrow.transform.position.y);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && checking && arrownum == 1)
        {
            arrowsfx.Play();
            arrownum--;
            arrow.transform.position = new Vector2(arrow.transform.position.x - 6f, arrow.transform.position.y);
        }
        if(Input.GetKeyDown(KeyCode.Return) && checking && animended)
        {
            confirmsfx.Play();
            if(arrownum == 0)
            {
                StartCoroutine(TransitionAnim());
            }
            else 
            {
                arrow.transform.position = arrowpos.position;
                animended = false;
                arrownum = 0;
                checking = false;
                checkcanvas.SetActive(false);
                arrow.SetActive(false);
                alphabetcanvas.SetActive(true);
            }
        }
    }

    IEnumerator TransitionAnim()
    {
        canvas.SetTrigger("end");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(11);
    }

    void UpdateLetters()
    {
        alphabets[xind, yind].color = new Color(0.3f, 0.4f, 0.55f);
    }

    IEnumerator FinalizeAnim()
    {
        string current = currentname.text;
        currentname.text = "";
        int typingindex = 0;
        foreach (char letter in current)
        {
            typingindex++;
            currentname.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
        checkcanvas.SetActive(true);
        confirmsfx.Play();
        arrow.SetActive(true);
        animended = true;
    }
}
