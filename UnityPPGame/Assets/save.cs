using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class save : MonoBehaviour
{
    private bool touching;
    public TextMeshProUGUI savetext;
    // Start is called before the first frame update
    void Update()
    {
        if(touching && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SaveCoroutine());
        }
    }

    IEnumerator SaveCoroutine()
    {
        yield return new WaitForSeconds(1f);
        PlayerPrefs.Save();
        //savetext.enabled = true;
        yield return new WaitForSeconds(3f);
        //savetext.enabled = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            touching = true;
        }
    }
    void OnCollisionExit2D()
    {
        touching = false;
    }
}
