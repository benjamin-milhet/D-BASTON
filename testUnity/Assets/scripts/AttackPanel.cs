using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AttackPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText; //Texte de description pour la fenetre de combat adapté en fonction du territoire
    public TextMeshProUGUI valueSlider; //Valeur du slider qui correspond au nombre de dé
    public Slider nbDe; //GameObject de type slider permettant de choisir un nombre de dé pour le combat
    
    /// <summary>
    /// Permet d'afficher le valeur du slider
    /// </summary>
    public void SetValueTextSlider()
    {
        valueSlider.text = nbDe.value.ToString();
    }
    
    

}
