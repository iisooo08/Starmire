using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorial_battlemanager : MonoBehaviour
{
    public List<string> fullatklist = new List<string>();
    public List<GameObject> objlist;
    public List<int> changeorder = new List<int>();
    public List<GameObject> elements = new List<GameObject>(); //list containing one, zero, and bug 
    private int currentindex; //when instantiating the values on the grid, to know which grid should be filled next 
    // Start is called before the first frame update
    private int nextindex; //to know which element of the fullatklist must be instantiated next 
    public List<GameObject> hpos = new List<GameObject>();
    public List<GameObject> vpos = new List<GameObject>();
    public float speed = 0.6f;
    public Slider progressbar; 


    public List<string> dialogue1;
    public string name;
    public List<string> dialogue2;
    public List<string> dialogue3;
    public List<string> dialogue4;
    public List<int> effects;
    private bool once1;
    void Start()
    {
        StartCoroutine(AttackFlow());
    }

    public IEnumerator AttackFlow()
    {
        StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogue1, name, effects));
        yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish);
        yield return new WaitForSeconds(1.5f);
        for(int x = 0; x < fullatklist.Count; x++)
        {
            progressbar.value = nextindex;
            if(x%2 == 0)
            {
                if(once1 == false)
                {
                    once1 = true;
                    StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogue2, name, effects));
                    yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish);
                }
                Instantiate();
            }
            yield return new WaitForSeconds(speed);
            DeleteRow();
            MoveDown();
            if(nextindex == fullatklist.Count - 1)
            {
                StartCoroutine(Remaining());
            }
            else
            {
                nextindex++;
            }
            if(changeorder[x] == 1)
            {
                StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogue3, name, effects));
                yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish);
                GameObject.Find("battle_player").GetComponent<player_battlescript>().ChangeSprite();
            }
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().StartDialogue(dialogue4, name, effects));
        yield return new WaitUntil(() => GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>().finish);
        SceneManager.LoadScene(29);
    }

    public void MoveDown()
    {
        for (int j = 3; j > -1; j--)
        {
            List<GameObject> childrenToMove = new List<GameObject>();

            for (int i = 0; i < vpos[j].transform.childCount; i++)
            {
                GameObject childObject = vpos[j].transform.GetChild(i).gameObject;
                childrenToMove.Add(childObject);
            }

            foreach (GameObject childObject in childrenToMove)
            {
                if (j + 1 < vpos.Count)
                {
                    childObject.transform.position = new Vector3(
                        childObject.transform.position.x,
                        vpos[j + 1].transform.position.y,
                        childObject.transform.position.z
                    );

                    childObject.transform.SetParent(vpos[j + 1].transform, worldPositionStays: true);
                }
            }
        }
    }

    public void Instantiate()
    {
        currentindex = 0;
        for(int x = 0; x < 6; x++)
        {
            GameObject newestline = Instantiate(elements[fullatklist[nextindex][x]-'0'], new Vector2(hpos[currentindex].transform.position.x, vpos[0].transform.position.y), Quaternion.identity);
            newestline.transform.SetParent(vpos[0].transform, worldPositionStays: true);
            currentindex++;
        }
    }

    public IEnumerator Remaining()
    {
        for(int x = 0; x < 3; x++)
        {
            yield return new WaitForSeconds(speed);
            DeleteRow();
            MoveDown();
        }
        yield return new WaitForSeconds(speed);
        DeleteRow();
    }

    public void DeleteRow()
    {
        if(vpos[4].transform.childCount != 0)
        {
            foreach (Transform child in vpos[4].transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
