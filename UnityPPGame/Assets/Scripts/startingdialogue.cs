using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingdialogue : MonoBehaviour
{
    public List<string> dialogue; // List of dialogue lines
    public List<string> names; // List of character names corresponding to each dialogue
    public List<int> effects; // Effects for each dialogue line
    private bool hasTriggered; // Flag to check if the dialogue has been triggered

    private void Start()
    {
        // Check if the dialogue has already been triggered using PlayerPrefs
        hasTriggered = PlayerPrefs.GetInt(gameObject.name + "_hasTriggered", 0) == 1;

        // Start the dialogue sequence if it hasn't been triggered yet
        if (!hasTriggered)
        {
            hasTriggered = true; // Set the flag to true
            PlayerPrefs.SetInt(gameObject.name + "_hasTriggered", 1); // Mark as triggered in PlayerPrefs
            PlayerPrefs.Save(); // Save PlayerPrefs

            // Start the dialogue sequence
            StartCoroutine(DialogueSequence());
        }
    }

    private IEnumerator DialogueSequence()
    {
        print("Dialogue sequence entered");
        string prevname = ""; // To track the previous speaker's name
        List<string> currentdialogue = new List<string>(); // Temporary storage for current dialogue
        List<int> currenteffects = new List<int>(); // Temporary storage for current effects

        for (int x = 0; x < names.Count; x++)
        {
            // Initialize the first dialogue entry
            if (x == 0)
            {
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else if (prevname == names[x]) // Same speaker as before
            {
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }
            else // Speaker has changed
            {
                // Trigger dialogue for the previous speaker
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));

                // Clear temporary lists and prepare for the new speaker
                currenteffects.Clear();
                currentdialogue.Clear();
                prevname = names[x];
                currenteffects.Add(effects[x]);
                currentdialogue.Add(dialogue[x]);
            }

            // If it's the last dialogue entry, trigger it
            if (x == names.Count - 1)
            {
                yield return StartCoroutine(FindObjectOfType<dialoguemanager>().StartDialogue(currentdialogue, prevname, currenteffects));
            }
        }
    }

}
