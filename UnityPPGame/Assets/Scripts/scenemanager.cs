using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public Animator image;
    public GameObject player;
    public static int prevscene;
    public List<Transform> positions = new List<Transform>();
    public List<int> scenes = new List<int>();
    public int next;
    void Start()
    {
        image.SetTrigger("start");
        for(int x = 0; x < scenes.Count; x++)
        {
            if(prevscene == scenes[x])
            {
                player.transform.position = positions[x].position;
            }
        }
        prevscene = SceneManager.GetActiveScene().buildIndex;
    }
    public IEnumerator NextScene()
    {
        image.SetTrigger("end");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(next);
    }
}
