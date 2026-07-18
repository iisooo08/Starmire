using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartingAnimationType
{
    Dialogue,
    MoveObject,
    ChangeMusic,
    PlaySFX,
    Wait
}

[System.Serializable]
public class StartingAnimationStep
{
    public StartingAnimationType type;

    public List<string> dialogues;
    public List<string> names;
    public List<int> effects;

    public Transform objectToMove;
    public Transform objectTarget;
    public float objectMoveDuration = 1f;

    public AudioClip musicClip;
    public AudioClip sfxClip;

    public float waitTime = 0.5f;
}

public class StartingAnimation : MonoBehaviour
{
    public string currentstr;
    public bool task = false;
    public string taskname;

    public List<StartingAnimationStep> steps = new List<StartingAnimationStep>();

    public dialoguemanager dm;
    public musicplayer mp;
    public AudioSource sfxSource;

    public MonoBehaviour playerMovementScript;

    IEnumerator Start()
    {
        mp = GameObject.Find("musicplayer").GetComponent<musicplayer>();

        if (dm == null)
            dm = GameObject.Find("dialoguemanager").GetComponent<dialoguemanager>();

        if (SaveData.startDialogueStorage.flags.TryGetValue(currentstr, out bool isDone) && isDone)
            yield break;

        SaveData.startDialogueStorage.flags[currentstr] = true;

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

            yield return new WaitForSeconds(1f);

        for (int x = 0; x < steps.Count; x++)
        {
            StartingAnimationStep step = steps[x];

            if (step.type == StartingAnimationType.Dialogue)
            {
                if (step.dialogues != null && step.dialogues.Count > 0)
                {
                    yield return StartCoroutine(dm.StartDialogue(step.dialogues, step.names, step.effects));
                    yield return new WaitUntil(() => dm.finish == true);
                }
            }
            else if (step.type == StartingAnimationType.MoveObject)
            {
                if (step.objectToMove != null && step.objectTarget != null)
                {
                    yield return StartCoroutine(MoveTransform(step.objectToMove, step.objectTarget.position, step.objectMoveDuration));
                }
            }
            else if (step.type == StartingAnimationType.ChangeMusic)
            {
                if (mp != null && step.musicClip != null)
                {
                    mp.ChangeMusic(step.musicClip);
                }
            }
            else if (step.type == StartingAnimationType.PlaySFX)
            {
                if (sfxSource != null && step.sfxClip != null)
                {
                    sfxSource.PlayOneShot(step.sfxClip);
                }
            }
            else if (step.type == StartingAnimationType.Wait)
            {
                yield return new WaitForSeconds(step.waitTime);
            }
        }

        if (task)
        {
            GameObject.Find("taskmanager").GetComponent<taskmanager>().AddTask(taskname);
        }

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }

    IEnumerator MoveTransform(Transform thing, Vector3 targetPos, float duration)
    {
        Vector3 startPos = thing.position;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            thing.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        thing.position = targetPos;
    }
}