using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject menu;

    public List<GameObject> menuList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       AddMenuData(); 
    }

    private void Awake()
    {
        instance = this;
        
    }


    void AddMenuData()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Menu") as GameObject[];
        foreach(GameObject menu in theArray)
        {
            menuList.Add(menu);
        }
        TintMenu();
    }
    
    public void TintMenu()
    {

        for(int i = 0; i < menuList.Count; i++)
        {
            menuList[i].SetActive(true);
        }
        
    }
    
    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void DisableMenu()
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        menu.SetActive(false);
        GameManager.instance.DeleteSaveFile();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void NextJoueur()
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

    private void PhaseUn()
    {
        try
        {
            CountryManager.instance.NbTroupePhase1 -= CountryManager.instance.getValueSlider();
            CountryManager.instance.CountrySlectedPhaseUn.country.nbTroupe += CountryManager.instance.getValueSlider();
        }
        catch (Exception e)
        {
            //messagebox erreur
        }
        CountryManager.instance.DisableSliderTroupe();
        CountryManager.instance.TintCountries();

        if (CountryManager.instance.NbTroupePhase1 <= 0)
        {
            CountryManager.instance.CountryIsSelected = false;
            CountryManager.instance.nbTroupePhaseUn.value = 0;
            CountryManager.instance.ChangementPhase();

        }

    }

    private void PhaseDeux()
    {
        CountryManager.instance.ChangementPhase();
        CountryManager.instance.TintCountries();
        GameManager.instance.Saving();
    }  
    
    private void PhaseTrois()
    {
        CountryManager.instance.DisableSliderTroupe();
        CountryManager.instance.TourJoueur++;
        if (CountryManager.instance.TourJoueur > CountryManager.instance.NbJoueur)
        {
            CountryManager.instance.TourJoueur = 0;
        }
        CountryManager.instance.ChangementPhase();
        CountryManager.instance.TintCountries();
        GameManager.instance.Saving();
    }
    
    
}
