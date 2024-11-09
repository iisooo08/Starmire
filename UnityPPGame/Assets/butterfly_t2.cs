using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterfly_t2 : MonoBehaviour
{
    public GameObject attack;
    void OnEnable()
    {
        StartCoroutine(InstantiateAttack());
    }

    IEnumerator InstantiateAttack()
    {
        print("instantiate entered");
        Instantiate(attack, new Vector2(1, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        GameObject.Find("battlemanager").GetComponent<battle_manager>().battleseqtracker++;
        gameObject.SetActive(false);
    }
}
