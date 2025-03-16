using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectionblock_move : MonoBehaviour
{
    public int currentscene;
    private int hslot;
    private int vslot;
    private int hslot2;
    private int vslot2;
    public List<Transform> hpos;
    public List<Transform> vpos;
    public List<Transform> hinv;
    public List<Transform> vinv;
    private List<int[]> grid;
    private List<GameObject[]> inventory;
    public Sprite grabsprite;
    public Sprite selectsprite;
    public bool whichpart; //false : inventory, true : grid 
    // Start is called before the first frame update
    public List<GameObject> blocks;
    public GameObject currenthold;
    private List<int[]> answers;
    public AudioSource pickup;
    public AudioSource down;
    public AudioSource move;
    public AudioSource move2;
    public AudioSource wrong1;
    public AudioSource correct1;
    public GameObject correctsprite;
    private bool wrong3;
    void Awake()
    {
        inventory = new List<GameObject[]>
        {
            new GameObject[] {blocks[0], blocks[4], blocks[8]},
            new GameObject[] {blocks[1], blocks[5], blocks[9]},
            new GameObject[] {blocks[2], blocks[6], null},
            new GameObject[] {blocks[3], blocks[7], null}
        };
        grid = new List<int[]>
        {
            new int[] {0, 0, 0},
            new int[] {0, 0, 0},
            new int[] {0, 0, 0},
            new int[] {0, 0, 0}
        };
        answers = new List<int[]>
        {new int[] {1, 6, 4},
            new int[] {2, 4, 5},
            new int[] {1, 3, 5},
            new int[] {2, 0, 0}
        };
    }
    void Start()
    {
        whichpart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //finish/clear mechanics 
        if(Input.GetKeyDown(KeyCode.C))
        {
            //return all of them to their original positions...? reload scene??? 
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            for(int x = 0; x < 4; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    if(answers[x][y] != grid[x][y])
                    {
                        wrong3 = true;
                        wrong();
                    }
                }
            }
            if(wrong3 != true)
            {
                StartCoroutine(correct());
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            reset_scenemanager.puzzlescene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(23);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(scenemanager.prevscene);
        }
        //grid selection
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(whichpart == false && currenthold != null)
            {
                move2.Play();
                whichpart = true;
                hslot = 0;
                vslot = 0;
                transform.position = new Vector2(hpos[0].position.x, hpos[0].position.y);
                currenthold.transform.position = transform.position;
            }
            if(whichpart == true && currenthold == null)
            {
                move2.Play();
                whichpart = false;
                hslot2 = 0;
                vslot2 = 0;
                transform.position = new Vector2(hinv[0].position.x, hinv[0].position.y);
            }
        }

        //grid grid 
        if(whichpart && Input.GetKeyDown(KeyCode.RightArrow) && hslot < 3)
        {
            move.Play();
            hslot++;
            transform.position = new Vector2(hpos[hslot].position.x, transform.position.y);
            currenthold.transform.position = transform.position;
        }
        else if(whichpart && Input.GetKeyDown(KeyCode.LeftArrow) && hslot > 0)
        {
            move.Play();
            hslot--;
            transform.position = new Vector2(hpos[hslot].position.x, transform.position.y);
            currenthold.transform.position = transform.position;
        }
        else if(whichpart && Input.GetKeyDown(KeyCode.UpArrow) && vslot > 0)
        {
            move.Play();
            vslot--;
            transform.position = new Vector2(transform.position.x, vpos[vslot].position.y);
            currenthold.transform.position = transform.position;
        }
        else if(whichpart && Input.GetKeyDown(KeyCode.DownArrow) && vslot < 2)
        {
            move.Play();
            vslot++;
            transform.position = new Vector2(transform.position.x, vpos[vslot].position.y);
            currenthold.transform.position = transform.position;
        }

        //inventory grid 
        if(currenthold == null && !whichpart && Input.GetKeyDown(KeyCode.RightArrow) && hslot2 < 3)
        {
            move.Play();
            hslot2++;
            transform.position = new Vector2(hinv[hslot2].position.x, transform.position.y);
        }
        else if(currenthold == null && !whichpart && Input.GetKeyDown(KeyCode.LeftArrow) && hslot2 > 0)
        {
            move.Play();
            hslot2--;
            transform.position = new Vector2(hinv[hslot2].position.x, transform.position.y);
        }
        else if(currenthold == null && !whichpart && Input.GetKeyDown(KeyCode.UpArrow) && vslot2 > 0)
        {
            move.Play();
            vslot2--;
            transform.position = new Vector2(transform.position.x, vinv[vslot2].position.y);
        }
        else if(currenthold == null && !whichpart && Input.GetKeyDown(KeyCode.DownArrow) && vslot2 < 2)
        {
            move.Play();
            vslot2++;
            transform.position = new Vector2(transform.position.x, vinv[vslot2].position.y);
        }

        if(whichpart && Input.GetKeyDown(KeyCode.Return))
        {
            if(grid[hslot][vslot] != 0)
            {
                //do somethign to prvent this 
            }
            else
            {
                print(currenthold.name);
                if(currenthold.name == "s1")
                {
                    grid[hslot][vslot] = 1;
                }
                else if(currenthold.name == "s2")
                {
                    grid[hslot][vslot] = 2;
                }
                else if(currenthold.name == "s3")
                {
                    grid[hslot][vslot] = 3;
                }
                else if(currenthold.name == "s4")
                {
                    grid[hslot][vslot] = 4;
                }
                else if(currenthold.name == "h")
                {
                    grid[hslot][vslot] = 5;
                }
                else if(currenthold.name == "v")
                {
                    grid[hslot][vslot] = 6;
                }
                currenthold = null;
                gameObject.GetComponent<SpriteRenderer>().sprite = selectsprite;
                down.Play();
            }
        }
        else if(whichpart != true && Input.GetKeyDown(KeyCode.Return))
        {
            if(inventory[hslot2][vslot2] == null)
            {
                //do somethign to prevent this 
            }
            else
            {
                pickup.Play();
                currenthold = inventory[hslot2][vslot2];
                inventory[hslot2][vslot2] = null;
                gameObject.GetComponent<SpriteRenderer>().sprite = grabsprite;
            }
        }
    }

    void wrong()
    {
        wrong1.Play();
    }
    IEnumerator correct()
    {
        print("yes");
        //puzzleonce_a2.a2_solved = true;
        PlayerPrefs.SetInt("p1a2_solved", 1);
        correct1.Play();
        correctsprite.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scenemanager.prevscene);
    }
}