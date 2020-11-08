using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Country {
    public string name;
    public enum theTribes
    {
        CLONE = 0,
        DROIDE = 1,
        JEDI = 2,
        SITH = 3,
        MENU
    }



    public theTribes tribe;

    public int moneyReward;
    public int expReward;
    public int nbTroupe;


   



    //public Text nbTroupeUI;

    /*public void start()
    {
        nbTroupeUI.text = nbTroupe.ToString();
    }*/

}


