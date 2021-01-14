using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class carteManager : MonoBehaviour
{
    public static carteManager instance; //Instance du menu en cours
    
    public GameObject cartePanel; //Panel pour afficher le menu des cartes bonus
    public Button carte1; // carte numero 1
    public Button carte2; // carte numero 2
    public Button carte3; // carte numero 3
    public TextMeshProUGUI valueCarte1; //Nombre de carte numero 1
    public TextMeshProUGUI valueCarte2; //Nombre de carte numero 2
    public TextMeshProUGUI valueCarte3; //Nombre de carte numero 3
    
    private List<int> carteJoueurRouge = new List<int>(); //Liste des cartes du joueur 1
    private List<int> carteJoueurBleu = new List<int>(); //Liste des cartes du joueur 2
    private List<int> carteJoueurVert = new List<int>(); //Liste des cartes du joueur 3
    private List<int> carteJoueurOrange = new List<int>(); //Liste des cartes du joueur 4
    
    //Properties
    public List<int> CarteJoueurRouge
    {
        get => carteJoueurRouge;
        set => carteJoueurRouge = value;
    }

    public List<int> CarteJoueurBleu
    {
        get => carteJoueurBleu;
        set => carteJoueurBleu = value;
    }

    public List<int> CarteJoueurVert
    {
        get => carteJoueurVert;
        set => carteJoueurVert = value;
    }

    public List<int> CarteJoueurOrange
    {
        get => carteJoueurOrange;
        set => carteJoueurOrange = value;
    }

    /// <summary>
    /// Recupere l'instance en cours
    /// </summary>
    private void Awake()
    {
        instance = this;
        
    }
    
    /// <summary>
    /// Permet d'afficher les cartes en fonction du joueur et du mode de jeu
    /// </summary>
    public void AfficherCarte()
    {
        this.cartePanel.gameObject.SetActive(true);
        //Sur la map Europe
        if (CountryManager.instance.Map == 0)
        {
            switch (CountryManager.instance.TourJoueur)
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
        //Sur la map Star Wars
        else
        {
            switch (CountryManager.instance.TourJoueur)
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

    /// <summary>
    /// Permet d'attribuer le bonus da la carte numero 1 en fonction du mode de jeu
    /// </summary>
    public void BonusCarte1()
    {
        //Pour la map Europe
        if (CountryManager.instance.Map == 0)
        {
            switch (CountryManager.instance.TourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 1;
                        this.carteJoueurRouge[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 1:
                    if (this.carteJoueurBleu[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 1;
                        this.carteJoueurBleu[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 2:
                    if (this.carteJoueurVert[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 2;
                        this.carteJoueurVert[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 3:
                    if (this.carteJoueurOrange[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 1;
                        this.carteJoueurOrange[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
            }
        }
        //Pour la map Star Wars
        else
        {
            switch (CountryManager.instance.TourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 2;
                        this.carteJoueurRouge[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 1:
                    if (this.carteJoueurBleu[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 2;
                        this.carteJoueurBleu[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 2:
                    if (this.carteJoueurVert[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 2;
                        this.carteJoueurVert[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
                case 3:
                    if (this.carteJoueurOrange[0] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 2;
                        this.carteJoueurOrange[0]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }

                    break;
            }
        }

        CountryManager.instance.TintCountries();    }
    
    /// <summary>
    /// Permet d'attribuer le bonus da la carte numero 2 en fonction du mode de jeu
    /// </summary>
    public void BonusCarte2()
    {
        //Pour la map Europe
        if (CountryManager.instance.Map == 0)
        {
            switch (CountryManager.instance.TourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 3;
                        this.carteJoueurRouge[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 4;
                        this.carteJoueurBleu[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 3;
                        this.carteJoueurVert[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 4;
                        this.carteJoueurOrange[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            } 
        }
        //Pour la map Star Wars
        else
        {
            switch (CountryManager.instance.TourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 6;
                        this.carteJoueurRouge[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 6;
                        this.carteJoueurBleu[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 6;
                        this.carteJoueurVert[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[1] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 6;
                        this.carteJoueurOrange[1]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            }
        }
        
        CountryManager.instance.TintCountries();    }
    
    /// <summary>
    /// Permet d'attribuer le bonus da la carte numero 3 en fonction du mode de jeu
    /// </summary>
    public void BonusCarte3()
    {
        //Pour la map Europe
        if (CountryManager.instance.Map == 0)
        {
            switch (CountryManager.instance.TourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 8;
                        this.carteJoueurRouge[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 7;
                        this.carteJoueurBleu[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 7;
                        this.carteJoueurVert[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 7;
                        this.carteJoueurOrange[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            }
        }
        //Pour la map Star Wars
        else
        {
            switch (CountryManager.instance.TourJoueur)
            {
                case 0:
                    if (this.carteJoueurRouge[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 9;
                        this.carteJoueurRouge[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (this.carteJoueurBleu[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 9;
                        this.carteJoueurBleu[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (this.carteJoueurVert[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 9;
                        this.carteJoueurVert[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (this.carteJoueurOrange[2] > 0)
                    {
                        CountryManager.instance.nbTroupePhase1 += 9;
                        this.carteJoueurOrange[2]--;
                        this.cartePanel.gameObject.SetActive(false);
                    }
                    break;
            }
        }
       
        CountryManager.instance.TintCountries();
    }
}
