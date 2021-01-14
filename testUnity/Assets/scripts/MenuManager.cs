using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance; //Instance du menu en cours
    public GameObject menu; //Menu pour gerer le volume et retourner au menu
    public GameObject menuCarte; //Menu pour gerer les cartes bonus
    public GameObject boutonCarte; //Bouton pour activer le menu des cartes
    public Slider volumeMusique; //Slider pour gerer le vilume de la musique
    public AudioSource audioMusique;//Musique de fond

    public List<GameObject> menuList = new List<GameObject>();

    // Start is called before the first frame update
    //
    void Start()
    {
       AddMenuData(); 
    }

    /// <summary>
    /// Recupere l'instance en cours
    /// </summary>
    private void Awake()
    {
        instance = this;
        
    }

    /// <summary>
    /// Permet d'afficher toute les information des menus
    /// </summary>
    void AddMenuData()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Menu") as GameObject[];
        foreach(GameObject menu in theArray)
        {
            menuList.Add(menu);
        }
        TintMenu();
    }
    
    /// <summary>
    /// Permet d'afficher tous les boutons des menus
    /// </summary>
    public void TintMenu()
    {

        for(int i = 0; i < menuList.Count; i++)
        {
            menuList[i].SetActive(true);
        }

        if (CountryManager.instance.DataMode != 2)
        {
            this.boutonCarte.SetActive(false);
        }
        
    }
    
    /// <summary>
    /// Permet d'activer le menu parametre
    /// </summary>
    public void ShowMenu()
    {
        menu.SetActive(true);
    }
    
    /// <summary>
    /// Permet d'activer le menu carte et de descativer la carte
    /// </summary>
    public void ShowMenuCarte()
    {
        CountryManager.instance.desactiverTerritoire();
        carteManager.instance.AfficherCarte();
    }

    /// <summary>
    /// Permet de desactiver le menu parametre
    /// </summary>
    public void DisableMenu()
    {
        menu.SetActive(false);
    }
    
    /// <summary>
    /// Permet de desactiver le menu carte et de réafficher le carte et toutes les images
    /// </summary>
    public void DisableMenuCarte()
    {
        menuCarte.SetActive(false);
        CountryManager.instance.TintCountries();
    }

    /// <summary>
    /// Permet de changer d'action suivant le phase de jeu
    /// </summary>
    public void NextAction()
    {
        switch (CountryManager.instance.PhaseEnCours)
        {
            case 1 :
                this.PhaseUn();
                break;
            case 2 :
                this.PhaseDeux();
                break;
            case 3 :
                this.PhaseTrois();
                break;
        }
        

    }
    
    /**
     * Pour les boutons 'Menu', a chaque phase ils peuvent avoir des actions différentes
     */
    
    /// <summary>
    /// Permet de gerer les boutons menus pendant la phase 1 : Deploiements
    /// </summary>
    private void PhaseUn()
    {
        //Recupere la valeur du slider lors après avoir appuyer sur le bouton suivant
        try
        {
            CountryManager.instance.NbTroupePhase1 -= CountryManager.instance.getValueSlider();
            CountryManager.instance.CountrySlectedPhaseUn.country.nbTroupe += CountryManager.instance.getValueSlider();
        }
        catch (Exception e)
        {
            //messagebox erreur
        }

        //On desactive le slider
        CountryManager.instance.DisableSliderTroupe();
        CountryManager.instance.nbTroupePhaseUn.value = 0;
        CountryManager.instance.CountryIsSelected = false;
        CountryManager.instance.TintCountries();

        //Une fois que le joueurs a deployer toutes ses troupes, passe automatiquement a la phase suivante
        if (CountryManager.instance.NbTroupePhase1 <= 0)
        {
            CountryManager.instance.ChangementPhase();
        }

    }

    /// <summary>
    /// Si le joueur appuye sur le bouton suivant, change de phase
    /// </summary>
    private void PhaseDeux()
    {
        CountryManager.instance.ChangementPhase();
        CountryManager.instance.TintCountries();
    }  
    
    /// <summary>
    /// Si le joueur appuye sur le bouton suivant, deplace le nombre de troue selectionné a partir du slider
    /// </summary>
    private void PhaseTrois()
    {
        if (CountryManager.instance.CountryIsSelectedAttacked)
        {
            //recupere la valeur du slider
            int value_slider = CountryManager.instance.getValueSlider();
            
            //Sur la map star wars, si le joueur deplace des troupes a travers le champs d'asteroide, il pert plus de troupe
            if (CountryManager.instance.Map == 1)
            {
                this.contrainteMapStarWars(value_slider);
            }
            CountryManager.instance.CountrySelected.country.nbTroupe -= CountryManager.instance.getValueSlider();
            CountryManager.instance.CountrySelectedAttacked.country.nbTroupe += value_slider;
        }

        CountryManager.instance.CountryIsSelectedAttacked = false;
        CountryManager.instance.DisableSliderTroupe();
        
        //Passe au joueur suivant
        CountryManager.instance.TourJoueur++;
        
        //Verifie si le joueur suivant existe
        this.Verification();
        
        //Reinitialise les valeurs et passe la prochaine phase
        CountryManager.instance.NbTroupePhase1 = CountryManager.instance.nbtroupeTerritoire(CountryManager.instance.TourJoueur);
        CountryManager.instance.nbTroupePhaseUn.value = 0;
        CountryManager.instance.ChangementPhase();
        CountryManager.instance.TintCountries();

    }

    /// <summary>
    /// Permet de verifier si le joueur suivant existe ou n'est pas éliminé de la partie
    /// </summary>
    public void Verification()
    {
        int verif = (CountryManager.instance.NbJoueur - 1);
        if (CountryManager.instance.NbJoueur > 4)
        {
            verif = 3;
        }
        if (CountryManager.instance.TourJoueur > verif)
        {
            CountryManager.instance.TourJoueur = 0;
        }
        
        //Rappelle la fonction si le joueur suivant n'existe pas pour tester son suivant
        if (CountryManager.instance.nbTerritoireTotal(CountryManager.instance.TourJoueur) == 0)
        {
            CountryManager.instance.TourJoueur++;
            
            this.Verification();
        }
    }
    
    /// <summary>
    /// Permet de gerer le volume de la musique dans le menu paramètre
    /// </summary>
    public void SetVolumeMusique()
    {
        this.audioMusique.volume = this.volumeMusique.value;
    }
    
    /// <summary>
    /// Permet de retourner au menu a partir du menu paramètre
    /// Change de scene pour revenir au menu principal
    /// </summary>
    public void retourMenu()
    {
        MenuPreparerPartie.DeleteSaveFile();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Permet de verifier, dans la map star wars, si le joueur deplace des troupes a travers le champs d'asteroide
    /// </summary>
    /// <param name="value_slider">nombre de troupe a déplacer de base</param>
    /// <returns>nombre de troupe a déplacer après en avoir enlever de facon aléatoire</returns>
    public int contrainteMapStarWars(int value_slider)
    {
        //On regarde les chemins qui passe par le champs d'asteroide
        if (CountryManager.instance.CountrySelected.country.name == "Mos_Eisley_Tatooine" && CountryManager.instance.CountrySelectedAttacked.country.name == "Coruscant")
        {
            if (CountryManager.instance.CountrySelected.country.nbTroupe > 1)
            {
                Random random = new Random();
                int res = random.Next(value_slider);
                value_slider -= res;
            }
        }
        if (CountryManager.instance.CountrySelected.country.name == "Coruscant" && CountryManager.instance.CountrySelectedAttacked.country.name == "Mos_Eisley_Tatooine")
        {
            if (CountryManager.instance.CountrySelected.country.nbTroupe > 1)
            {
                Random random = new Random();
                int res = random.Next(value_slider);
                value_slider -= res;
            }
        }
        if (CountryManager.instance.CountrySelected.country.name == "Alderaan" && CountryManager.instance.CountrySelectedAttacked.country.name == "Coruscant")
        {
            if (CountryManager.instance.CountrySelected.country.nbTroupe > 1)
            {
                Random random = new Random();
                int res = random.Next(value_slider);
                value_slider -= res;
            }
        }
        if (CountryManager.instance.CountrySelected.country.name == "Coruscant" && CountryManager.instance.CountrySelectedAttacked.country.name == "Alderaan")
        {
            if (CountryManager.instance.CountrySelected.country.nbTroupe > 1)
            {
                Random random = new Random();
                int res = random.Next(value_slider);
                value_slider -= res;
            }
        }
        
        return value_slider;
    }
    
    
    
    
    
}
