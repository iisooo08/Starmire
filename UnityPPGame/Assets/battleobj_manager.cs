using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleobj_manager : MonoBehaviour
{
    public List<GameObject> objlist;
    public List<int> animlist;
    public List<GameObject> animobj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartAttack()
    {
        if(animlist[stat_manager.battlenum] == 0)
        {
            Anim1();
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(objlist[stat_manager.battlenum].GetComponent<attack_obj>().Attack()); 
    }
    void Anim1() //random generation 
    {
        for(int x = 0; x < 4; x++)
        {
            Instantiate(animobj[stat_manager.battlenum], new Vector2(Random.Range(-10f, -4f), Random.Range(-4f, 5f)), Quaternion.identity);
        }
        for(int x = 0; x < 4; x++)
        {
            Instantiate(animobj[stat_manager.battlenum], new Vector2(Random.Range(5f, 11f), Random.Range(-4f, 5f)), Quaternion.identity);
        }
        StartCoroutine(Break());
    }
    IEnumerator Break()
    {
        yield return new WaitForSeconds(5f);
        if(animlist[stat_manager.battlenum] == 0)
        {
            Anim1();
        }
    }
}
