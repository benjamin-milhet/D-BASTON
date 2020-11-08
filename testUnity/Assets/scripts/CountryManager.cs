using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CountryManager : MonoBehaviour
{
    public static CountryManager instance;

    public GameObject attackPanel;
    public GameObject menu;
    public GameObject attaqueText;
    public List<GameObject> countryList = new List<GameObject>();

    private bool countryIsSelected = false;

    public bool CountryIsSelected { get => countryIsSelected; set => countryIsSelected = value; }

    
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        attackPanel.SetActive(false);
        AddCountryData();
        

        if (GameManager.instance.battleHasEnded && GameManager.instance.battleWon)
        {
            CountryHandler count = GameObject.Find(GameManager.instance.attackedCountry).GetComponent<CountryHandler>();
            count.country.tribe = Country.theTribes.CLONE;
            GameManager.instance.exp += count.country.expReward;
            GameManager.instance.money += count.country.moneyReward;
            TintCountries();

        }
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

            
        }
        
    }

    public void ShowAttackPanel(string description, int moneyReward, int expReward)
    {
        attackPanel.SetActive(true);
        AttackPanel gui = attackPanel.GetComponent<AttackPanel>();
        gui.descriptionText.text = description;
        gui.moneyRewardText.text = "+ " + moneyReward.ToString();
        gui.expRewardText.text = "+ " + expReward.ToString();
    }

    public void DisableAttackPanel()
    {
        attackPanel.SetActive(false);
        TintCountries();
    }

    public void StartFight()
    {
        SceneManager.LoadScene(1);
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

      
            for (int j = 0; j < countryList.Count; j++)
            {
                if (!country.voisins.Contains(countryList[j].GetComponent<CountryHandler>()) && country != countryList[j].GetComponent<CountryHandler>())
                {
                    if (!(countryList[j].name.Equals("Menu")) )
                    {
                        countryList[j].SetActive(false);
                        
                    

                    }  
                }
                else
                {
                    print(countryList[j].name);

                }
                
            }
            foreach (CountryHandler c in country.voisins)
            {
                if (c.country.tribe == Country.theTribes.CLONE)
                {
                    c.gameObject.SetActive(false);
                }
            }




    }

    public void Initialisation()
    {
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
            if (!countryList[i].name.Equals("Menu"))
            {
                if (cptJ1 > 9)
                {
                    t = 1;
                }
                else if (cptJ2 > 9)
                {
                    t = 0;
                }
                countryList[i].GetComponent<CountryHandler>().country.tribe = (Country.theTribes)t;


            }
            TintCountries();
        }
    }






}
