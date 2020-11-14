using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class FightSim
{
    private int valueDe;
    private int valueDeAttacked;

    public FightSim(int valueDe, int valueDeAttacked)
    {
        this.valueDe = valueDe;
        this.valueDeAttacked = valueDeAttacked;
    }


    public void Fight()
   {
       
        List<int> resultat = new List<int>();
        int res = 0;
        int resAttacked = 0;
        List<int> resultatAttacked = new List<int>();
       
        
        for (int i = 0; i < this.valueDe; i++)
        {
            int de = Random.Range(1, 6);
            resultat.Add(de);
        }
        
        for (int i = 0; i < this.valueDeAttacked; i++)
        {
            int de = Random.Range(1, 6);
            resultatAttacked.Add(de);
        }
        
        resultat.Sort();
        resultatAttacked.Sort();

        
        if (resultat[this.valueDe-1] >= resultatAttacked[this.valueDeAttacked-1])
        {
            resAttacked++;
            
        }
        else
        {
            res++;
        }
        if (this.valueDeAttacked == 2 && this.valueDe == 3)
        {
            if (resultat[1] >= resultatAttacked[0])
            {
                resAttacked++;
            
            }
            else
            {
                res++;
            }
        }
        else if (this.valueDeAttacked == 2 && this.valueDe == 2)
        {
            if (resultat[0] >= resultatAttacked[0])
            {
                resAttacked++;
            
            }
            else
            {
                res++;
            }
        }
       

        bool fin = CountryManager.instance.ResAttaque(res,resAttacked);

        if (fin)
        {
            GameManager.instance.battleWon = true;
        }
        else
        {
            GameManager.instance.battleWon = false;

        }
        GameManager.instance.battleHasEnded = true;
        CountryManager.instance.finFight();
        //SceneManager.LoadScene(0);
    } 
}
