using System.Collections;
using UnityEngine;
using TMPro;

public class puzzle_countdown : MonoBehaviour
{
    public float startTime = 300f; // Set starting time in seconds (e.g., 300 for 5 minutes)
    private float currentTime;
    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = startTime;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }

        currentTime = 0; // Ensures timer shows 0 when finished
        UpdateTimerDisplay();
        TimerFinished();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerFinished()
    {
        // Action to take when the timer ends
        Debug.Log("Time's up!");
    }
}

