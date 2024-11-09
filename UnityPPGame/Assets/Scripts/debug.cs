using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Resets a specific PlayerPrefs key
    public void ResetDialogueTrigger(string dialogueTriggerKey)
    {
        PlayerPrefs.DeleteKey(dialogueTriggerKey);
        PlayerPrefs.Save();
        Debug.Log($"Reset PlayerPrefs key: {dialogueTriggerKey}");
    }

    // Resets all PlayerPrefs
    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs reset.");
    }

    private void Update()
    {
        // Reset specific dialogue trigger when pressing "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetDialogueTrigger("dialoguecutscene_hasTriggered"); // Replace with your actual key
        }

        // Reset all PlayerPrefs when pressing "A"
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetAllPlayerPrefs();
        }
    }
}
