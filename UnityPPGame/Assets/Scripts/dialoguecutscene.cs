using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class dialoguecutscene : MonoBehaviour
{
    public List<string> dialogue; 
    public List<string> names; 
    public List<int> effects; 
    private bool hasTriggered; 

    public Transform cameraTarget; // Target for the camera to focus on
    public float panDuration = 1f; // Duration for the camera to pan
    private CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    public Transform player; // Reference to the player transform
    public bool next;
    public GameObject nextobj;

    private void Start()
    {
        print(PlayerPrefs.GetInt(gameObject.name + "_hasTriggered") + "getint from dcscript");
        gameObject.SetActive(PlayerPrefs.GetInt(gameObject.name + "_hasTriggered") == 0);
// if(gameObject.name == "dc1")
// {
//     gameObject.SetActive(PlayerPrefs.GetInt("dc1_hasTriggered", 0) == 0); // Inactive if true
// }
// else if(gameObject.name == "dc2")
// {
//     gameObject.SetActive(PlayerPrefs.GetInt); // Inactive if true
// }

        // hasTriggered = PlayerPrefs.GetInt(gameObject.name + "_hasTriggered", 0) == 1;

        // if (!hasTriggered)
        // {
            // hasTriggered = true; 
            // PlayerPrefs.SetInt(gameObject.name + "_hasTriggered", 1); 
            // PlayerPrefs.Save(); 

            // Get the Cinemachine virtual camera component
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            StartCoroutine(DialogueSequence());
        //}
    }

    public IEnumerator DialogueSequence()
    {
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        string prevname = ""; 
        List<string> currentdialogue = new List<string>(); 
        List<int> currenteffects = new List<int>(); 

        // Pan camera to the target
        if (cameraTarget != null)
        {
            yield return StartCoroutine(PanCamera(cameraTarget));
        }

        for (int x = 0; x < names.Count; x++)
        {
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

        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        PlayerPrefs.SetInt(gameObject.name + "_hasTriggered", 1);
        print(gameObject.name + "hasTriggered");
        print(PlayerPrefs.GetInt(gameObject.name + "hasTriggered"));
        // Pan back to the player after dialogue
        if (player != null)
        {
            yield return StartCoroutine(PanCamera(player));
        }
                    if(next)
            {
                nextobj.SetActive(true);
            }
    }

    private IEnumerator PanCamera(Transform target)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = target; // Optional: make the camera follow the target
            yield return new WaitForSeconds(panDuration); // Wait for the duration of the pan
        }
    }

}
