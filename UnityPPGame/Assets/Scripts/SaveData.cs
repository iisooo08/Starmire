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


public class ItemStorage
{
   public Dictionary<string, bool> flags = new Dictionary<string, bool>()
   {
       { "item_1", false },
       { "item_2", false },
       { "item_3", false },
       { "item_4", false },
       { "item_5", false },
       { "item_6", false },
       { "item_7", false },
       { "item_8", false }
   };
}


public static class SaveData
{
   public static ItemStorage itemStorage = new ItemStorage();
   public static string[] availableWeapons =
   {
       "weapon_1",
       "weapon_2",
       "weapon_3",
       "weapon_4"
   };
   public static string[] availableArmor =
   {
       "armor_1",
       "armor_2",
       "armor_3",
       "armor_4"
   };
   public static string currentWeapon = "BaseWand";
   public static string currentArmor = "Robes";


   public static int playerLevel = 1;
   public static int currentScene = 0;
   public static string displayedLocation = "HisRoom";
   public static Vector3 currentPosition = Vector3.zero;
   public static bool hasSaveFile = false;
   public static bool hasWatchedTitle = false;
   public static string playerName = "DefaultName";

   public static bool hasLoadedThisSession = false;


   public static float playtime = 0f;
   public static CutsceneStorage cutsceneStorage = new CutsceneStorage();
   public static StartDialogueStorage startDialogueStorage = new StartDialogueStorage();
   public static TaskStorage taskStorage = new TaskStorage();
   public static AnimationStorage animationStorage = new AnimationStorage();


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


   public static void SaveAnimations()
   {
       string json = JsonUtility.ToJson(new DictionaryWrapper(animationStorage.flags));
       PlayerPrefs.SetString("animationStorage", json);
   }
  
   public static void LoadAnimations()
{
   if (PlayerPrefs.HasKey("animationStorage"))
   {
       string json = PlayerPrefs.GetString("animationStorage");
       animationStorage.flags = new DictionaryWrapper(json).ToDictionary();
   }
   else
   {
       animationStorage = new AnimationStorage();
   }
}
public static void SaveItems()
{
   string json = JsonUtility.ToJson(new DictionaryWrapper(itemStorage.flags));
   PlayerPrefs.SetString("itemStorage", json);
}


public static void LoadItems()
{
   itemStorage = new ItemStorage();


   if (PlayerPrefs.HasKey("itemStorage"))
   {
       string json = PlayerPrefs.GetString("itemStorage");
       Dictionary<string, bool> loadedItems = new DictionaryWrapper(json).ToDictionary();


       foreach (var item in loadedItems)
       {
           if (itemStorage.flags.ContainsKey(item.Key))
           {
               itemStorage.flags[item.Key] = item.Value;
           }
       }
   }
}


public static void GetItem(string itemName)
{
   if (itemStorage.flags.ContainsKey(itemName))
   {
       itemStorage.flags[itemName] = true;
       SaveAll();
   }
   else
   {
       Debug.LogWarning("Item does not exist: " + itemName);
   }
}


public static bool HasItem(string itemName)
{
   return itemStorage.flags.ContainsKey(itemName) && itemStorage.flags[itemName];
}


public static void SaveEquipment()
{
   PlayerPrefs.SetString("currentWeapon", currentWeapon);
   PlayerPrefs.SetString("currentArmor", currentArmor);
}


public static void LoadEquipment()
{
   currentWeapon = PlayerPrefs.GetString("currentWeapon", "None");
   currentArmor = PlayerPrefs.GetString("currentArmor", "None");


   if (!IsValidEquipment(currentWeapon, availableWeapons))
   {
       currentWeapon = "None";
   }


   if (!IsValidEquipment(currentArmor, availableArmor))
   {
       currentArmor = "None";
   }
}


private static bool IsValidEquipment(string equipmentName, string[] equipmentList)
{
   if (equipmentName == "None")
   {
       return true;
   }


   foreach (string equipment in equipmentList)
   {
       if (equipment == equipmentName)
       {
           return true;
       }
   }


   return false;
}


public static void SetWeapon(string weaponName)
{
   if (IsValidEquipment(weaponName, availableWeapons))
   {
       currentWeapon = weaponName;
       SaveAll();
   }
   else
   {
       Debug.LogWarning("Weapon does not exist: " + weaponName);
   }
}


public static void SetArmor(string armorName)
{
   if (IsValidEquipment(armorName, availableArmor))
   {
       currentArmor = armorName;
       SaveAll();
   }
   else
   {
       Debug.LogWarning("Armor does not exist: " + armorName);
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
       PlayerPrefs.SetString("displayedLocation", displayedLocation);
       PlayerPrefs.SetFloat("playtime", playtime);


       SaveTasks();
       SaveCutscenes();
       SaveStartDialogues();
       SaveAnimations();
       SaveItems();
       SaveEquipment();


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


       displayedLocation = PlayerPrefs.GetString("displayedLocation", "HisRoom");
       hasSaveFile = PlayerPrefs.GetInt("hasSaveFile", 0) == 1;
       hasWatchedTitle = PlayerPrefs.GetInt("hasWatchedTitle", 0) == 1;
       playerName = PlayerPrefs.GetString("playerName", "DefaultName");
       playtime = PlayerPrefs.GetFloat("playtime",0f);
      


       LoadTasks();
       LoadCutscenes();
       LoadStartDialogues();
       LoadAnimations();
       LoadItems();
       LoadEquipment();
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
   displayedLocation = "HisRoom";
   playtime = 0f;


   cutsceneStorage = new CutsceneStorage();
   startDialogueStorage = new StartDialogueStorage();
   taskStorage = new TaskStorage();
   animationStorage = new AnimationStorage();


   itemStorage = new ItemStorage();


   currentWeapon = "BaseWand";
   currentArmor = "Robes";
}
}

