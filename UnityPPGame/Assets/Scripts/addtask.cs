using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addtask : MonoBehaviour
{
    public string taskstring;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("eventbook").GetComponent<eventbook_script>().AddTask(taskstring);
    }
}
