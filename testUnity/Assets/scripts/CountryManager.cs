using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DefaultNamespace;
using Random = System.Random;

public class CountryManager : MonoBehaviour
{
    public static CountryManager instance;

    //Variables
    public GameObject attackPanel; //Panel du combat
    public GameObject attaqueText; 
    public Image couleurTeam; //Couleur de la team en cours
    public Slider nbTroupePhaseUn;
    public Text valueSlider;
    public Text textPhase;
    public GameObject attaqueUI;
    public Canvas Team1;
    public Canvas Team2;
    public Canvas Team3;
    public Canvas Team4;
    private CountryHandler countrySlectedPhaseUn;
    public List<GameObject> countryList = new List<GameObject>();
    private bool countryIsSelected = false;
    private CountryHandler countrySelected = null;
    private int phaseEnCours = 1;
    private int nbTroupePhase1 = 0;
    private CountryHandler countrySelectedAttacked = null;
    private int tourJoueur; //Permet de savoir le tour de quel joueur est en cours
    private int nbJoueur = 4;
    
    
    private List<CountryHandler> global = new List<CountryHandler>();

    
    //Getter et Setter
    public int NbTroupePhase1
    {
        get => nbTroupePhase1;
        set => nbTroupePhase1 = value;
    }
    
    public CountryHandler CountrySlectedPhaseUn => countrySlectedPhaseUn;
    
    public int PhaseEnCours => phaseEnCours;
    
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

    public bool CountryIsSelected
    {
        get => countryIsSelected;
        set => countryIsSelected = value;
    }
    
    public int TourJoueur
    {
        get => tourJoueur;
        set => tourJoueur = value;
    }


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
        
