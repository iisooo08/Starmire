using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodguy_script : MonoBehaviour
{
    public AudioSource goodguy;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            StartCoroutine(WaitForAnim());
        }
    }
    IEnumerator WaitForAnim()
    {
        anim.SetTrigger("trigger");
        goodguy.Play();
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
