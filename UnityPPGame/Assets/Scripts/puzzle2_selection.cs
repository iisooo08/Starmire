using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puzzle2_selection : MonoBehaviour
{
    private int index;
    public List<Transform> parentpositions;
    public List<Transform> childpositions;
    public List<GameObject> childheads;
    public List<string> dialogues;
    public TextMeshProUGUI dtext;
    public List<GameObject> parentheads;
    private Vector3 originalScale; 
    public float scaleMultiplier = 1.1f; 
    public List<string> names;
    public TextMeshProUGUI ntext;
    private bool side; 
    public List<GameObject> labels;
    private int labelint;
    public List<Transform> labelpos_parent;
    public List<Transform> labelpos_child;
    private List<int> parentlabels_track = new List<int>(){0, 0, 0, 0};
    private List<int> childlabels_track = new List<int>(){0, 0, 0, 0};
    public List<string> answers;
    public GameObject textbox;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = parentheads[0].transform.localScale;
        parentheads[0].transform.localScale = originalScale * scaleMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

        if(side == false && Input.GetKeyDown(KeyCode.DownArrow) && index < 3)
        {
            ReturnScale();
            index++;
            transform.position = parentpositions[index].position;
            ntext.text = names[index];
            dtext.text = dialogues[index];
            parentheads[index].transform.localScale = originalScale * scaleMultiplier;
        }
        else if(side == false && Input.GetKeyDown(KeyCode.UpArrow) && index > 0)
        {
            ReturnScale();
            index--;
            transform.position = parentpositions[index].position;
            ntext.text = names[index];
            dtext.text = dialogues[index];
            parentheads[index].transform.localScale = originalScale * scaleMultiplier;
        }

        if(side && Input.GetKeyDown(KeyCode.DownArrow) && index < 3)
        {
            ReturnScale();
            index++;
            transform.position = childpositions[index].position;
            ntext.text = names[index];
            dtext.text = dialogues[index];
            childheads[index].transform.localScale = originalScale * scaleMultiplier;
        }
        else if(side && Input.GetKeyDown(KeyCode.UpArrow) && index > 0)
        {
            ReturnScale();
            index--;
            transform.position = childpositions[index].position;
            ntext.text = names[index];
            dtext.text = dialogues[index];
            childheads[index].transform.localScale = originalScale * scaleMultiplier;
        }

        if(side == false && Input.GetKeyDown(KeyCode.Return) && labelint <= 3)
        {
            if(parentlabels_track[index] == 0)
            {
                textbox.SetActive(false);
                parentlabels_track[index] = labelint + 1;
                Instantiate(labels[labelint], labelpos_parent[index].position, Quaternion.identity);
                index = 0;
                side = true;
                transform.position = childpositions[0].position;
                childheads[0].transform.localScale = originalScale * scaleMultiplier;
            }
            //select parent and assign number to it 

        }
        else if(side == true && Input.GetKeyDown(KeyCode.Return))
        {
            if(childlabels_track[index] == 0)
            {
                textbox.SetActive(true);
                childlabels_track[index] = labelint + 1;
                Instantiate(labels[labelint], labelpos_child[index].position, Quaternion.identity);
                index = 0;
                labelint++;
                side = false;
                transform.position = parentpositions[0].position;
                parentheads[0].transform.localScale = originalScale * scaleMultiplier;
            }
            //select child 

        }
        if(labelint > 3)
        {
            CheckAnswers();
        }
    }
    void ReturnScale()
    {
        for(int x = 0; x < parentheads.Count; x++)
        {
            parentheads[x].transform.localScale = originalScale;
            childheads[x].transform.localScale = originalScale;
        }
    }
    void CheckAnswers()
    {
        for(int x = 0; x < answers.Count; x++)
        {
            if(parentlabels_track[int.Parse(answers[x][0] + "")] != childlabels_track[int.Parse(answers[x][1] + "")])
            {
                print("wrong");
            }
        }
        print("correct");
    }
}
