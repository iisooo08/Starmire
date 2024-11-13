using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class battle_manager : MonoBehaviour
{
    //basic gameobjects 
    public GameObject enemyobj; 

    //camera variables 
    public CinemachineVirtualCamera vcam;
    public Transform enemypos; 
    public Transform playerpos;

    //battle dialogue variables 
    public List<string> battledialogue;
    public List<string> textbubbledialogue;
    private List<string> temptextbox = new List<string>();
    private List<string> temptextbubble = new List<string>();
    public GameObject textboxmanager;
    public GameObject textbubblemanager;

    //battle sequence variables 
    public List<GameObject> battletechniques; //gameobjects store unique scripts for each battle pattern 
    public List<string> battlesequence; //sequence of numbers that indicate which battle techniques should be used each turn 
    public List<string> animationtriggers; //names of the animation triggers of the enemy object that will be played each time 

    //other utility variables 
    public int battleseqtracker; //technique objects will add 1 to this value once they are finished 
    private bool startbattle;

    void Start()
    {
        //also get battledialogue and battlesequence and battletechniques based on battlestatic number 
        StartCoroutine(BattleFunction());
    }

    void Update()
    {
        if(textboxmanager.GetComponent<battle_dialogue>().ended && Input.GetKeyDown(KeyCode.Return) && textboxmanager.GetComponent<battle_dialogue>().turn)
        {
            startbattle = true;
            textboxmanager.GetComponent<battle_dialogue>().generating = false;
            textbubblemanager.GetComponent<battle_dialogue>().generating = false;
        }
    }

    public IEnumerator BattleFunction()
    {
        for(int x = 0; x < battlesequence.Count; x++)
        {
            //clear all variables 
            temptextbox.Clear();
            temptextbubble.Clear();
            battleseqtracker = 0;
            //set enemy animation 
            FollowEnemy();
            if(animationtriggers[x] != "null")
            {
                enemyobj.GetComponent<Animator>().SetTrigger(animationtriggers[x]);
            }
            //set dialogues 
            yield return new WaitForSeconds(0.5f);
            temptextbox.Add(battledialogue[x]);
            temptextbubble.Add(textbubbledialogue[x]);
            StartCoroutine(textboxmanager.GetComponent<battle_dialogue>().StartDialogue(temptextbox));
            StartCoroutine(GameObject.Find("textbubblemanager").GetComponent<battle_dialogue>().StartDialogue(temptextbubble));
            textboxmanager.GetComponent<battle_dialogue>().turn = true;
            yield return new WaitUntil(() => startbattle);
            startbattle = false;
            //transition to player and stop enemy animation 
            textboxmanager.GetComponent<battle_dialogue>().turn = false;
            FollowPlayer();
            enemyobj.GetComponent<Animator>().SetTrigger("back");
            enemyobj.GetComponent<Animator>().ResetTrigger(animationtriggers[x]);
            //instantiate the battle sequence 
            for(int y = 0; y < battlesequence[x].Length; y++)
            {
                battletechniques[int.Parse(battlesequence[x][y] + "")].SetActive(true); //set the object active. the object will have code in start function and will auto destruct anyway 
            }
            yield return new WaitUntil(() => battleseqtracker == battlesequence[x].Length);
            yield return new WaitForSeconds(0.5f);
        }
        //add mechanism for when battle ends 
    }

    void FollowEnemy()
    {
        vcam.Follow = enemypos;
    }

    void FollowPlayer()
    {
        vcam.Follow = playerpos;
    }

    void ClearVariables()
    {
        //modify this later to clear all variables 
    }

}