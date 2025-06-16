using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class save : MonoBehaviour
{
    private bool touching;
    public GameObject savemonitor;
    public TextMeshProUGUI savetext;
    public string currentlocation;
    private bool savescreenon = false;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        SaveData.LoadCutscenes();
        SaveData.LoadStartDialogues();
    }
    void Update()
    {
        if(touching && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SaveFunct());
        }
        if(savescreenon && Input.GetKeyDown(KeyCode.Return))
        {
            savescreenon = false;
            savemonitor.SetActive(false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    IEnumerator SaveFunct()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        savetext.text = "<Memory Saver>";
        savemonitor.SetActive(true);
        yield return new WaitForSeconds(1f);
        Save();
        savetext.text = "saved memories of " + SaveData.playerName + " at [" + currentlocation + "]";
        savescreenon = true;
    }
    void Save()
    {
        SaveData.currentScene = SceneManager.GetActiveScene().buildIndex;
        SaveData.currentPosition = transform.position;
        SaveData.SaveAll();
        SaveData.SaveCutscenes();
        SaveData.SaveStartDialogues();
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
