using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puzzleinstructions : MonoBehaviour
{
    public List<GameObject> images;
    public Transform imageposition;
    private bool returned;
    public static int puzzle1scene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowInstructions());
    }
    IEnumerator ShowInstructions()
    {
        for(int x = 0; x < images.Count; x++)
        {
            GameObject prevobj = Instantiate(images[x], imageposition.position, Quaternion.identity);
            yield return new WaitUntil(() => returned);
            Destroy(prevobj);
            returned = false;
        }
        SceneManager.LoadScene(puzzle1scene);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            returned = true;
        }
    }
}
