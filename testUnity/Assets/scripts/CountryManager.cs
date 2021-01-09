using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace;
using TMPro;
using Random = System.Random;

public class CountryManager : MonoBehaviour
{
    public static CountryManager instance;

    //Variables
    public GameObject attackPanel; //Panel du combat
    public GameObject attaqueText;
    public GameObject finFightPanel;
    public GameObject finPartiePanel;
    public GameObject cartePanel;
    public Image couleurTeam; //Couleur de la team en cours
    public Slider nbTroupePhaseUn;
    public Text valueSlider;
    public Text textPhase;
    public Text textCombat;
    public GameObject attaqueUI;
    public Canvas Team1;
    public Canvas Team2;
    public Canvas Team3;
    public Canvas Team4;
    public Slider sliderVictoire;
    public TextMeshProUGUI textSliderVictoire;
    public TextMeshProUGUI textJ1;
    public TextMeshProUGUI textJ2;
    public TextMeshProUGUI textJ3;
    public TextMeshProUGUI textJ4;
    public Button carte1;
    public Button carte2;
    public Button carte3;
    public TextMeshProUGUI valueCarte1;
    public TextMeshProUGUI valueCarte2;
    public TextMeshProUGUI valueCarte3;
    public GameObject boutonCarte;
    public GameObject boutonSuivant;
    public GameObject boutonMenu;
    
    private CountryHandler countrySlectedPhaseUn;
    public List<GameObject> countryList = new List<GameObject>();
    private bool countryIsSelected = false;
    private CountryHandler countrySelected = null;
    private int phaseEnCours = 1;
    private int nbTroupePhase1 = 0;
    public CountryHandler countrySelectedAttacked = null;
    private int tourJoueur; //Permet de savoir le tour de quel joueur est en cours
    private int nbJoueur = 4;
    private bool countryIsSelectedAttacked;
    private int map;
    private int nbTroupe;
    private int nombreAttaque = 0;
    private int dataNbAttaque = 4;
    private int dataMode = 1;
    

    private int isBonus = 1;

    private List<int> carteJoueurRouge = new List<int>();
    private List<int> carteJoueurBleu = new List<int>();
    private List<int> carteJoueurVert = new List<int>();
    private List<int> carteJoueurOrange = new List<int>();
    
    public static bool territoireActive = false; 
    
    private List<string> classement = new List<string>();
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

    public bool CountryIsSelectedAttacked
    {
        get => countryIsSelectedAttacked;
        set => countryIsSelectedAttacked = value;
    }
    
    public int Map
    {
        get => map;
        set => map = value;
    }
    
    public int NombreAttaque
    {
        get => nombreAttaque;
        set => nombreAttaque = value;
    }
    
    public int DataMode
    {
        get => dataMode;
        set => dataMode = value;
    }
    
