using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationManager : MonoBehaviour
{
    public static int animationNumber;
    public static int prevscene;
    public List<GameObject> animationObjects;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = animationObjects[animationNumber];
        StartCoroutine(obj.GetComponent<animationObjectScript>().AnimationSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
