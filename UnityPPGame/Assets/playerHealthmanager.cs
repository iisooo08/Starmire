using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthmanager : MonoBehaviour
{
    public int healthnum = 20;
    public List<GameObject> heartobjs;
    public List<Sprite> heartsprites;
    private bool cooled;
    private float flickerDuration = 1f;
    private float flickerInterval = 0.2f;
    public SpriteRenderer spriteRenderer;
    private bool touching;
    
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(touching && cooled == false)
        {
            cooled = true;
            StartCoroutine(DecreaseCool());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy") && cooled == false)
        {
            touching = true;
            cooled = true;
            StartCoroutine(DecreaseCool());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("enemy"))
        {
            touching = false;
        }
    }

    IEnumerator DecreaseCool()
    {
        DecreaseHealth();

        float elapsed = 0f;
        bool isVisible = true;

        while (elapsed < flickerDuration)
        {
            isVisible = !isVisible;
            Color c = spriteRenderer.color;
            c.a = isVisible ? 1f : 0.3f; // opaque or semi-transparent
            spriteRenderer.color = c;

            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        // Ensure visibility is restored at the end
        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f;
        spriteRenderer.color = finalColor;
        cooled = false;
    }

    void DecreaseHealth()
    {
        if(healthnum % 4 == 1)
        {
            //one quarter 
            if(healthnum > 1)
            {
                heartobjs[((healthnum + 3)/4) - 1].GetComponent<SpriteRenderer>().sprite = heartsprites[0];
            }
            else
            {
                heartobjs[((healthnum + 3)/4) - 1].GetComponent<SpriteRenderer>().sprite = heartsprites[0];
                print("player died");
            }
        }
        else if(healthnum % 4 == 2)
        {
            //one half 
            heartobjs[((healthnum + 3)/4) - 1].GetComponent<SpriteRenderer>().sprite = heartsprites[1];
        }
        else if(healthnum % 4 == 3)
        {
            //three quarters 
            heartobjs[((healthnum + 3)/4) - 1].GetComponent<SpriteRenderer>().sprite = heartsprites[2];
        }
        else if(healthnum % 4 == 0)
        {
            //full 
            heartobjs[((healthnum + 3)/4) - 1].GetComponent<SpriteRenderer>().sprite = heartsprites[3];
        }
        healthnum--;
    }

    void IncreaseHealth()
    {
        //eat item
    }
}
