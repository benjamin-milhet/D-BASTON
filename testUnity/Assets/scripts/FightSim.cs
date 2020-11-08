using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class FightSim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fight());
    }

   IEnumerator Fight()
    {
        yield return new WaitForSeconds(2);
        int num = Random.Range(0, 2);

        if (num ==0)
        {
            GameManager.instance.battleWon = false;
        }
        else
        {
            GameManager.instance.battleWon = true;

        }
        GameManager.instance.battleHasEnded = true;
        SceneManager.LoadScene(0);
    } 
}
