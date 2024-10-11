using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Instantiate(redsquare, gameObject.transform.position, Quaternion.identity);
    }

    void displaygreen()
    {
        Instantiate(greensquare, gameObject.transform.position, Quaternion.identity);
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
        healthbar.value = health;
    }
}
