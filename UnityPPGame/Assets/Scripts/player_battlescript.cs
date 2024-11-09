using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class player_battlescript : MonoBehaviour
{
    public static int startingnum;
    public int currentnum;
    public List<Sprite> sprites = new List<Sprite>();
    public SpriteRenderer spriteRenderer;
    public int health; 
    public GameObject redsquare;
    public GameObject greensquare;
    public Slider healthbar;
    public AudioSource good;
    public AudioSource bad;
    public AudioSource mission;
    public int combo; 
    public TextMeshProUGUI combotext;
    public Animator comboanim; 
    // Start is called before the first frame update
    void Start()
    {
        currentnum = startingnum;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(startchange());
    }

    IEnumerator startchange()
    {
        yield return new WaitForSeconds(2f);
        spriteRenderer.sprite = sprites[startingnum];
    }

    public void ChangeSprite()
    {
        if(currentnum == 0)
        {
            spriteRenderer.sprite = sprites[1];
            currentnum = 1;
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
            currentnum = 0;
        }
    }

    void displayred()
    {
        bad.Play();
        combo = 0; 
        combotext.text = combo + "";
        Instantiate(redsquare, gameObject.transform.position, Quaternion.identity);
    }

    void displaygreen()
    {
        good.Play();
        combo++; 
        combotext.text = combo + ""; 
        Instantiate(greensquare, gameObject.transform.position, Quaternion.identity);
        StartCoroutine(AnimateCombo());
    }

    IEnumerator AnimateCombo()
    {
        comboanim.SetTrigger("anim");
        yield return new WaitForSeconds(0.12f);
        comboanim.SetTrigger("back");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("zero"))
        {
            if(currentnum == 1)
            {
                displayred();
                health--;
            }
            else
            {
                displaygreen();
            }
        }
        else if(other.gameObject.CompareTag("one"))
        {
            if(currentnum == 0)
            {
                displayred();
                health--;
            }
            else
            {
                displaygreen();
            }
        }
        else if(other.gameObject.CompareTag("bug"))
        {
            displayred();
            health--;
        }
        if(health <= 0)
        {
            SceneManager.LoadScene(10);
        }
        healthbar.value = health;
    }
}
