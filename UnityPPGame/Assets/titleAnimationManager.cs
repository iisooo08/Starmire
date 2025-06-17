using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleAnimationManager : MonoBehaviour
{
    public List<Transform> positions;
    public GameObject star;
    public GameObject title;
    public AudioSource sfx;
    public AudioSource titlesong;
    public GameObject credits;
    private List<GameObject> stars = new List<GameObject>();
    public GameObject selection;
    public GameObject arrow;
    public GameObject canvas;
    private bool skippable = false;
    private Coroutine animCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        canvas.GetComponent<Animator>().SetTrigger("start");
        if(SaveData.hasWatchedTitle)
        {
            skippable = true;
        }
        animCoroutine = StartCoroutine(Animations());
    }

    void Update()
    {
        if(skippable && Input.GetKeyDown(KeyCode.Return))
        {
            if (animCoroutine != null)
            {
                StopCoroutine(animCoroutine);
                animCoroutine = null;
            }   
            title.SetActive(false);
            credits.SetActive(false);
            selection.SetActive(true);
            arrow.SetActive(true);
            DestroyStars();
            sfx.Stop();
            titlesong.Play();
        }
    }

    IEnumerator Animations()
    {
        yield return new WaitForSeconds(0.5f);
        sfx.Play();
        for(int x = 0; x < positions.Count; x++)
        {
            stars.Add(Instantiate(star, positions[x].position, Quaternion.identity));
            yield return new WaitForSeconds(0.2727f);
        }
        yield return new WaitForSeconds(0.6f);
        title.SetActive(true);
        yield return new WaitForSeconds(9f);
        DestroyStars();
        title.SetActive(false);
        credits.SetActive(true);
        yield return new WaitForSeconds(5f);
        credits.SetActive(false);
        yield return new WaitForSeconds(1f);
        selection.SetActive(true);
        arrow.SetActive(true);
        SaveData.hasWatchedTitle = true;
        PlayerPrefs.Save();
        yield return new WaitForSeconds(0.3f);
        titlesong.Play();
    }

    void DestroyStars()
    {
        for(int x = 0; x < stars.Count; x++)
        {
            Destroy(stars[x]);
        }
    }
}