        GameManager.instance.Saving();
    }

    /// <summary>
    /// Permet de recuperer tout les territoires depuis UNITY
    /// </summary>
    void AddCountryData()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[]; //Recupere tous les object ayant le tag Country
        foreach(GameObject country in theArray) //Ajoute tout les territoires dans la liste global
        {
            countryList.Add(country);
        }
        
        Initialisation(); //Initialisation de la partie
        GameManager.instance.Loading(); //Load le fichier text de sauvegarde
        TintCountries();//Affiche tout les terriotires avec leurs informations, tribu, ...
        
    }

    /// <summary>
    /// Permet d'afficher tout les territoires
    /// </summary>
    public void TintCountries()
    {
        for(int i = 0; i < countryList.Count; i++) //On parcouts tous les territoires
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();//On recupere le CountryHandler de chaque territoires
            countryList[i].SetActive(true);//On les affiches

            switch (countHandler.country.tribe)//On leur attribut une couleur suivant son propriétaire
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
                
                case Country.theTribes.SITH:
                    countHandler.TintColor(new Color32(180, 90, 0, 200));
                    break;
            }

            this.global = new List<CountryHandler>();
            this.couleurTeam.color = this.getCouleurTeam(TourJoueur);
            countHandler.showTroupe();// On affiche le nombre de troupe sur le territoire
            this.AfficherTeam();    

        }
        
    }

    public void AfficherTeam()
    {
        
        teamPanel teamPanel = Team1.GetComponent<teamPanel>();
        teamPanel.nbCountry.text = CountryManager.instance.nbTerritoireTotal(0).ToString();
        Country.theTribes tribe = (Country.theTribes) 0;
        teamPanel.nomTeam.text = tribe.ToString();
        teamPanel.nbTroupe.text = CountryManager.instance.nbTroupeTotal(0).ToString();
        teamPanel.bgcolor.color = this.getCouleurTeam(0);
        
        teamPanel teamPanel2 = Team2.GetComponent<teamPanel>();
        teamPanel2.nbCountry.text = CountryManager.instance.nbTerritoireTotal(1).ToString();
        tribe = (Country.theTribes) 1;
        teamPanel2.nomTeam.text = tribe.ToString();
        teamPanel2.nbTroupe.text = CountryManager.instance.nbTroupeTotal(1).ToString();
        teamPanel2.bgcolor.color = this.getCouleurTeam(1);

        if (nbJoueur >= 3)
        {
            Team3.gameObject.SetActive(true);
            teamPanel teamPanel3 = Team3.GetComponent<teamPanel>();
            teamPanel3.nbCountry.text = CountryManager.instance.nbTerritoireTotal(2).ToString();
            tribe = (Country.theTribes) 2;
            teamPanel3.nomTeam.text = tribe.ToString();
            teamPanel3.nbTroupe.text = CountryManager.instance.nbTroupeTotal(2).ToString();
            teamPanel3.bgcolor.color = this.getCouleurTeam(2);

            if (nbJoueur >= 4)
            {
                Team4.gameObject.SetActive(true);
                teamPanel teamPanel4 = Team4.GetComponent<teamPanel>();
                teamPanel4.nbCountry.text = CountryManager.instance.nbTerritoireTotal(3).ToString();
                tribe = (Country.theTribes) 3;
                teamPanel4.nomTeam.text = tribe.ToString();
                teamPanel4.nbTroupe.text = CountryManager.instance.nbTroupeTotal(3).ToString();
                teamPanel4.bgcolor.color = this.getCouleurTeam(3);
            }
        }
    }

    /// <summary>
    /// Menu d'attaque afin de preparer un combat
    /// </summary>
    /// <param name="description">texte a afficher dans le menu</param>
    public void ShowAttackPanel(string description)
    {
        attackPanel.SetActive(true);//On affiche le menu
        
        AttackPanel gui = attackPanel.GetComponent<AttackPanel>();//On initialise le menu
        gui.nbDe.value = 1; //On fixe la valeur du slider a 1
        gui.SetValueTextSlider();//Permet d'afficher la valeur du slider en dessous
        gui.descriptionText.text = description;//Affiche la description du menu

        //Permet de mettre la valeur maximum du slider en fonction du nombre de troupe sur le territoire
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

    /// <summary>
    /// Permet de desactiver 
    /// </summary>
    public void DisableAttackPanel()
    {
        attackPanel.SetActive(false);//Desactive le menu de preparation du combat
        TintCountries();//Reaffiche tous les territoires
    }

    /// <summary>
    /// Debute un combat
    /// </summary>
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
    }
    
    public void ShowAttaqueText()
    {
        attaqueText.SetActive(true);
    }

    public void DisableAttaqueText()
    {
        attaqueText.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="country">Le territoire selectionne allie pour le combat</param>
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
                    //print(countryList[j].name);
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

    /// <summary>
    /// Permet d'afficher tous les territoires reliées au territoire données en parametre
    /// </summary>
    /// <param name="country">territoire sélectionné</param>
    public void TintVoisinsCountries(CountryHandler country)
    {
        this.global.Add(country);

        //On repertorie tous les territories reliés
        List<CountryHandler> listTest = new List<CountryHandler>();
        foreach (CountryHandler c in country.voisins)
        {
            if (c.country.tribe == (Country.theTribes)tourJoueur){
                if (!this.global.Contains(c))
                {
                    listTest.Add(c);
                }
            }
        }

        //On rappelle cette fonction autant de fois que ce territoire a de voisins allié
        if (listTest.Count>0)
        {
            foreach (CountryHandler c in listTest)
            {
                this.TintVoisinsCountries(c);
            }
        }
        
        //Desactive tous les territoires
        for (int j = 0; j < countryList.Count; j++)
        {
            countryList[j].SetActive(false);
        }
        
        //Active uniquement les terrtoires reliées
        foreach (CountryHandler ch in global)
        {
            ch.gameObject.SetActive(true);
        }
        
    }
    
    public void TintThisCountries(CountryHandler country)
    {
        this.countrySlectedPhaseUn = country;
        for (int j = 0; j < countryList.Count; j++)
        {
            if (!country.Equals(countryList[j].GetComponent<CountryHandler>()))
            {
                countryList[j].SetActive(false);
  
            }
        }
    }

    public void Initialisation()
    {
        this.TourJoueur = 0;
        List<int> addTerritoire = new List<int>();
        for (int k = 0; k < this.nbJoueur; k++)
        {
            addTerritoire.Add(0);
        }

        int i = 0;
        int nbJ;
        if (this.nbJoueur != 4)
        {
             nbJ = countryList.Count / nbJoueur;
        }
        else
        {
             nbJ = 5;
        }
        while (i < countryList.Count)
        {
            int t = UnityEngine.Random.Range(0, this.nbJoueur);
            if (addTerritoire[t] < nbJ)
            {
                countryList[i].GetComponent<CountryHandler>().country.tribe = (Country.theTribes)t;
                countryList[i].GetComponent<CountryHandler>().country.nbTroupe = 1;
                addTerritoire[t]+= 1;
                i++;
            }



        }
        for (int ii = 0; ii < nbJoueur; ii++)
        {
            int nbTroupeTotalParJoueur = 30;
            Random random = new Random();
            int j = 0;
            while (j < nbTroupeTotalParJoueur)
            {
                int res = random.Next(countryList.Count);
                if (countryList[res].GetComponent<CountryHandler>().country.tribe == (Country.theTribes) ii)
                {
                    countryList[res].GetComponent<CountryHandler>().country.nbTroupe += 1;
                    j++;

                    
                }
            }
        }

        this.nbTroupePhase1 = this.nbTerritoire(TourJoueur);
        SetTextPhase();
        TintCountries();

        
       
        
    }

    public bool ResAttaque(int t1, int t2)
    {
        bool res = false;
        this.countrySelected.country.nbTroupe -= t1;
        this.countrySelectedAttacked.country.nbTroupe -= t2;

        
        if (this.countrySelectedAttacked.country.nbTroupe <= 0)
        {
            this.countrySelected.country.nbTroupe -= 1;
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
            this.ConditionVictoire();

        }

        GameManager.instance.battleWon = false;
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.Saving();
    }

    public int nbTerritoire(int tourJ)
    {
        Country.theTribes tribe = (Country.theTribes) tourJ;
        int count = 0;
        int res = 0;
        for (int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            if (countHandler.country.tribe == tribe)
            {
                count++;
            }
        }

        if (count <= 10)
        {
            res = 3;
        }
        else if (count >10 && count <=14)
        {
            res = 4;
        }
        else
        {
            res = 5;
        }
        return res;
    }
    
    public int nbTerritoireTotal(int tourJ)
    {
        Country.theTribes tribe = (Country.theTribes) tourJ;
        int count = 0;
        for (int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            if (countHandler.country.tribe == tribe)
            {
                count++;
            }
        }
        
        return count;
    }
    public int nbTroupeTotal(int tourJ)
    {
        Country.theTribes tribe = (Country.theTribes) tourJ;
        int count = 0;
        for (int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            if (countHandler.country.tribe == tribe)
            {
                count += countHandler.country.nbTroupe;
            }
        }
        
        return count;
    }

    public void ChangementPhase()
    {
        CountryManager.instance.CountryIsSelected = false;
        this.phaseEnCours++;
        if (this.phaseEnCours > 3)
        {
            this.phaseEnCours = 1;
        }
        SetTextPhase();
    }
    
    public void ResetPhase()
    {
        CountryManager.instance.CountryIsSelected = false;
        this.nbTroupePhase1 = this.nbTerritoire(TourJoueur);
        this.phaseEnCours = 1;
        SetTextPhase();
    }

    public Color32 getCouleurTeam(int tourJ)
    {
        Color32 res;
        switch ((Country.theTribes)tourJ)
        {
            case Country.theTribes.CLONE:
                res = new Color32(180, 0, 0, 200);
                break;

            case Country.theTribes.DROIDE:
                res = new Color32(0, 24, 114, 200);
                break;

            case Country.theTribes.JEDI:
                res = new Color32(0, 180, 0, 200);
                break;
            
            case Country.theTribes.SITH:
                res = new Color32(180, 90, 0, 200);
                break;
            
            default:
                res = new Color32(0, 0, 0, 200);
                break;
        }
        
        return res;
    }

    public void ShowSliderTroupe(int max)
    {
        valueSlider.gameObject.SetActive(true);
        nbTroupePhaseUn.value = 0;
        nbTroupePhaseUn.maxValue = max;
        nbTroupePhaseUn.gameObject.SetActive(true);
    }

    public void DisableSliderTroupe()
    {
        valueSlider.gameObject.SetActive(false);
        nbTroupePhaseUn.gameObject.SetActive(false);

    }

    public int getValueSlider()
    {
        return (int) nbTroupePhaseUn.value;
    }

    public void setValueTextSlider()
    {
        valueSlider.text = nbTroupePhaseUn.value.ToString();
    }

    public void SetTextPhase()
    {
        String res = "";
        switch (PhaseEnCours)
        {
            case 1 :
                res = "Déploiement";
                break;
            case 2 :
                res = "Combat";
                break;
            case 3 :
                res = "Déplacement";
                break;
        }

        textPhase.text = "Phase " + PhaseEnCours.ToString() + " : " + res;
    }












    public void ConditionVictoire()
    {
        int resVictoire = 0;
        for (int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            if (countHandler.country.tribe == (Country.theTribes)TourJoueur)
            {
                resVictoire++;
            }
        }

        if (resVictoire == countryList.Count)
        {
            GameManager.instance.DeleteSaveFile();
            SceneManager.LoadScene(0);
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
