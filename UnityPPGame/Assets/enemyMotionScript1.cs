using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMotionScript1 : MonoBehaviour
{
    //this script is for enemies that: 
    //1. "hide" before the battle starts, in a different form 
    //2. transform into the true form, then attack the player by ramming at them 
    //3. may or may not generate bullets to dodge 
    // Start is called before the first frame update

    public float radius; //how far can the enemy sense the player from? 
    private GameObject player;
    private Animator anim;
    private bool beforetrans;
    private bool attacking;
    private bool defeated;
    public float wait;
    public float hspeed;
    public float vspeed;
    private bool hasstarted;
    public Vector2 minBounds; // bottom left corner of region
    public Vector2 maxBounds; // top right corner of region

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= radius && !hasstarted)
            {
                hasstarted = true;
                StartCoroutine(AttackSeq());
                beforetrans = true;
            }
        }

        if(beforetrans && player.transform.position.x < transform.position.x)
        {
            anim.SetTrigger("front");
            anim.ResetTrigger("back");
        }
        if(beforetrans && player.transform.position.x > transform.position.x)
        {
            anim.SetTrigger("back");
            anim.ResetTrigger("front");
        }
        if(hasstarted == false)
        {
            anim.ResetTrigger("return");
            anim.SetTrigger("transform");
        }
    }

    IEnumerator AttackSeq()
    {
        anim.ResetTrigger("transform");
        anim.SetTrigger("return");
        StartCoroutine(CountDown());
        while (beforetrans)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > radius)
            {
                hasstarted = false;
                yield break;
            }
            MoveToPlayer();
            yield return null;
        }

        anim.ResetTrigger("return");
        anim.SetTrigger("transform");
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(GlideXCoroutine(player.transform.position.x, hspeed));
        yield return new WaitUntil(() => beforetrans);
        StartCoroutine(AttackSeq());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(wait);
        beforetrans = false;
    }

    void MoveToPlayer()
    {
        Vector3 pos = transform.position;
        float targetY = player.transform.position.y;
        pos.y = Mathf.MoveTowards(pos.y, targetY, vspeed * Time.deltaTime); 
        transform.position = pos;
        ClampPosition();
    }

    private IEnumerator GlideXCoroutine(float targetX, float speed)
    {
        // Clamp the target to stay within enemy bounds
        targetX = Mathf.Clamp(targetX, minBounds.x, maxBounds.x);

        while (Mathf.Abs(transform.position.x - targetX) > 0.01f)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.MoveTowards(pos.x, targetX, speed * Time.deltaTime);
            transform.position = pos;
            ClampPosition();
            yield return null;
        }
        beforetrans = true;
    }
}
