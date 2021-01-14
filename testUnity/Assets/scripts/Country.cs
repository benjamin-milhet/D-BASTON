using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel qui gere les territoires
/// </summary>
[System.Serializable]
public class Country {
    
    public static List<string> theTribes = new List<string>();//List des equipes
    
    public string name; //Nom du territoire
    public string tribe; //Joueur qui possede ce territoire
    public int nbTroupe;//Nombre de troupe sur ce territoire
    public string camp;//Pour la carte star wars conquete, pour attribuer une equipe de base a ce territoire



}


