using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayermoveBattle : MonoBehaviour
{
    public GameObject attackeffect;
    public GameObject backobj;
    public GameObject frontobj;
    public GameObject rightobj;
    public GameObject leftobj;
    public Vector2 movement; 

    private bool front = true;
    private bool back;
    private bool right;
    private bool left;

    private bool cooltime;

    private Rigidbody2D rb;

    void Start()
    {
        cooltime = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        movement.x = 0;
        movement.y = -1;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical"); 
        if(Input.GetKeyDown(KeyCode.F) && cooltime)
        {
            cooltime = false;
            StartCoroutine(CooltimeTracker());
            gameObject.GetComponent<Animator>().SetBool("attack", true);
            StartCoroutine(ResetTriggerTimer());
        }

        if(movement.x != 0 || movement.y != 0) 
        {
            front = false;
            back = false;
            right = false;
            left = false;
        }
        if(movement.x == 0 && movement.y == -1) 
        {
            front = true;
        }
        if(movement.x == 0 && movement.y == 1) 
        {
            back = true;
        }
        if(movement.x == 1 && movement.y == 0) 
        {
            right = true;
        }
        if(movement.x == -1 && movement.y == 0) 
        {
            left = true;
        }
    }

    IEnumerator ResetTriggerTimer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if(front)
        {
            frontobj.SetActive(true);
            Instantiate(attackeffect, frontobj.transform.position, Quaternion.identity);
        }
        else if(back)
        {
            backobj.SetActive(true);
            Instantiate(attackeffect, backobj.transform.position, Quaternion.Euler(0f, 0f, 200f));
        }
        else if(right) 
        {
            rightobj.SetActive(true);
            Instantiate(attackeffect, rightobj.transform.position, Quaternion.Euler(0f, 0f, 123f));
        }
        else if(left)
        {
            leftobj.SetActive(true);
            Instantiate(attackeffect, leftobj.transform.position, Quaternion.Euler(0f, 0f, 290f));
        }
        yield return new WaitForSeconds(0.2f); 
        gameObject.GetComponent<Animator>().SetBool("attack", false);
        frontobj.SetActive(false);
        backobj.SetActive(false);
        rightobj.SetActive(false);
        leftobj.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator CooltimeTracker()
    {
        yield return new WaitForSeconds(0.5f);
        cooltime = true;
    }
}
