using UnityEngine;

public class SavePlayTime : MonoBehaviour
{
    private static SavePlayTime instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        SaveData.playtime += Time.unscaledDeltaTime;

        print(SaveData.playtime);
    }
}