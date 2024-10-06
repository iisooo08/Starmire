using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battlemanager_script : MonoBehaviour
{
    public List<string> fullatklist = new List<string>();
    public List<GameObject> elements = new List<GameObject>(); //list containing one, zero, and bug 
    private int currentindex; //when instantiating the values on the grid, to know which grid should be filled next 
    // Start is called before the first frame update
    private int nextindex; //to know which element of the fullatklist must be instantiated next 
    public List<GameObject> hpos = new List<GameObject>();
    public List<GameObject> vpos = new List<GameObject>();
    public float speed = 1f;
    void Start()
    {
        StartCoroutine(AttackFlow());
    }

    public IEnumerator AttackFlow()
    {
        for(int x = 0; x < fullatklist.Count; x++)
        {
            Instantiate();
            yield return new WaitForSeconds(speed);
            MoveDown();
            if(nextindex == fullatklist.Count - 1)
            {
                print("battle is over");
            }
            else
            {
                nextindex++;
            }
        }
    }

    public void MoveDown()
    {
        for(int x = 0; x < 4; x++)
        {
            foreach (Transform child in vpos[x].transform)
            {
                GameObject childObject = child.gameObject;
                childObject.transform.position = new Vector3(childObject.transform.position.x, vpos[x+1].transform.position.y, childObject.transform.position.z);
                childObject.transform.SetParent(vpos[x+1].transform, worldPositionStays:true);
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
}
