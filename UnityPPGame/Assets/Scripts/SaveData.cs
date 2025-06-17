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
        keys = wrapper.keys;
        values = wrapper.values;
    }

    public Dictionary<string, bool> ToDictionary()
    {
        Dictionary<string, bool> result = new Dictionary<string, bool>();
        for (int i = 0; i < keys.Count; i++)
        {
            result[keys[i]] = values[i];
        }
        return result;
    }
}

public static class SaveData
{
    public static CutsceneStorage cutsceneStorage = new CutsceneStorage();
    public static StartDialogueStorage startDialogueStorage = new StartDialogueStorage();

    public static void SaveCutscenes()
    {
        string json = JsonUtility.ToJson(new DictionaryWrapper(cutsceneStorage.flags));
        PlayerPrefs.SetString("cutsceneStorage", json);
        PlayerPrefs.Save(); 
    }

    public static void LoadCutscenes()
    {
        if (PlayerPrefs.HasKey("cutsceneStorage"))
        {
            string json = PlayerPrefs.GetString("cutsceneStorage");
            cutsceneStorage.flags = new DictionaryWrapper(json).ToDictionary();
        }
    }

    public static void SaveStartDialogues()
    {
        string json = JsonUtility.ToJson(new DictionaryWrapper(startDialogueStorage.flags));
        PlayerPrefs.SetString("startDialogueStorage", json);
        PlayerPrefs.Save(); 
    }

    public static void LoadStartDialogues()
    {
        if (PlayerPrefs.HasKey("startDialogueStorage"))
        {
            string json = PlayerPrefs.GetString("startDialogueStorage");
            startDialogueStorage.flags = new DictionaryWrapper(json).ToDictionary();
        }
    }

    public static bool hasSaveFile
    {
        get => PlayerPrefs.GetInt("hasSaveFile", 0) == 1;
        set => PlayerPrefs.SetInt("hasSaveFile", value ? 1 : 0);
    }

    public static bool hasWatchedTitle
    {
        get => PlayerPrefs.GetInt("hasWatchedTitle", 0) == 1;
        set => PlayerPrefs.SetInt("hasWatchedTitle", value ? 1 : 0);
    }

    public static string playerName
    {
        get => PlayerPrefs.GetString("playerName", "DefaultName");
        set => PlayerPrefs.SetString("playerName", value);
    }

    public static int playerLevel
    {
        get => PlayerPrefs.GetInt("playerLevel", 1);
        set => PlayerPrefs.SetInt("playerLevel", value);
    }

    public static int currentScene
    {
        get => PlayerPrefs.GetInt("currentScene", 0);
        set => PlayerPrefs.SetInt("currentScene", value);
    }

    public static Vector3 currentPosition
    {
        get
        {
            float x = PlayerPrefs.GetFloat("posX", 0f);
            float y = PlayerPrefs.GetFloat("posY", 0f);
            float z = PlayerPrefs.GetFloat("posZ", 0f);
            return new Vector3(x, y, z);
        }
        set
        {
            PlayerPrefs.SetFloat("posX", value.x);
            PlayerPrefs.SetFloat("posY", value.y);
            PlayerPrefs.SetFloat("posZ", value.z);
        }
    }

    public static void SaveAll()
    {
        PlayerPrefs.Save();
    }

    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
