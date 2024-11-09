using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class textselection : MonoBehaviour
{
    public static int index;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        text1.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && index < 3)
        {
            index++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && index > 0)
        {
            index--;
            sound.Play();
        }
        if(index == 0)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(2);
            }
            text1.color = Color.blue;
            text2.color = Color.white;
            text3.color = Color.white;
            text4.color = Color.white;
        }
        if(index == 1)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(12);
            }
            text1.color = Color.white;
            text2.color = Color.blue;
            text3.color = Color.white;
            text4.color = Color.white;
        }
        if(index == 2)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(13);
            }
            text1.color = Color.white;
            text2.color = Color.white;
            text3.color = Color.blue;
            text4.color = Color.white;
        }
        if(index == 3)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(14);
            }
            text1.color = Color.white;
            text2.color = Color.white;
            text3.color = Color.white;
            text4.color = Color.blue;
        }
    }
}
