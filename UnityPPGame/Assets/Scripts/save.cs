using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class save : MonoBehaviour
{
    private bool touching;
    public bool savescreenon = false;
    public Rigidbody2D playerRb;
    public Transform playerTransform;
    public string location;

    void Start()
    {
        if (!SaveData.hasLoadedThisSession)
        {
            SaveData.LoadAll();
            SaveData.hasLoadedThisSession = true;
        }
    }

    void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.Return) && !savescreenon)
        {
            SaveFunct();
        }
    }

    void SaveFunct()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        GameObject.Find("savescreen").GetComponent<UI_savescreen>().SaveScreenFunct();
        savescreenon = true;
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