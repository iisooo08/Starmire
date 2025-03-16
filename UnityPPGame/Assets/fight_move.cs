using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fight_move : MonoBehaviour
{
    private int slotx; 
    private int sloty;
    public AudioSource move;
    private Vector3 currentpos;
    private bool blocked;
    public Slider healthbar;
    // Start is called before the first frame update
    void Start()
    {
        //prevent player from moving towards badguys and taking their position by having a badguymanager that keeps track of all positions
    }

    // Update is called once per frame
    void Update()
    {
        if(blocked == false && Input.GetKeyDown(KeyCode.RightArrow) && slotx < 4 && checkbadguy(0) == false)
        {
            slotx++;
            transform.position += new Vector3(1.4f, 0f, 0f);
            move.Play();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && slotx < 4 && checkbadguy(0) == true)
        {
            slotx++;
            StartCoroutine(HurtAnim());
            StartCoroutine(BlockAnim(0));
        }

        if(blocked == false && Input.GetKeyDown(KeyCode.LeftArrow) && slotx > 0 && checkbadguy(1) == false)
        {
            slotx--;
            transform.position += new Vector3(-1.4f, 0f, 0f);
            move.Play();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && slotx > 0 && checkbadguy(1) == true)
        {
            slotx--;
            StartCoroutine(HurtAnim());
            StartCoroutine(BlockAnim(1));
        }

        if(blocked == false && Input.GetKeyDown(KeyCode.UpArrow) && sloty < 4 && checkbadguy(2) == false)
        {
            sloty++;
            transform.position += new Vector3(0f, 1.4f, 0f);
            move.Play();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && sloty < 4 && checkbadguy(2) == true)
        {
            sloty++;
            StartCoroutine(HurtAnim());
            StartCoroutine(BlockAnim(2));
        }

        if(blocked == false && Input.GetKeyDown(KeyCode.DownArrow) && sloty > 0 && checkbadguy(3) == false)
        {
            sloty--;
            transform.position += new Vector3(0f, -1.4f, 0f);
            move.Play();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && sloty > 0 && checkbadguy(3) == true)
        {
            sloty--;
            StartCoroutine(HurtAnim());
            StartCoroutine(BlockAnim(3));
        }
    }
    bool checkbadguy(int movetype)
    {
        for(int x = 0; x < GameObject.Find("badguy_manager").GetComponent<badguy_manager>().xpos.Count; x++)
        {
            int xpos_temp = GameObject.Find("badguy_manager").GetComponent<badguy_manager>().xpos[x];
            int ypos_temp = GameObject.Find("badguy_manager").GetComponent<badguy_manager>().ypos[x];
            if(movetype == 0 && slotx + 1 == xpos_temp && sloty == ypos_temp)
            {
                return true;
            }
            if(movetype == 1 && slotx - 1 == xpos_temp && sloty == ypos_temp)
            {
                return true;
            }
            if(movetype == 2 && slotx == xpos_temp && sloty + 1 == ypos_temp)
            {
                return true;
            }
            if(movetype == 3 && slotx == xpos_temp && sloty - 1 == ypos_temp)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator BlockAnim(int movetype)
    {
        blocked = true;
        currentpos = transform.position;
        if(movetype == 0)
        {
            transform.position += new Vector3(1.4f, 0f, 0f);
            yield return new WaitForSeconds(0.4f);
            transform.position = currentpos;
            slotx--;
        }
        else if(movetype == 1)
        {
            transform.position += new Vector3(-1.4f, 0f, 0f);
            yield return new WaitForSeconds(0.4f);
            transform.position = currentpos;
            slotx++;
        }
        else if(movetype == 2)
        {
            transform.position += new Vector3(0f, 1.4f, 0f);
            yield return new WaitForSeconds(0.4f);
            transform.position = currentpos;
            sloty--;
        }
        else if(movetype == 3)
        {
            transform.position += new Vector3(0f, -1.4f, 0f);
            yield return new WaitForSeconds(0.4f);
            transform.position = currentpos;
            sloty++;
        }
        blocked = false;
    }
    public IEnumerator HurtAnim()
    {
        gameObject.GetComponent<Animator>().SetTrigger("hurt");
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<Animator>().ResetTrigger("hurt");
        gameObject.GetComponent<Animator>().SetTrigger("alright");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            healthbar.value -= 10;
            if(healthbar.value <= 0)
            {
                //trigger death;
            }
        }
    }
}
