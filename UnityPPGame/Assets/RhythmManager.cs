using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RhythmManager : MonoBehaviour
{
    public List<GameObject> objlist; 
    public static int P1_gamenum = 0;
    private GameObject current;
    private List<string> currentlist = new List<string>();
    public List<Transform> spawnpos;
    private int currentindex;
    public List<GameObject> noteprefab;
    private List<float> waitlist = new List<float>();

    public bool isperfect = true;
    public bool iscombo = true;

    public List<GameObject> hearts;
    public int heartcount = 5;

    public AudioSource music;

    public GameObject heartobj;
    public GameObject coverobj;
    public AudioSource loudtick;
    public AudioSource smalltick;
    public Transform effectpos;
    public GameObject ap;
    public GameObject fc;
    public GameObject clear;
    public GameObject starting;
    public AudioSource congrats1;
    public AudioSource congrats2;
    public AudioSource danger;
    public AudioSource dangershort;
    public AudioSource toobad;
    public GameObject fullcover;
    public GameObject gameoverpic;
    public Sprite winsprite;
    public string winstring;
    public TextMeshProUGUI endingtext;

    private bool once = true;

    public Sprite deadsprite;
    // Start is called before the first frame update
    void Start()
    {
        current = objlist[P1_gamenum];
        RetrieveList();
        StartCoroutine(RhythmGame());
    }

    void RetrieveList()
    {
        currentlist.Clear();
        for(int x = 0; x < current.GetComponent<RhythmObject>().rhythmseq.Count; x++)
        {
            currentlist.Add(current.GetComponent<RhythmObject>().rhythmseq[x]);
        }
        waitlist.Clear();
        for(int x = 0; x < current.GetComponent<RhythmObject>().waitseq.Count; x++)
        {
            waitlist.Add(current.GetComponent<RhythmObject>().waitseq[x]);
        }
    }

    IEnumerator RhythmGame()
    {
        yield return new WaitForSeconds(1f);
        smalltick.Play();
        coverobj.SetActive(true);
        yield return new WaitForSeconds(1f);
        loudtick.Play();
        heartobj.SetActive(true);
        yield return new WaitForSeconds(1f);
        congrats1.Play();
        Instantiate(starting, effectpos.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayMusic());
        for(int x = 0; x < currentlist.Count; x++)
        {
            for(int y = 0; y < currentlist[x].Length; y++)
            {
                currentindex = y;
                if((currentlist[x][y] + "").Equals("1"))
                {
                    InstantiateNote();
                }
            }
            yield return new WaitForSeconds(waitlist[x]);
        }
        
        yield return new WaitForSeconds(4.5f);
        music.Stop();
        if(isperfect)
        {
            Instantiate(ap, effectpos.position, Quaternion.identity);
            congrats2.Play();
        }
        else if(iscombo)
        {
            Instantiate(fc, effectpos.position, Quaternion.identity);
            congrats1.Play();
        }
        else
        {
            Instantiate(clear, effectpos.position, Quaternion.identity);
            congrats1.Play();
        }
        yield return new WaitForSeconds(1.5f);
        fullcover.SetActive(true);
        loudtick.Play();
        yield return new WaitForSeconds(1f);
        smalltick.Play();
        gameoverpic.GetComponent<SpriteRenderer>().sprite = winsprite;
        endingtext.text = winstring;
        gameoverpic.SetActive(true);
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(3.36f);
        music.Play();
    }

    void InstantiateNote()
    {
        Instantiate(noteprefab[currentindex], spawnpos[currentindex].position, Quaternion.identity);
    }

    void UpdateHearts()
{
    for (int i = 0; i < hearts.Count; i++)
    {
        if(i >= heartcount)
        {
            hearts[i].GetComponent<SpriteRenderer>().sprite = deadsprite;
        }
    }
}

    public void LowerHearts()
    {
        if(heartcount > 0)
        {        
            heartcount--;
            dangershort.Play();
            UpdateHearts();
        }
        if(heartcount == 1)
        {
            danger.Play();
        }
        if(heartcount <= 0 && once)
        {
            StartCoroutine(EndGameSeq());
            once = false;
            
        }
    }

    IEnumerator EndGameSeq()
    {
        music.Stop();
        toobad.Play();
        yield return new WaitForSeconds(1f);
        fullcover.SetActive(true);
        loudtick.Play();
        yield return new WaitForSeconds(1f);
        smalltick.Play();
        gameoverpic.SetActive(true);
    }
}