    public List<string> Classement
    {
        get => classement;
        set => classement = value;
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Loading();

        attackPanel.SetActive(false);
        AddCountryData();
        
        GameManager.instance.Saving();
    }

    
    public void Loading()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFileMenu.json"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFileMenu.json",FileMode.Open) ;

            MenuPreparerPartie.SaveData data = (MenuPreparerPartie.SaveData)bf.Deserialize(stream);
            stream.Close();

            this.nbJoueur = data.nbJoueur;
            this.map = data.map;
            this.nbTroupe = data.nbTroupe;
            Country.theTribes = data.tribe;
            this.dataNbAttaque = data.nbAttaque;
            this.dataMode = data.choixMode;
            this.isBonus = data.isBonus;


            CountryManager.instance.TintCountries();
            print("Game loaded");
        }
        else
        {
            print("No SaveFile Found");
        }
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
        this.boutonMenu.SetActive(true);
        this.boutonSuivant.SetActive(true);
        if (this.nombreAttaque == this.dataNbAttaque)
        {
            this.ChangementPhase();
        }
        for(int i = 0; i < countryList.Count; i++) //On parcouts tous les territoires
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();//On recupere le CountryHandler de chaque territoires
            countryList[i].SetActive(true);//On les affiches

            switch (countHandler.country.tribe)//On leur attribut une couleur suivant son propriétaire
            {
                case "Allemagne":
                    countHandler.TintColor(new Color32(180, 0, 0, 200));
                    break;

                case "France":
                    countHandler.TintColor(new Color32(0, 24, 114, 200));
                    break;

                case "Angleterre":
                    countHandler.TintColor(new Color32(0, 180, 0, 200));
                    break;
                
                case "Espagne":
                    countHandler.TintColor(new Color32(180, 90, 0, 200));
                    break;
                
                case "Empire":
                    countHandler.TintColor(new Color32(180, 0, 0, 200));
                    break;

                case "Rebelle":
                    countHandler.TintColor(new Color32(71, 175, 14, 200));
                    break;

                case "Mandalorien":
                    countHandler.TintColor(new Color32(80, 80, 80, 200));
                    break;
                
                case "Jedi":
                    countHandler.TintColor(new Color32(48, 110, 186, 200));
                    break;
            }

            this.global = new List<CountryHandler>();
            this.couleurTeam.color = this.getCouleurTeam(TourJoueur);
            countHandler.showTroupe();// On affiche le nombre de troupe sur le territoire
            this.textCombat.text = "Combat restant : " + (this.dataNbAttaque - this.nombreAttaque).ToString();
            this.AfficherTeam();    

        }
        
    }

    public void AfficherTeam()
    {
        
        teamPanel teamPanel = Team1.GetComponent<teamPanel>();
        teamPanel.nbCountry.text = CountryManager.instance.nbTerritoireTotal(0).ToString();
        string tribe;
        if (Country.theTribes.Count >= 5)
        {
             tribe = Country.theTribes[4];
        }
        else
        {
            tribe = Country.theTribes[0];
        }
        teamPanel.nomTeam.text = tribe.ToString();
        teamPanel.nbTroupe.text = CountryManager.instance.nbTroupeTotal(0).ToString();
        teamPanel.bgcolor.color = this.getCouleurTeam(0);
        
        teamPanel teamPanel2 = Team2.GetComponent<teamPanel>();
        teamPanel2.nbCountry.text = CountryManager.instance.nbTerritoireTotal(1).ToString();
        if (Country.theTribes.Count >= 6)
        {
            tribe = Country.theTribes[5];
        }
        else
        {
            tribe = Country.theTribes[1];
        }
        teamPanel2.nomTeam.text = tribe.ToString();
        teamPanel2.nbTroupe.text = CountryManager.instance.nbTroupeTotal(1).ToString();
        teamPanel2.bgcolor.color = this.getCouleurTeam(1);

        if (nbJoueur >= 3)
        {
            Team3.gameObject.SetActive(true);
            teamPanel teamPanel3 = Team3.GetComponent<teamPanel>();
            teamPanel3.nbCountry.text = CountryManager.instance.nbTerritoireTotal(2).ToString();
            if (Country.theTribes.Count >= 7)
            {
                tribe = Country.theTribes[6];
            }
            else
            {
                tribe = Country.theTribes[2];
            }
            teamPanel3.nomTeam.text = tribe.ToString();
            teamPanel3.nbTroupe.text = CountryManager.instance.nbTroupeTotal(2).ToString();
            teamPanel3.bgcolor.color = this.getCouleurTeam(2);

            if (nbJoueur >= 4)
            {
                Team4.gameObject.SetActive(true);
                teamPanel teamPanel4 = Team4.GetComponent<teamPanel>();
                teamPanel4.nbCountry.text = CountryManager.instance.nbTerritoireTotal(3).ToString();
                if (Country.theTribes.Count == 8)
                {
                    tribe = Country.theTribes[7];
                }
                else
                {
                    tribe = Country.theTribes[3];
                }
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
    /// Debute un combat avec le bon nombre de dé pour le defenseur suivant son nombre de troupe
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
                if (c.country.tribe == Country.theTribes[tourJoueur])
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
            if (c.country.tribe == Country.theTribes[tourJoueur]){
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
        if (this.map == 2)
        {
            this.TourJoueur = 0;
            int i = 0;
            
            while (i < countryList.Count)
            {
                if (countryList[i].GetComponent<CountryHandler>().country.camp == "rebelle" || countryList[i].GetComponent<CountryHandler>().country.camp == "rebelle1")
                {
                    countryList[i].GetComponent<CountryHandler>().country.tribe = Country.theTribes[1];
                }
                else
                {
                    countryList[i].GetComponent<CountryHandler>().country.tribe = Country.theTribes[0];
                }
                countryList[i].GetComponent<CountryHandler>().country.nbTroupe = 1;
                i++;
            }
            for (int ii = 0; ii < nbJoueur; ii++)
            {
                Random random = new Random();
                int j = 0;
                int totalTroupe = this.nbTroupe;
                if (ii == 0)
                {
                    totalTroupe += 10;
                }
                while (j < totalTroupe)
                {
                    int res = random.Next(countryList.Count);
                    if (countryList[res].GetComponent<CountryHandler>().country.tribe == Country.theTribes[ii])
                    {
                        countryList[res].GetComponent<CountryHandler>().country.nbTroupe += 1;
                        j++;
                    }
                }
            }
            
        }
        else
        {
            this.TourJoueur = 0;
            List<int> addTerritoire = new List<int>();
            for (int k = 0; k < this.nbJoueur; k++)
            {
                addTerritoire.Add(0);
            }

            int i = 0;
            int nbJ = 0;
            if (this.map == 0)
            {
                if (this.nbJoueur == 3)
                {
                    nbJ = 6;
                }
                else if (this.nbJoueur == 2)
                {
                    nbJ = 9;
                }
                else
                {
                    nbJ = 5;
                }
            }
            else
            {
                if (this.nbJoueur == 3)
                {
                    nbJ = 6;
                }
                else if (this.nbJoueur == 2)
                {
                    nbJ = 8;
                }
                else
                {
                    nbJ = 4;
                }
            }
        
            while (i < countryList.Count)
            {
                int t = UnityEngine.Random.Range(0, this.nbJoueur);
                if (addTerritoire[t] < nbJ)
                {
                    countryList[i].GetComponent<CountryHandler>().country.tribe = Country.theTribes[t];
                    countryList[i].GetComponent<CountryHandler>().country.nbTroupe = 1;
                    addTerritoire[t]+= 1;
                    i++;
                }
            }
            for (int ii = 0; ii < nbJoueur; ii++)
            {
                Random random = new Random();
                int j = 0;
                while (j < this.nbTroupe)
                {
                    int res = random.Next(countryList.Count);
                    if (countryList[res].GetComponent<CountryHandler>().country.tribe == Country.theTribes[ii])
                    {
                        countryList[res].GetComponent<CountryHandler>().country.nbTroupe += 1;
                        j++;

                    
                    }
                }
            }
        }
        
        
        
        
        this.nbTroupePhase1 = this.nbTerritoire(TourJoueur);
        SetTextPhase();

        
        this.carteJoueurRouge.Add(0);
        this.carteJoueurRouge.Add(0);
        this.carteJoueurRouge.Add(0);
        
        this.carteJoueurBleu.Add(0);
        this.carteJoueurBleu.Add(0);
        this.carteJoueurBleu.Add(0);
        
        this.carteJoueurVert.Add(0);
        this.carteJoueurVert.Add(0);
        this.carteJoueurVert.Add(0);
        
        this.carteJoueurOrange.Add(0);
        this.carteJoueurOrange.Add(0);
        this.carteJoueurOrange.Add(0);
        
        TintCountries();

    }

    public bool ResAttaque(int t1, int t2)
    {
        this.nombreAttaque++;
        bool res = false;
        
        this.countrySelected.country.nbTroupe -= t1;
        this.countrySelectedAttacked.country.nbTroupe -= t2;
        //this.countrySelectedAttacked.country.nbTroupe -= 500;

        int modifNbTroupe = 0;
        if (this.Map == 1 && this.isBonus == 1)
            {
                if (this.CountrySelected.country.name == "Mos_Eisley_Tatooine" && this.CountrySelectedAttacked.country.name == "Coruscant")
                {
                    if (this.CountrySelected.country.nbTroupe > 1)
                    {
                        Random random = new Random();
                        int resRandom = random.Next(this.countrySelected.country.nbTroupe - 1);
                        modifNbTroupe = resRandom;
                    }
                }
                if (this.CountrySelected.country.name == "Coruscant" && this.CountrySelectedAttacked.country.name == "Mos_Eisley_Tatooine")
                {
                    if (CountryManager.instance.CountrySelected.country.nbTroupe > 1)
                    {
                        Random random = new Random();
                        int resRandom = random.Next(this.countrySelected.country.nbTroupe - 1);
                        modifNbTroupe = resRandom;
                    }
                }
                if (this.CountrySelected.country.name == "Alderaan" && this.CountrySelectedAttacked.country.name == "Coruscant")
                {
                    if (this.CountrySelected.country.nbTroupe > 1)
                    {
                        Random random = new Random();
                        int resRandom = random.Next(this.countrySelected.country.nbTroupe - 1);
                        modifNbTroupe = resRandom;
                    }
                }
                if (this.CountrySelected.country.name == "Coruscant" && this.CountrySelectedAttacked.country.name == "Alderaan")
                {
                    if (this.CountrySelected.country.nbTroupe > 1)
                    {
                        Random random = new Random();
                        int resRandom = random.Next(this.countrySelected.country.nbTroupe - 1);
                        modifNbTroupe = resRandom;
                    } 
                }

                this.countrySelected.country.nbTroupe -= modifNbTroupe;
            }
            
        
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
            if (this.nbTerritoireTotal(Country.theTribes.IndexOf(countrySelectedAttacked.country.tribe)) == 1)
            {
                if (!this.Classement.Contains(Country.theTribes.IndexOf(countrySelectedAttacked.country.tribe).ToString()))
                {
                    this.Classement.Add(Country.theTribes.IndexOf(countrySelectedAttacked.country.tribe).ToString());
                }
            }
            countrySelectedAttacked.country.tribe = countrySelected.country.tribe;
            if (this.ConditionVictoire())
            {
                this.Victoire();
                
            }
            else
            {
                this.finFightPanel.SetActive(true);
                this.sliderVictoire.value = 0;
                this.sliderVictoire.maxValue = countrySelected.country.nbTroupe - 1;
            }
        }
        
        GameManager.instance.battleWon = false;
        GameManager.instance.battleHasEnded = false;
        GameManager.instance.Saving();
    }

    public void finFightVictoire()
    {
       
        
            int valueslider = (int) this.sliderVictoire.value;
            countrySelectedAttacked.country.nbTroupe += valueslider;
            countrySelected.country.nbTroupe -= valueslider;
        
            this.finFightPanel.SetActive(false);
            
            Random random = new Random();
            int res = random.Next(2);
            print("=======" + res);

            switch (this.tourJoueur)
            {
                case 0:
                    this.carteJoueurRouge[res]++;
                    break;
                case 1:
                    this.carteJoueurBleu[res]++;
                    break;
                case 2:
                    this.carteJoueurVert[res]++;
                    break;
                case 3:
                    this.carteJoueurOrange[res]++;
                    break;

            }
            
            TintCountries();
        
        
            GameManager.instance.battleWon = false;
            GameManager.instance.battleHasEnded = false;
            GameManager.instance.Saving();
        
        
    }

    public int nbTerritoire(int tourJ)
    {
        string tribe = Country.theTribes[tourJ];
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

        if (this.map == 1 && this.isBonus == 1)
        {
            for (int i = 0; i < countryList.Count; i++)
            {
                CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
                if (countHandler.country.name == "Arme_Etoile_de_la_Mort" || countHandler.country.name == "Réacteur_Etoile_de_la_Mort")
                {
                    if (countHandler.country.tribe == tribe && tribe == "Empire")
                    {
                        res += 3;
                    }
                }
                if (countHandler.country.name == "Endor" || countHandler.country.name == "Hoth" || countHandler.country.name == "Naboo")
                {
                    if (countHandler.country.tribe == tribe && tribe == "Rebelle")
                    {
                        res += 2;
                    }
                }
                if (countHandler.country.name == "Lothal" || countHandler.country.name == "Mandalore" || countHandler.country.name == "Ilum")
                {
                    if (countHandler.country.tribe == tribe && tribe == "Mandalorien")
                    {
                        res += 2;
                    }
                }
                if (countHandler.country.name == "Coruscant")
                {
                    if (countHandler.country.tribe == tribe && tribe == "Jedi")
                    {
                        res += 5;
                    }
                }
            }
        }
        
        return res;
    }
    
    public int nbTerritoireTotal(int tourJ)
    {
        string tribe = Country.theTribes[tourJ];
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
        string tribe = Country.theTribes[tourJ];
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
        if (this.phaseEnCours == 2)
        {
            this.boutonCarte.SetActive(false);
            this.textCombat.gameObject.SetActive(true);
        }
        if (this.phaseEnCours == 3)
        {
            this.textCombat.gameObject.SetActive(false);
            this.nombreAttaque = 0;
        }
        if (this.phaseEnCours > 3)
        {
            this.phaseEnCours = 1;
            /*if (this.dataMode == 2)
            {
                this.AfficherCarte();
            }*/
        }
        SetTextPhase();
        if (this.phaseEnCours == 1)
        {
            if (this.dataMode == 2)
            {
                this.boutonCarte.SetActive(true);
            }
            
        }
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
        switch ((Country.theTribes)[tourJ])
        {
            case "Allemagne":
                res = new Color32(180, 0, 0, 200);
                break;

            case "France":
                res = new Color32(0, 24, 114, 200);
                break;

            case "Angleterre":
                res = new Color32(0, 180, 0, 200);
                break;
            
            case "Espagne":
                res = new Color32(180, 90, 0, 200);
                break;
            
            case "Empire":
                res = new Color32(180, 0, 0, 200);
                break;

            case "Rebelle":
                res = new Color32(71, 175, 14, 200);
                break;

            case "Mandalorien":
                res = new Color32(80, 80, 80, 200);
                break;
            
            case "Jedi":
                res = new Color32(48, 110, 186, 200);
                break;
            
            default:
                res = new Color32(0, 0, 0, 200);
                break;
        }
        
        return res;
    }

    public void ShowSliderTroupe(int max, int min)
    {
        valueSlider.gameObject.SetActive(true);
        nbTroupePhaseUn.value = min;
        nbTroupePhaseUn.maxValue = max;
        nbTroupePhaseUn.minValue = min; 
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
    
    public void setValueTextSliderVictoire()
    {
        this.textSliderVictoire.text = sliderVictoire.value.ToString();
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


    
    public bool ConditionVictoire()
    {
        int resVictoire = 0;
        bool res = false;
        if (this.map == 2)
        {
            int resVictoireRebelle = 0;
            int resVictoireEmpire = 0;
            for (int i = 0; i < countryList.Count; i++)
            {
                if (countryList[i].GetComponent<CountryHandler>().country.camp == "rebelle1")
                {
                    if (countryList[i].GetComponent<CountryHandler>().country.tribe == Country.theTribes[0])
                    {
                        resVictoireRebelle++;
                    }
                }
                if (countryList[i].GetComponent<CountryHandler>().country.camp == "empire1")
                {
                    if (countryList[i].GetComponent<CountryHandler>().country.tribe == Country.theTribes[1])
                    {
                        resVictoireEmpire++;
                    }
                }
            }

            if (resVictoireRebelle == 4)
            {
                this.Classement.Add(Country.theTribes.IndexOf(Country.theTribes[1]).ToString());
                res = true;
            }
            else if (resVictoireEmpire == 2)
            {
                this.Classement.Add(Country.theTribes.IndexOf(Country.theTribes[0]).ToString());
                res = true;
            }
        }
        else
        {
            for (int i = 0; i < countryList.Count; i++)
            {
                CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
                if (countHandler.country.tribe == Country.theTribes[TourJoueur])
                {
                    resVictoire++;
                }
            }

            if (resVictoire == countryList.Count)
            {
                res = true;
            }
        }
        
        

        return res;
    }
    
    public void Victoire()
    {
        this.finPartiePanel.SetActive(true);
            this.textJ3.gameObject.SetActive(false);
            this.textJ4.gameObject.SetActive(false);
            for (int i = 0; i < this.nbJoueur; i++)
            {
                if (!this.Classement.Contains(i.ToString()))
                {
                    this.Classement.Add(i.ToString());
                }
            }
        
            this.Classement.Reverse();
            int position = 0;
            int position2 = 0;
            if (Country.theTribes.Count > 4)
            {
                position2 = 4;
            }
            this.textJ1.text = Country.theTribes[int.Parse(this.Classement[position])+position2];
            this.textJ2.text = Country.theTribes[int.Parse(this.Classement[position + 1])+position2];
            
            if (this.nbJoueur >= 3)
            {
                this.textJ3.gameObject.SetActive(true);
                this.textJ3.text = Country.theTribes[int.Parse(this.Classement[position + 2])];
                if (this.nbJoueur == 4)
                {
                    this.textJ4.gameObject.SetActive(false);
                    this.textJ4.text = Country.theTribes[int.Parse(this.Classement[position + 3])];
                }
            }
            
            //UnityEditor.EditorApplication.isPlaying = false;
        }


    public void AfficherCarte()
    {
        this.cartePanel.gameObject.SetActive(true);
        if (this.map == 0)
        {
            switch (this.tourJoueur)
            {
                case 0:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteRouge1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteRouge2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteRouge3");
                    this.valueCarte1.text = this.carteJoueurRouge[0].ToString();
                    this.valueCarte2.text = this.carteJoueurRouge[1].ToString();
                    this.valueCarte3.text = this.carteJoueurRouge[2].ToString();
                    break;
                case 1:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteBleu1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteBleu2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteBleu3");
                    this.valueCarte1.text = this.carteJoueurBleu[0].ToString();
                    this.valueCarte2.text = this.carteJoueurBleu[1].ToString();
                    this.valueCarte3.text = this.carteJoueurBleu[2].ToString();
                    break;
                case 2:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteVert1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteVert2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteVert3");
                    this.valueCarte1.text = this.carteJoueurVert[0].ToString();
                    this.valueCarte2.text = this.carteJoueurVert[1].ToString();
                    this.valueCarte3.text = this.carteJoueurVert[2].ToString();
                    break;
                case 3:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteOrange1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteOrange2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteOrange3");
                    this.valueCarte1.text = this.carteJoueurOrange[0].ToString();
                    this.valueCarte2.text = this.carteJoueurOrange[1].ToString();
                    this.valueCarte3.text = this.carteJoueurOrange[2].ToString();
                    break;
                default:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteRouge1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteRouge2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteRouge3");
                    this.valueCarte1.text = this.carteJoueurRouge[0].ToString();
                    this.valueCarte2.text = this.carteJoueurRouge[1].ToString();
                    this.valueCarte3.text = this.carteJoueurRouge[2].ToString();
                    break;
            }
        }
        else
        {
            switch (this.tourJoueur)
            {
                case 0:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarRouge1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarRouge2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarRouge3");
                    this.valueCarte1.text = this.carteJoueurRouge[0].ToString();
                    this.valueCarte2.text = this.carteJoueurRouge[1].ToString();
                    this.valueCarte3.text = this.carteJoueurRouge[2].ToString();
                    break;
                case 1:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarVert1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarVert2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarVert3");
                    this.valueCarte1.text = this.carteJoueurBleu[0].ToString();
                    this.valueCarte2.text = this.carteJoueurBleu[1].ToString();
                    this.valueCarte3.text = this.carteJoueurBleu[2].ToString();
                    break;
                case 2:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarGris1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarGris2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarGris3");
                    this.valueCarte1.text = this.carteJoueurVert[0].ToString();
                    this.valueCarte2.text = this.carteJoueurVert[1].ToString();
                    this.valueCarte3.text = this.carteJoueurVert[2].ToString();
                    break;
                case 3:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarBleu1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarBleu2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarBleu3");
                    this.valueCarte1.text = this.carteJoueurOrange[0].ToString();
                    this.valueCarte2.text = this.carteJoueurOrange[1].ToString();
                    this.valueCarte3.text = this.carteJoueurOrange[2].ToString();
                    break;
                default:
                    this.carte1.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarRouge1");
                    this.carte2.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarRouge2");
                    this.carte3.GetComponent<Image>().sprite = Resources.Load<Sprite>("carteStarRouge3");
                    this.valueCarte1.text = this.carteJoueurRouge[0].ToString();
                    this.valueCarte2.text = this.carteJoueurRouge[1].ToString();
                    this.valueCarte3.text = this.carteJoueurRouge[2].ToString();
                    break;
            }
        }


    }


    public void BonusCarte1()
    {
        if (this.map == 0)
        {
            switch (this.tourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[0] > 0)
                    {
                        this.nbTroupePhase1 += 1;
                        this.carteJoueurRouge[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 1:
                    if (this.carteJoueurBleu[0] > 0)
                    {
                        this.nbTroupePhase1 += 1;
                        this.carteJoueurBleu[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 2:
                    if (this.carteJoueurVert[0] > 0)
                    {
                        this.nbTroupePhase1 += 2;
                        this.carteJoueurVert[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 3:
                    if (this.carteJoueurOrange[0] > 0)
                    {
                        this.nbTroupePhase1 += 1;
                        this.carteJoueurOrange[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
            }
        }
        else
        {
            switch (this.tourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[0] > 0)
                    {
                        this.nbTroupePhase1 += 2;
                        this.carteJoueurRouge[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 1:
                    if (this.carteJoueurBleu[0] > 0)
                    {
                        this.nbTroupePhase1 += 2;
                        this.carteJoueurBleu[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 2:
                    if (this.carteJoueurVert[0] > 0)
                    {
                        this.nbTroupePhase1 += 2;
                        this.carteJoueurVert[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 3:
                    if (this.carteJoueurOrange[0] > 0)
                    {
                        this.nbTroupePhase1 += 2;
                        this.carteJoueurOrange[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
            }
        }

        this.TintCountries();
    }
    
    public void BonusCarte2()
    {
        if (this.map == 0)
        {
            switch (this.tourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[1] > 0)
                    {
                        this.nbTroupePhase1 += 3;
                        this.carteJoueurRouge[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[1] > 0)
                    {
                        this.nbTroupePhase1 += 4;
                        this.carteJoueurBleu[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[1] > 0)
                    {
                        this.nbTroupePhase1 += 3;
                        this.carteJoueurVert[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[1] > 0)
                    {
                        this.nbTroupePhase1 += 4;
                        this.carteJoueurOrange[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            } 
        }
        else
        {
            switch (this.tourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[1] > 0)
                    {
                        this.nbTroupePhase1 += 6;
                        this.carteJoueurRouge[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[1] > 0)
                    {
                        this.nbTroupePhase1 += 6;
                        this.carteJoueurBleu[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[1] > 0)
                    {
                        this.nbTroupePhase1 += 6;
                        this.carteJoueurVert[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[1] > 0)
                    {
                        this.nbTroupePhase1 += 6;
                        this.carteJoueurOrange[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            }
        }
        
        this.TintCountries();
    }
    
    public void BonusCarte3()
    {
        if (this.map == 0)
        {
            switch (this.tourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[2] > 0)
                    {
                        this.nbTroupePhase1 += 8;
                        this.carteJoueurRouge[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[2] > 0)
                    {
                        this.nbTroupePhase1 += 7;
                        this.carteJoueurBleu[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[2] > 0)
                    {
                        this.nbTroupePhase1 += 7;
                        this.carteJoueurVert[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[2] > 0)
                    {
                        this.nbTroupePhase1 += 7;
                        this.carteJoueurOrange[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (this.tourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[2] > 0)
                    {
                        this.nbTroupePhase1 += 9;
                        this.carteJoueurRouge[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[2] > 0)
                    {
                        this.nbTroupePhase1 += 9;
                        this.carteJoueurBleu[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[2] > 0)
                    {
                        this.nbTroupePhase1 += 9;
                        this.carteJoueurVert[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[2] > 0)
                    {
                        this.nbTroupePhase1 += 9;
                        this.carteJoueurOrange[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            }
        }
       
        this.TintCountries();
    }

    public void desactiverTerritoire()
    {
        //Récupère la liste de touts les territoires et les désactives 
        for(int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            countryList[i].SetActive(false);
        }

        //Indique si tous les territoires ont été désactivés 
        if(!countryList[(countryList.Count)-1].activeSelf) {
            territoireActive = true;
        }
        
        this.boutonMenu.SetActive(false);
        this.boutonSuivant.SetActive(false);
    }
    
}
