using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = System.Random;

public class CountryManager : MonoBehaviour
{
    public static CountryManager instance;

    public GameObject attackPanel;
    public GameObject attaqueText;
    public GameObject attaqueUI;
    public List<GameObject> countryList = new List<GameObject>();
    

    private bool countryIsSelected = false;
    private CountryHandler countrySelected = null;
    

    private CountryHandler countrySelectedAttacked = null;

    public CountryHandler CountrySelected
    {
        get => countrySelected;
        set => countrySelected = value;
    }

    public CountryHandler CountrySelectedAttacked
    {
        get => countrySelectedAttacked;
        set => countrySelectedAttacked = value;
    }

    public bool CountryIsSelected { get => countryIsSelected; set => countryIsSelected = value; }

    
    private int tourJoueur; //Permet de savoir le tour de quel joueur est en cours
    
    public int TourJoueur
    {
        get => tourJoueur;
        set => tourJoueur = value;
    }

    private int nbJoueur = 2;

    public int NbJoueur
    {
        get => nbJoueur;
        set => nbJoueur = value;
    }


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        attackPanel.SetActive(false);
        AddCountryData();
        

        /*if (GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        {
            CountryHandler count = GameObject.Find(GameManager.instance.attackedCountry).GetComponent<CountryHandler>();
            count.country.tribe = (Country.theTribes)tourJoueur;
            GameManager.instance.exp += count.country.expReward;
            GameManager.instance.money += count.country.moneyReward;
            TintCountries();

        }*/
        GameManager.instance.Saving();
    }

    void AddCountryData()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in theArray)
        {
            countryList.Add(country);
        }
        Initialisation();
        GameManager.instance.Loading();
        TintCountries();
    }

    public void TintCountries()
    {

        for(int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            countryList[i].SetActive(true);

            switch (countHandler.country.tribe)
            {
                case Country.theTribes.CLONE:
                    countHandler.TintColor(new Color32(180, 0, 0, 200));
                    break;

                case Country.theTribes.DROIDE:
                    countHandler.TintColor(new Color32(0, 24, 114, 200));
                    break;

                case Country.theTribes.JEDI:
                    countHandler.TintColor(new Color32(0, 180, 0, 200));
                    break;
            }
            
            
            countHandler.showTroupe();
            

            

        }
        
    }

    public void ShowAttackPanel(string description, int moneyReward, int expReward)
    {
        attackPanel.SetActive(true);
        AttackPanel gui = attackPanel.GetComponent<AttackPanel>();
        gui.descriptionText.text = description;
        gui.moneyRewardText.text = "+ " + moneyReward.ToString();
        gui.expRewardText.text = "+ " + expReward.ToString();
        
        switch (countrySelected.country.nbTroupe)
        {
            case 1 :
                gui.nbDe.maxValue = 1;
                break;
            case 2 :
                gui.nbDe.maxValue = 2;
                break;
            default:
                gui.nbDe.maxValue = 3;
                break;
        }

        
        
    }

    public void DisableAttackPanel()
    {
        attackPanel.SetActive(false);
        TintCountries();
    }

    public void StartFight()
    {
        AttackPanel gui = attackPanel.GetComponent<AttackPanel>();
        
        
        
        attackPanel.SetActive(false);
        TintCountries();
        attaqueUI.SetActive(true);

        int res;
        switch (this.countrySelectedAttacked.country.nbTroupe)
        {
            case 1 :
                res = 1;
                break;
            default:
                res = 2;
                break;
        }

        FightSim fightSim = new FightSim((int) gui.nbDe.value, res);
        fightSim.Fight();
        attaqueUI.SetActive(false);

        //SceneManager.LoadScene(1);
    }

    

    public void ShowAttaqueText()
    {
        attaqueText.SetActive(true);
    }

    public void DisableAttaqueText()
    {
        attaqueText.SetActive(false);
    }

    public void TintAttaqueCountries(CountryHandler country)
    {
        this.countrySelected = country;
            for (int j = 0; j < countryList.Count; j++)
            {
                if (!country.voisins.Contains(countryList[j].GetComponent<CountryHandler>()) && country != countryList[j].GetComponent<CountryHandler>())
                {
                    countryList[j].SetActive(false);
  
                }
                else
                {
                    print(countryList[j].name);

                }
                
            }
            foreach (CountryHandler c in country.voisins)
            {
                if (c.country.tribe == (Country.theTribes)tourJoueur)
                {
                    c.gameObject.SetActive(false);
                }
            }




    }

    public void Initialisation()
    {
        this.TourJoueur = 0;
        int nbJoueur = 2;
        int cptJ1 = 0;
        int cptJ2 = 0;
        for (int i = 0; i < countryList.Count; i++)
        {
            int t = UnityEngine.Random.Range(0, 2);
            if (t == 0)
            {
                cptJ1++;
                
            }
            else
            {
                cptJ2++;
            }
            
            if (cptJ1 > 9)
            { 
                t = 1;
            }
            else if (cptJ2 > 9)
            {
                t = 0;
            }
            countryList[i].GetComponent<CountryHandler>().country.tribe = (Country.theTribes)t;
            countryList[i].GetComponent<CountryHandler>().country.nbTroupe = 1;

        }
        for (int i = 0; i < nbJoueur; i++)
        {
            int nbTroupeTotalParJoueur = 30;
            Random random = new Random();
            int j = 0;
            while (j < nbTroupeTotalParJoueur)
            {
                int res = random.Next(countryList.Count);
                if (countryList[res].GetComponent<CountryHandler>().country.tribe == (Country.theTribes) i)
                {
                    countryList[res].GetComponent<CountryHandler>().country.nbTroupe += 1;
                    j++;

                    
                }
            }
        }



        TintCountries();

        
       
        
    }

    public bool ResAttaque(int t1, int t2)
    {
        bool res = false;
        this.countrySelected.country.nbTroupe -= t1;
        this.countrySelectedAttacked.country.nbTroupe -= t2;

        print("-------------------t1" + t1);
        print("-------------------t2" + t2);
        if (this.countrySelectedAttacked.country.nbTroupe <= 0)
        {
            this.countrySelectedAttacked.country.nbTroupe = 1;
            res = true;
        }
        TintCountries();
        

        return res;
    }

    public void finFight()
    {
        if (GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        {
            countrySelectedAttacked.country.tribe = countrySelected.country.tribe;
            TintCountries();

        }

        GameManager.instance.battleWon = false;
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.Saving();
    }
    
    






}
