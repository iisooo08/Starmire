using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battle_data : MonoBehaviour
{
    public List<string> fullatk = new List<string>();
    public List<int> change = new List<int>();
    public float sp;
    public List<string> extension1 = new List<string>();
    public bool extension1b;
    //1: some elements turn into a bug 
    //2: there are segments when there are no breaks between lines 
    //3: the elements change shape so you can't see them anymore, you have to memorize the sequence beforehand 
}
