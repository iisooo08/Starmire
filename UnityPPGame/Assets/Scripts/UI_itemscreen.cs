using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_itemscreen : MonoBehaviour
{
    public bool isactive = false;
    public GameObject itemscreen;
    public List<GameObject> items;
    public TextMeshProUGUI weapontype;
    public TextMeshProUGUI armortype;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && isactive == false && GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish && !GameObject.Find("taskbook").GetComponent<taskbookScript>().taskActive)
        {
            GameObject.Find("playersprite_final").GetComponent<PlayermoveBattle>().FreezeBody();
            isactive = true;
            LoadItems();
            itemscreen.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.I) && isactive == true)
        {
            GameObject.Find("playersprite_final").GetComponent<PlayermoveBattle>().UnfreezeBody();
            isactive = false;
            UnloadItems();
            itemscreen.SetActive(false);
        }
    }

    private void LoadItems()
    {
        for(int x = 0; x < 8; x++)
        {
            if(SaveData.HasItem("item_"+(x+1)))
            {
                items[x].SetActive(true);
            }
        }
    }
    private void UnloadItems()
    {
        for(int x = 0; x < 8; x++)
        {
            items[x].SetActive(false);
        }
    }
}
