using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterfly_t1 : MonoBehaviour
{
    public GameObject attack;
    void OnEnable()
    {
        StartCoroutine(InstantiateAttack());
    }

    IEnumerator InstantiateAttack()
    {
        Instantiate(attack, new Vector2(0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("battlemanager").GetComponent<battle_manager>().battleseqtracker++;
        gameObject.SetActive(false);
    }
}
