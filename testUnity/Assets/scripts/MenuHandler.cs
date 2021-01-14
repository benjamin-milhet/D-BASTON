using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Chaque bouton ayant le tag 'Menu' sont récupéré
 * Permet de savoir de quelle menu il s'agit
 */
[RequireComponent(typeof(PolygonCollider2D))]
public class MenuHandler : MonoBehaviour
{
    public string name;
    
    
    
    /// <summary>
    /// Permet de faire une action suivant le bouton menu choisit
    /// </summary>
    void OnMouseUpAsButton()
    {
        if (name == "menu")
        {
            MenuManager.instance.ShowMenu();
        }
        else if (name == "passer")
        {
            MenuManager.instance.NextAction();
        }
        else if (name == "carte")
        {
            MenuManager.instance.ShowMenuCarte();
        }
        
    }
    
    /// <summary>
    /// Permet d'afficher dans le jeu tous les boutons ayant le tag 'Menu'
    /// </summary>
    private void OnDrawGizmos()
    {
        this.name = name;
        this.tag = "Menu";
    }
}
