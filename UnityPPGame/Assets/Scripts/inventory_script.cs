using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_script : MonoBehaviour
{
    public GameObject inventorybox;
    private bool once;
    public static List<GameObject> itemslist = new List<GameObject>();
    private List<Transform> positionlist = new List<Transform>();
    private bool ready;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 1; x < 13; x++)
        {
            positionlist.Add(GameObject.Find(x + "").transform);
        }
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && once == false && GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish == true && ready == true)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            once = true;
            inventorybox.SetActive(true);
            for(int x = 0; x < itemslist.Count; x++)
            {
                GameObject item1 = Instantiate(itemslist[x], positionlist[x].position, Quaternion.identity, inventorybox.transform);
                item1.GetComponent<Animator>().SetBool("idle", true);
            }
        }
        else if(Input.GetKeyDown(KeyCode.I) && once == true)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            once = false;
            foreach (Transform child in inventorybox.transform)
            {
                Destroy(child.gameObject);
            }
            inventorybox.SetActive(false);
        }
    }
}
