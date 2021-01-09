using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Country {
    
    public static List<string> theTribes = new List<string>();
    
    public string name; //Nom du territoire
    public string tribe; //Joueur qui possede ce territoire
    public int moneyReward;
    public int expReward;
    public int nbTroupe;//Nombre de troupe sur ce territoire
    public string camp;



}


