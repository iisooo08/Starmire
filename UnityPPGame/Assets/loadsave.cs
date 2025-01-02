using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadsave : MonoBehaviour
{
    public static bool dc1_T;
    public static bool dc2_T;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        transform.position = new Vector2(x, y);

        

        // Load other game data
        
    }
    }

    // Update is called once per frame
    void Update()
    {
        //print(PlayerPrefs.GetInt("dc1_hasTriggered"));
    }

    void Awake()
    {
        dc1_T = PlayerPrefs.GetInt("dc1_hasTriggered", 0) == 1;
        dc2_T = PlayerPrefs.GetInt("dc2_hasTriggered", 0) == 1;
        bool a1_hasTriggered = PlayerPrefs.GetInt("a1_hasTriggered", 0) == 0;
        
        //setactive 
        
    }
}
