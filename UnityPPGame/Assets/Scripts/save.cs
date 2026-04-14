using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class save : MonoBehaviour
{
    private bool touching;
    public string currentlocation;
    private bool savescreenon = false;
    public Rigidbody2D playerRb;
    public Transform playerTransform;

    void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.Return) && !savescreenon)
        {
            StartCoroutine(SaveFunct());
        }

        if (savescreenon && Input.GetKeyDown(KeyCode.Return))
        {
            savescreenon = false;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    IEnumerator SaveFunct()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(1f);

        Save();

        savescreenon = true;
    }

    void Save()
    {
        SaveData.currentScene = SceneManager.GetActiveScene().buildIndex;
        SaveData.currentPosition = playerTransform.position;
        SaveData.hasSaveFile = true;
        SaveData.SaveAll();
        Debug.Log("saved");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            touching = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            touching = false;
        }
    }
}