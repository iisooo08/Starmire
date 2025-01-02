using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class animation_2 : MonoBehaviour
{
    public List<string> dialogue;
    public List<string> names;
    public List<int> effects;
    private bool ended;
    public GameObject queen;
    private Animator queenanim;
    public Transform cameraTarget;
    public float panDuration = 1f;
    private CinemachineVirtualCamera virtualCamera;
    private bool panned = false;
    public Transform player;
    private bool checkcollider = true;
    public AudioClip newClip;
    public AudioClip prevClip;
    private bool musiconce;

    // Use a unique key for this animation
    private const string AnimationPlayedKey1 = "a1_hasTriggered";

    void Start()
    {
        print(PlayerPrefs.GetInt(AnimationPlayedKey1,0));
        // Check if the animation has already been played
        if (PlayerPrefs.GetInt(AnimationPlayedKey1, 0) == 1)
        {
            checkcollider = false;
            return; // Exit if the animation has already played
        }
        else
        {
            checkcollider = true;
        }

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    IEnumerator AnimationSeq()
    {
        queenanim = queen.GetComponent<Animator>();

    FadeOut();
        yield return new WaitForSeconds(0.5f);
        
        if (cameraTarget != null)
        {
            yield return StartCoroutine(PanCamera(cameraTarget));
            yield return new WaitUntil(() => panned);
        }

        StartCoroutine(DialogueSequence());
        yield return new WaitUntil(() => ended);

        queenanim.SetTrigger("rage");
        yield return new WaitForSeconds(1f);
        queenanim.SetTrigger("back");
        yield return StartCoroutine(PanCamera(player));
        checkcollider = false;

        // Mark the animation as played
        PlayerPrefs.SetInt(AnimationPlayedKey1, 1);

        gameObject.SetActive(false);
    }

    private IEnumerator DialogueSequence()
    {
        string prevname = "";
        List<string> currentdialogue = new List<string>();
        List<int> currenteffects = new List<int>();

        for (int x = 0; x < names.Count; x++)
        {
            if (x == 1)
            {
                queenanim.SetTrigger("frontmove");
                for (int i = 0; i < 3; i++)
                {
                    Vector3 position = queen.transform.position;
                    position.y -= 0.3f;
                    queen.transform.position = position;
                    yield return new WaitForSeconds(0.3f);
                }
                queenanim.SetTrigger("back");
            }
            if (x == 3)
            {
                queenanim.SetTrigger("rage");
                yield return new WaitForSeconds(0.4f);
                queenanim.SetTrigger("back");
            }

            if (x == 0)
            {
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else if (prevname == names[x])
            {
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else
            {
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
                currenteffects.Clear();
                currentdialogue.Clear();
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }

            if (x == names.Count - 1)
            {
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
            }
        }
        GameObject.Find("playersprite_final").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        musicplayer musicPlayer = FindObjectOfType<musicplayer>();
if (musicPlayer != null)
{
    musicPlayer.ChangeMusic(prevClip);
}

        yield return new WaitForSeconds(0.5f);
        ended = true;
    }

    private IEnumerator PanCamera(Transform target)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = target;
            yield return new WaitForSeconds(panDuration);
            panned = true;
        }
        if (!checkcollider) yield break;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") && checkcollider == true)
        {
            StartCoroutine(AnimationSeq());
        }
    }
        void FadeOut()
    {

        musicplayer musicPlayer = FindObjectOfType<musicplayer>();

        if(musiconce == false)
        {
musiconce = true;
musicPlayer.ChangeMusic(newClip);
        StartCoroutine(musicPlayer.FadeIn(1.0f));
        }
    }
}
