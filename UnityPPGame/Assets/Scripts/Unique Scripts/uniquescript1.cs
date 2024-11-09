using UnityEngine;

public class uniquescript1 : MonoBehaviour
{
    public static bool searchedforfriend;
    public GameObject gameobj;
    private string objectKey = "pk_soldiers";

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(objectKey, 0) == 1)
        {
            gameobj.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            searchedforfriend = true;

            PlayerPrefs.SetInt(objectKey, 1);

            gameobj.SetActive(false);
        }
    }
}
