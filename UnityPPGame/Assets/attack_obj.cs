using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class attack_obj : MonoBehaviour
{
    public List<GameObject> attackobj;
    public List<string> attackpos;
    public List<string> barrierpos;
    public GameObject badguy;
    public List<float> breaktime;
    public List<string> antierror_spawn;
    private bool prev;
    public bool boss;
    // Start is called before the first frame update
    public IEnumerator Attack()
    {
        GameObject.Find("antierror_manager").GetComponent<antierror_manager>().boss = boss;
        print("attack started");
        for(int x = 0; x < barrierpos.Count; x++)
        {
            int posx = int.Parse(barrierpos[x][0] + ""); 
            int posy = int.Parse(barrierpos[x][1] + "");
            GameObject.Find("badguy_manager").GetComponent<badguy_manager>().xpos.Add(posx);
            GameObject.Find("badguy_manager").GetComponent<badguy_manager>().ypos.Add(posy);
            Instantiate(badguy, new Vector2((-2.75f + posx * 1.4f), (-2.28f + posy * 1.4f)), Quaternion.identity);
        }
for (int x = 0; x < attackpos.Count; x++)
{
    string positionData = attackpos[x];
    int posx = 0, posy = 0;
    bool readX = true;

    for (int y = 0; y < positionData.Length; y++)
    {
        char currentChar = positionData[y];

        if (currentChar == '.')
        {
            // Instantiate at the calculated position
            Instantiate(attackobj[x], new Vector2((-2.82f + posx * 1.4f), (-2.84f + posy * 1.4f)), Quaternion.identity);
            readX = true; // Reset to reading x-coordinate
        }
        else if (char.IsDigit(currentChar))
        {
            int value = currentChar - '0';
            if (readX)
            {
                posx = value;
                readX = false;
            }
            else
            {
                posy = value;
            }
        }
    }
    if(antierror_spawn[x] != "0")
    {
        int px = int.Parse(antierror_spawn[x][0] + "");
        int py = int.Parse(antierror_spawn[x][1] + "");
        Instantiate(GameObject.Find("antierror_manager").GetComponent<antierror_manager>().antierror, new Vector2((-2.82f + px * 1.4f), (-2.84f + py * 1.4f)), Quaternion.identity);
    }
    yield return new WaitForSeconds(breaktime[x]);
}
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(stat_manager.prevscene);
    }
}
