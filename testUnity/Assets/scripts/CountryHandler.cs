using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(PolygonCollider2D))]

public class CountryHandler : MonoBehaviour
{
    public Country country;//Territoire qu'il lui ai associcié
    public Text nbTroupeUI;//Text contenant le nombre de troupe sur ce territoire 
    private SpriteRenderer sprite; //Sprite du territoire qu'il lui est associé
    public Color32 oldColor; //Ancienne couleur du territoire
    public Color32 hoverColor; //Nouvelle couleur du territoire après l'avoir survolé
    public List<CountryHandler> voisins; // List de CountryHandler comoprtant tous ses territoires voisins

    /// <summary>
    /// recupere le sprite de l'image et affiche le nombre de troupe
    /// </summary>
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        showTroupe();
    }

    /// <summary>
    /// Permet d'afficher le nombre de troupe sur le territoire
    /// </summary>
    public void showTroupe()
    {
        nbTroupeUI.text = country.nbTroupe.ToString();
    }

    /// <summary>
    /// Permet de changer la couleur du sprite quand il est survolé 
    /// </summary>
    private void OnMouseEnter()
    {
        oldColor = sprite.color;
        hoverColor = new Color32(oldColor.r, oldColor.g, oldColor.b, 255);
        sprite.color = hoverColor;
    }

    /// <summary>
    /// Permet de remettre la couleur d(avant quand le sprite n'est plus survolé
    /// </summary>
    private void OnMouseExit()
    {
        sprite.color = oldColor;

    }

    /// <summary>
    /// Action différente quand on clique sur un territorie suivant la phase de jeu
    /// </summary>
    void OnMouseUpAsButton()
    {
        switch (CountryManager.instance.PhaseEnCours)
        {
            case 1 :
                this.PhaseUn();
                break;
            case 2 :
                this.PhaseDeux();
                break;
        }
        
    }

    private void OnDrawGizmos()
    {
        country.name = name;
        this.tag = "Country";
    }

    /// <summary>
    /// Permet de changer la couleur du sprite
    /// </summary>
    /// <param name="color">nouvelle couleur</param>
    public void TintColor(Color32 color)
    {
        sprite.color = color;
    }

    /// <summary>
    /// Permet d'afficher l'ecran d'attaque avec les informations de ce territoire
    /// </summary>
    void ShowGUI()
    {
        CountryManager.instance.ShowAttackPanel(country.name + " appartient aux " + country.tribe.ToString() + ". Est-ce que vous voulez vraiment les attaquer?", country.moneyReward, country.expReward);
        GameManager.instance.attackedCountry = country.name;
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.battleWon = false;
        CountryManager.instance.CountrySelectedAttacked = this;
    }

    /// <summary>
    /// Permet de selectionner un territoire et de desactiver tous les autres
    /// Il change de couleur, si on reclique dessus, annule l'action
    /// </summary>
    private void PhaseUn()
    {
        if (country.tribe == (Country.theTribes) CountryManager.instance.TourJoueur) //On regarde si le territoire selectionné est bien au joueur en cours
        {
            if (!CountryManager.instance.CountryIsSelected)
            {
                CountryManager.instance.CountryIsSelected = true;
                
                oldColor = sprite.color;
                hoverColor = new Color32(oldColor.r, oldColor.g, oldColor.b, 190);
                sprite.color = hoverColor;
                
                CountryManager.instance.ShowSliderTroupe(CountryManager.instance.NbTroupePhase1);
                CountryManager.instance.TintThisCountries(this);


            }
            else
            {
                CountryManager.instance.CountryIsSelected = false;
                CountryManager.instance.DisableSliderTroupe();
                CountryManager.instance.TintCountries();
            }
            
            
            //this.country.nbTroupe += CountryManager.instance.nbTerritoire(this.country.tribe);
            
        }
        
    }
    /// <summary>
    /// Permet de selectionner un territoire et d'afficher uniquement les territoires voisins
    /// Il change de couleur, si on reclique dessus, annule l'action
    /// </summary>
    private void PhaseDeux()
    {
        //print("Pressed");
        if (country.tribe == (Country.theTribes)CountryManager.instance.TourJoueur && country.nbTroupe >1) //On regarde si le territoire selectionné est bien au joueur en cours et si il y a strictement plus de 1troupe sur ce territoire
        {
            if (!CountryManager.instance.CountryIsSelected)
            {
                bool res = false;
                foreach (CountryHandler c in this.voisins)
                {
                    if (c.country.tribe != (Country.theTribes)CountryManager.instance.TourJoueur)
                    {
                        res = true;
                    }
                }

                if (res)
                {
                    CountryManager.instance.CountryIsSelected = true;

                    oldColor = sprite.color;
                    hoverColor = new Color32(oldColor.r, oldColor.g, oldColor.b, 190);
                    sprite.color = hoverColor;


                    CountryManager.instance.TintAttaqueCountries(this);

                    CountryManager.instance.ShowAttaqueText();
                }
            }
            else
            {
                CountryManager.instance.CountryIsSelected = false;
                CountryManager.instance.DisableAttaqueText();
                CountryManager.instance.TintCountries();
            }

        }
        else if (country.tribe != (Country.theTribes)CountryManager.instance.TourJoueur && CountryManager.instance.CountryIsSelected)
        {
            CountryManager.instance.CountryIsSelected = false;

            CountryManager.instance.DisableAttaqueText();
            ShowGUI();

        }
    }
}
