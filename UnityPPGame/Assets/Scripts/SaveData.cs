using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DictionaryWrapper
{
    public List<string> keys = new List<string>();
    public List<bool> values = new List<bool>();

    public DictionaryWrapper(Dictionary<string, bool> dict)
    {
        foreach (var kvp in dict)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public DictionaryWrapper(string json)
    {
        var wrapper = JsonUtility.FromJson<DictionaryWrapper>(json);

        if (wrapper != null)
        {
            keys = wrapper.keys ?? new List<string>();
            values = wrapper.values ?? new List<bool>();
        }
        else
        {
            keys = new List<string>();
            values = new List<bool>();
        }
    }

    public Dictionary<string, bool> ToDictionary()
    {
        Dictionary<string, bool> result = new Dictionary<string, bool>();

        int count = Mathf.Min(keys.Count, values.Count);

        for (int i = 0; i < count; i++)
        {
            result[keys[i]] = values[i];
        }

        return result;
    }
}

public static class SaveData
{
    public static int playerLevel = 1;
    public static int currentScene = 0;
    public static Vector3 currentPosition = Vector3.zero;
    public static bool hasSaveFile = false;
    public static bool hasWatchedTitle = false;
    public static string playerName = "DefaultName";

    public static CutsceneStorage cutsceneStorage = new CutsceneStorage();
    public static StartDialogueStorage startDialogueStorage = new StartDialogueStorage();
    public static TaskStorage taskStorage = new TaskStorage();

    public static void SaveTasks()
    {
        string json = JsonUtility.ToJson(new DictionaryWrapper(taskStorage.flags));
        PlayerPrefs.SetString("taskStorage", json);
    }

    public static void LoadTasks()
    {
        if (PlayerPrefs.HasKey("taskStorage"))
        {
            string json = PlayerPrefs.GetString("taskStorage");
            taskStorage.flags = new DictionaryWrapper(json).ToDictionary();
        }
        else
        {
            taskStorage = new TaskStorage();
        }
    }

    public static void SaveCutscenes()
    {
        string json = JsonUtility.ToJson(new DictionaryWrapper(cutsceneStorage.flags));
        PlayerPrefs.SetString("cutsceneStorage", json);
    }

    public static void LoadCutscenes()
    {
        if (PlayerPrefs.HasKey("cutsceneStorage"))
        {
            string json = PlayerPrefs.GetString("cutsceneStorage");
            cutsceneStorage.flags = new DictionaryWrapper(json).ToDictionary();
        }
        else
        {
            cutsceneStorage = new CutsceneStorage();
        }
    }

    public static void SaveStartDialogues()
    {
        string json = JsonUtility.ToJson(new DictionaryWrapper(startDialogueStorage.flags));
        PlayerPrefs.SetString("startDialogueStorage", json);
    }

public static void LoadStartDialogues()
{
    if (PlayerPrefs.HasKey("startDialogueStorage"))
    {
        string json = PlayerPrefs.GetString("startDialogueStorage");
        startDialogueStorage.flags = new DictionaryWrapper(json).ToDictionary();
    }
    else
    {
        startDialogueStorage = new StartDialogueStorage();
    }
}

    public static void SaveAll()
    {
        PlayerPrefs.SetInt("playerLevel", playerLevel);
        PlayerPrefs.SetInt("currentScene", currentScene);
        PlayerPrefs.SetFloat("posX", currentPosition.x);
        PlayerPrefs.SetFloat("posY", currentPosition.y);
        PlayerPrefs.SetFloat("posZ", currentPosition.z);
        PlayerPrefs.SetInt("hasSaveFile", hasSaveFile ? 1 : 0);
        PlayerPrefs.SetInt("hasWatchedTitle", hasWatchedTitle ? 1 : 0);
        PlayerPrefs.SetString("playerName", playerName);

        SaveTasks();
        SaveCutscenes();
        SaveStartDialogues();

        PlayerPrefs.Save();
    }

    public static void LoadAll()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
        currentScene = PlayerPrefs.GetInt("currentScene", 0);

        float x = PlayerPrefs.GetFloat("posX", 0f);
        float y = PlayerPrefs.GetFloat("posY", 0f);
        float z = PlayerPrefs.GetFloat("posZ", 0f);
        currentPosition = new Vector3(x, y, z);

        hasSaveFile = PlayerPrefs.GetInt("hasSaveFile", 0) == 1;
        hasWatchedTitle = PlayerPrefs.GetInt("hasWatchedTitle", 0) == 1;
        playerName = PlayerPrefs.GetString("playerName", "DefaultName");

        LoadTasks();
        LoadCutscenes();
        LoadStartDialogues();
    }

public static void ResetAll()
{
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();

    playerLevel = 1;
    currentScene = 0;
    currentPosition = Vector3.zero;
    hasSaveFile = false;
    hasWatchedTitle = false;
    playerName = "DefaultName";

    cutsceneStorage = new CutsceneStorage();
    startDialogueStorage = new StartDialogueStorage();
    taskStorage = new TaskStorage();
}
}