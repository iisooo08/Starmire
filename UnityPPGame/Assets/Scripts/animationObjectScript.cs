using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class animationObjectScript : MonoBehaviour
{
    public List<Sprite> images;
    public List<int> wait;
    public GameObject imagedisplay;

    public IEnumerator AnimationSequence()
    {
        for(int x = 0; x < images.Count; x++)
        {
            imagedisplay.GetComponent<SpriteRenderer>().sprite = images[x];
            yield return new WaitForSeconds(wait[x]);
        }
        SceneManager.LoadScene(GeneralAnimationManager.prevscene);
    }
}
