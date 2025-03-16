using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class antierror_manager : MonoBehaviour
{
    public GameObject antierror;
    public int errorscale;
    public bool boss;
    public Slider errorslider;
    public void AddAntierror()
    {
        if(!boss)
        {
            errorscale += 25;
            errorslider.value = errorscale;
        }
        else
        {
            errorscale += 10;
            errorslider.value = errorscale;
        }
        if(errorscale >= 100) 
        {
            StartCoroutine(LoadNext());
        }
    }
    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(stat_manager.prevscene);
    }
}
