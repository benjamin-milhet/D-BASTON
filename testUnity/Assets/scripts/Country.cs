using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Country {
    /// <summary>
    /// Enumeration des equipes possibles
    /// </summary>
    public enum theTribes
    {
        ALLEMAGNE = 0,
        FRANCE = 1,
        ANGLETERRE = 2,
        ESPAGNE = 3
    }


    public string name; //Nom du territoire
    public theTribes tribe; //Joueur qui possede ce territoire
    public int moneyReward;
    public int expReward;
    public int nbTroupe;//Nombre de troupe sur ce territoire

}


