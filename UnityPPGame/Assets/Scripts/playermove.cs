using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float movespeed = 4f; 
    public Rigidbody2D rb; 
    Vector2 movement; 
    public Animator anim; 

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical"); 
        anim.SetFloat("Horizontal", movement.x); 
        anim.SetFloat("Vertical", movement.y); 
        if(movement.x != 0 || movement.y != 0) 
        {
            anim.SetFloat("Speed", 1); 
            anim.ResetTrigger("front"); 
            anim.ResetTrigger("back"); 
            anim.ResetTrigger("left"); 
            anim.ResetTrigger("right"); 
        }
        else 
        {
            anim.SetFloat("Speed", 0); 
        }

        if(movement.x == 0 && movement.y == -1) 
        {
            anim.SetTrigger("front"); 
        }
        if(movement.x == 0 && movement.y == 1) 
        {
            anim.SetTrigger("back"); 
        }
        if(movement.x == 1 && movement.y == 0) 
        {
            anim.SetTrigger("right"); 
        }
        if(movement.x == -1 && movement.y == 0) 
        {
            anim.SetTrigger("left"); 
        }
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime); 
    }

}
