using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSim : MonoBehaviour
{
    //Valeurs du nombre de dés lancés
    private int valueDe;
    private int valueDeAttacked;

    //Liste des territoires 
    public List<GameObject> countryList;

    //Listes des résultats des dés 
    private static List<int> resultat = new List<int>();
    private static List<int> resultatAttacked = new List<int>();
    public static bool fin;

    //Tous les types de dés
    public GameObject diceRD;
    public GameObject diceRG;
    public GameObject diceBD;
    public GameObject diceBG;
    public GameObject diceVD;
    public GameObject diceVG;
    public GameObject diceJD;
    public GameObject diceJG;

    //Dés 
    private GameObject dice1;
    private GameObject dice2;
    private GameObject dice3;

    //Dés adverses 
    private GameObject dice4;
    private GameObject dice5;
    private GameObject dice6;

    //Valeur renvoyée par les dés 
    private static int r = 0;
    private static double r2 = 0;
    private static double r3 = 0;

    //Valeur renvoyée par les dés adverses
    private static double r4 = 0;
    private static double r5 = 0;
    private static double r6 = 0;

    //Variable permettant de savoir si les territoires sont actifs ou non
    private static bool active = false; 

    //Valeurs récupérant les variables valueDe et valueDeAttacked
    private static int nbDe = 1;
    private static int nbAt = 1;

    //Couroutine
    private IEnumerator coroutine;
    private IEnumerator coroutine2;
    private IEnumerator coroutine3;
    private IEnumerator coroutine4;
    private IEnumerator coroutine5;

    //GameObject à désactiver + fonds à activer 
    public GameObject menu;
    public GameObject passer;
    public GameObject jetDes;
    public GameObject jetBVJ;
    public GameObject jetBVV;
    public GameObject jetJVR;
    public GameObject jetVVJ;
    public GameObject jetVVR;

    //Images et textes finaux
    public Canvas resAttackCanva;
    public Canvas resAttackedCanva;
    public Canvas resBastonCanva;

    //Instance des dés pour récupérer la valeur
    private Dice value11;
    private Dice value12;
    private Dice value13;
    private Dice value14;
    private Dice value15;
    private Dice value16;
    private Dice2 value21;
    private Dice2 value22;
    private Dice2 value23;
    private Dice2 value24;
    private Dice2 value25;
    private Dice2 value26;

    //Constructeur qui récupère les valeurs du slide du nombre de dés  
    public FightSim(int valueDe, int valueDeAttacked)
    {
        this.valueDe = valueDe;
        this.valueDeAttacked = valueDeAttacked;
        countryList = CountryManager.instance.countryList;
    }

    //Méthode permettant de récupérer et désactiver les territoires (pour les collisions avec le dé)
    public void Fight()
    {   
        //Récupère la liste de touts les territoires et les désactives 
        for(int i = 0; i < countryList.Count; i++)
        {
            CountryHandler countHandler = countryList[i].GetComponent<CountryHandler>();
            countryList[i].SetActive(false);
        }

        //Indique si tous les territoires ont été désactivés 
        if(!countryList[(countryList.Count)-1].activeSelf) {
            active = true;
        }

        nbDe = this.valueDe;
        nbAt = this.valueDeAttacked;
    } 

    //Méthode qui gère les dés et le résultat
    void Update() {

        //Si tous les territoires sont désactivés (pour les collisions)
        if(active) {

            //Désactivation des "boutons" passer et menu (pour les collisions)
            passer.SetActive(false);
            menu.SetActive(false);

            //Variables pour savoir qui attaque et qui se fait attaquer
            int tourJoueur = CountryManager.instance.TourJoueur;
            int nbJoueurAtt = Country.theTribes.IndexOf(CountryManager.instance.countrySelectedAttacked.country.tribe);

            //Variable pour savoir si le dé part de la gauche ou la droite
            bool startDroite = false;

            //Case of pour savoir quels dés instancier en fonction de qui attaque et de qui se fait attaquer
            switch (tourJoueur)
            {
                //Le joueur rouge attaque...
                case 0: 
                    //Instantiation des dés sur le modèle du dé "R"ouge à "D"roite 
                    dice1 = Instantiate<GameObject>(diceRD);
                    dice2 = Instantiate<GameObject>(diceRD);
                    dice3 = Instantiate<GameObject>(diceRD);
                    value11 = dice1.GetComponent<Dice>();
                    value12 = dice2.GetComponent<Dice>();
                    value13 = dice3.GetComponent<Dice>();
                    switch (nbJoueurAtt)
                    {
                        //...le joueur bleu
                        case 1:
                            dice4 = Instantiate<GameObject>(diceBG);
                            dice5 = Instantiate<GameObject>(diceBG);
                            dice6 = Instantiate<GameObject>(diceBG);
                            value24 = dice4.GetComponent<Dice2>();
                            value25 = dice5.GetComponent<Dice2>();
                            value26 = dice6.GetComponent<Dice2>();
                            jetDes.SetActive(true);
                        break;
                        //...le joueur vert
                        case 2:
                            dice4 = Instantiate<GameObject>(diceVG);
                            dice5 = Instantiate<GameObject>(diceVG);
                            dice6 = Instantiate<GameObject>(diceVG);
                            value24 = dice4.GetComponent<Dice2>();
                            value25 = dice5.GetComponent<Dice2>();
                            value26 = dice6.GetComponent<Dice2>();
                            jetVVR.SetActive(true);
                        break;
                        //...le joueur jaune
                        case 3: 
                            dice4 = Instantiate<GameObject>(diceJG);
                            dice5 = Instantiate<GameObject>(diceJG);
                            dice6 = Instantiate<GameObject>(diceJG);
                            value24 = dice4.GetComponent<Dice2>();
                            value25 = dice5.GetComponent<Dice2>();
                            value26 = dice6.GetComponent<Dice2>();
                            jetJVR.SetActive(true);
                        break;
                    }
                    startDroite = true;
                break;
                //Le joueur bleu attaque...
                case 1:
                    dice1 = Instantiate<GameObject>(diceBG);
                    dice2 = Instantiate<GameObject>(diceBG);
                    dice3 = Instantiate<GameObject>(diceBG);
                    value21 = dice1.GetComponent<Dice2>();
                    value22 = dice2.GetComponent<Dice2>();
                    value23 = dice3.GetComponent<Dice2>();
                    switch (nbJoueurAtt)
                    {
                        //...le joueur rouge
                        case 0:
                            dice4 = Instantiate<GameObject>(diceRD);
                            dice5 = Instantiate<GameObject>(diceRD);
                            dice6 = Instantiate<GameObject>(diceRD);
                            value14 = dice4.GetComponent<Dice>();
                            value15 = dice5.GetComponent<Dice>();
                            value16 = dice6.GetComponent<Dice>();
                            jetDes.SetActive(true);
                        break;
                        //...le joueur vert
                        case 2:
                            dice4 = Instantiate<GameObject>(diceVD);
                            dice5 = Instantiate<GameObject>(diceVD);
                            dice6 = Instantiate<GameObject>(diceVD);
                            value14 = dice4.GetComponent<Dice>();
                            value15 = dice5.GetComponent<Dice>();
                            value16 = dice6.GetComponent<Dice>();
                            jetBVV.SetActive(true);
                        break;
                        //...le joueur jaune
                        case 3: 
                            dice4 = Instantiate<GameObject>(diceJD);
                            dice5 = Instantiate<GameObject>(diceJD);
                            dice6 = Instantiate<GameObject>(diceJD);
                            value14 = dice4.GetComponent<Dice>();
                            value15 = dice5.GetComponent<Dice>();
                            value16 = dice6.GetComponent<Dice>();
                            jetBVJ.SetActive(true);
                        break;
                    }
                break;
                //Le joueur vert attaque...
                case 2:
                    switch (nbJoueurAtt)
                    {
                        //...le joueur rouge
                        case 0:
                            dice1 = Instantiate<GameObject>(diceVG);
                            dice2 = Instantiate<GameObject>(diceVG);
                            dice3 = Instantiate<GameObject>(diceVG);
                            value21 = dice1.GetComponent<Dice2>();
                            value22 = dice2.GetComponent<Dice2>();
                            value23 = dice3.GetComponent<Dice2>();
                            dice4 = Instantiate<GameObject>(diceRD);
                            dice5 = Instantiate<GameObject>(diceRD);
                            dice6 = Instantiate<GameObject>(diceRD);
                            value14 = dice4.GetComponent<Dice>();
                            value15 = dice5.GetComponent<Dice>();
                            value16 = dice6.GetComponent<Dice>();
                            jetVVR.SetActive(true);
                        break;
                        //...le joueur bleu
                        case 1:
                            dice1 = Instantiate<GameObject>(diceVD);
                            dice2 = Instantiate<GameObject>(diceVD);
                            dice3 = Instantiate<GameObject>(diceVD);
                            value11 = dice1.GetComponent<Dice>();
                            value12 = dice2.GetComponent<Dice>();
                            value13 = dice3.GetComponent<Dice>();
                            dice4 = Instantiate<GameObject>(diceBG);
                            dice5 = Instantiate<GameObject>(diceBG);
                            dice6 = Instantiate<GameObject>(diceBG);
                            value24 = dice4.GetComponent<Dice2>();
                            value25 = dice5.GetComponent<Dice2>();
                            value26 = dice6.GetComponent<Dice2>();
                            jetBVV.SetActive(true);
                            startDroite = true;
                        break;
                        //...le joueur jaune
                        case 3: 
                            dice1 = Instantiate<GameObject>(diceVG);
                            dice2 = Instantiate<GameObject>(diceVG);
                            dice3 = Instantiate<GameObject>(diceVG);
                            value21 = dice1.GetComponent<Dice2>();
                            value22 = dice2.GetComponent<Dice2>();
                            value23 = dice3.GetComponent<Dice2>();
                            dice4 = Instantiate<GameObject>(diceJD);
                            dice5 = Instantiate<GameObject>(diceJD);
                            dice6 = Instantiate<GameObject>(diceJD);
                            value14 = dice4.GetComponent<Dice>();
                            value15 = dice5.GetComponent<Dice>();
                            value16 = dice6.GetComponent<Dice>();
                            jetVVJ.SetActive(true);
                        break;
                    }
                break;
                //Le joueur jaune attaque...
                case 3:
                    switch (nbJoueurAtt)
                    {
                        //...le joueur rouge
                        case 0:
                            dice1 = Instantiate<GameObject>(diceJG);
                            dice2 = Instantiate<GameObject>(diceJG);
                            dice3 = Instantiate<GameObject>(diceJG);
                            value21 = dice1.GetComponent<Dice2>();
                            value22 = dice2.GetComponent<Dice2>();
                            value23 = dice3.GetComponent<Dice2>();
                            dice4 = Instantiate<GameObject>(diceRD);
                            dice5 = Instantiate<GameObject>(diceRD);
                            dice6 = Instantiate<GameObject>(diceRD);
                            value14 = dice4.GetComponent<Dice>();
                            value15 = dice5.GetComponent<Dice>();
                            value16 = dice6.GetComponent<Dice>();
                            jetJVR.SetActive(true);
                        break;
                        //...le joueur bleu
                        case 1:
                            dice1 = Instantiate<GameObject>(diceJD);
                            dice2 = Instantiate<GameObject>(diceJD);
                            dice3 = Instantiate<GameObject>(diceJD);
                            value11 = dice1.GetComponent<Dice>();
                            value12 = dice2.GetComponent<Dice>();
                            value13 = dice3.GetComponent<Dice>();
                            dice4 = Instantiate<GameObject>(diceBG);
                            dice5 = Instantiate<GameObject>(diceBG);
                            dice6 = Instantiate<GameObject>(diceBG);
                            value24 = dice4.GetComponent<Dice2>();
                            value25 = dice5.GetComponent<Dice2>();
                            value26 = dice6.GetComponent<Dice2>();
                            jetBVJ.SetActive(true);
                            startDroite = true;
                        break;
                        //...le joueur vert
                        case 2: 
                            dice1 = Instantiate<GameObject>(diceJD);
                            dice2 = Instantiate<GameObject>(diceJD);
                            dice3 = Instantiate<GameObject>(diceJD);
                            value11 = dice1.GetComponent<Dice>();
                            value12 = dice2.GetComponent<Dice>();
                            value13 = dice3.GetComponent<Dice>();
                            dice4 = Instantiate<GameObject>(diceVG);
                            dice5 = Instantiate<GameObject>(diceVG);
                            dice6 = Instantiate<GameObject>(diceVG);
                            value24 = dice4.GetComponent<Dice2>();
                            value25 = dice5.GetComponent<Dice2>();
                            value26 = dice6.GetComponent<Dice2>();
                            jetVVJ.SetActive(true);
                            startDroite = true;
                        break;
                    }
                break;
            }

            //Instancie le nombre de dés en fonction de la valeur du slider et récupère son résultat
            dice1.SetActive(true);

            //Si le dé part de droite... 
            if(startDroite == true) {
                r = value11.value();
            }
            else {
                r = value21.value();
            }
            resultat.Add((int)r+1);

            //Si deux dés 
            if(nbDe == 2) {
                dice2.SetActive(true);
                if(startDroite == true) {
                    r2 = value12.value();
                }
                else {
                    r2 = value22.value();
                }
                resultat.Add((int)r2+1);
            }

            //Si trois dés
            else if(nbDe == 3) {
                dice2.SetActive(true);
                if(startDroite == true) {
                    r2 = value12.value();
                }
                else {
                    r2 = value22.value();
                }
                dice3.SetActive(true);
                if(startDroite == true) {
                    r3 = value13.value();
                }
                else {
                    r3 = value23.value();
                }
                resultat.Add((int)r2+1);
                resultat.Add((int)r3+1);
            }

            //Trie du tableau contenant les résultats des dés
            resultat.Sort();

            //Début de la couroutine pour les autres dés
            coroutine4 = jet2(startDroite);
            StartCoroutine(coroutine4);

            //Démarre les 2 couroutines 
            coroutine = wait();
            StartCoroutine(coroutine);
            coroutine2 = wait2(resultat, resultatAttacked);
            StartCoroutine(coroutine2);
            coroutine5 = resBattle(resultat, resultatAttacked);
            StartCoroutine(coroutine5);

            //Les territoires redevienent actifs 
            active = false;
        }
    }

    //Couroutine qui après 5 secondes, lance les dés adverses
    IEnumerator jet2(bool startDroite)
    {
        yield return new WaitForSeconds(5.0f);

        //Instancie le nombre de dés adverses en fonction de la valeur du slider et récupère son résultat
        //Si le dé part de droite
        dice4.SetActive(true);
        if(startDroite == true) {
            r4 = value24.value();
        }
        else {
            r4 = value14.value();
        }
        resultatAttacked.Add((int)r4+1);

        //Si deux dés 
        if(nbAt == 2) {
            dice5.SetActive(true);
            if(startDroite == true) {
                r5 = value25.value();
            }
            else {
                r5 = value15.value();
            }
            resultatAttacked.Add((int)r5+1);
        }

        //Si trois dés 
        else if(nbAt == 3) {
            dice5.SetActive(true);
            dice6.SetActive(true);
            if(startDroite == true) {
                r5 = value25.value();
            }
            else {
                r5 = value15.value();
            }
            if(startDroite == true) {
                r6 = value26.value();
            }
            else {
                r6 = value16.value();
            }
            resultatAttacked.Add((int)r5+1);
            resultatAttacked.Add((int)r6+1);
        }

        resultatAttacked.Sort();
    }   

    //Couroutine qui après 5 secondes, désactive les dés et réinitialise leur position
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5.0f);

        dice1.SetActive(false);
        dice1.transform.position = new Vector2(8.9f, 6.83f);
        dice2.SetActive(false);
        dice2.transform.position = new Vector2(8.9f, 6.83f);
        dice3.SetActive(false);
        dice3.transform.position = new Vector2(8.9f, 6.83f);
    }   

    //Couroutine qui après 10 secondes, désactive les dés adverses et réinitialise leur position, et affiche les images et valeurs de fin de baston
    IEnumerator wait2(List<int> resultat, List<int> resultatAttacked)
    {
        yield return new WaitForSeconds(10.0f);

        dice4.SetActive(false);
        dice4.transform.position = new Vector2(-8.9f, 6.83f);
        dice5.SetActive(false);
        dice5.transform.position = new Vector2(-8.9f, 6.83f);
        dice6.SetActive(false);
        dice6.transform.position = new Vector2(-8.9f, 6.83f);

        //Calcul pour savoir si c'est une victoire ou une défaite
        int res = 0;
        int resAttacked = 0;

        if (resultat[nbDe-1] >= resultatAttacked[nbAt-1])
        {
            resAttacked++;
        }
        else
        {
            res++;
        }

        if (nbAt == 2 && nbDe == 3)
        {
            if (resultat[1] >= resultatAttacked[0])
            {
                resAttacked++;
            }
            else
            {
                res++;
            }
        }
        else if (nbAt == 2 && nbDe == 2)
        {
            if (resultat[0] >= resultatAttacked[0])
            {
                resAttacked++;
            }
            else
            {
                res++;
            }
        }

        //Applique le résultat sur les territoires 
        bool fin = CountryManager.instance.ResAttaque(res,resAttacked);

        resAttackCanva.gameObject.SetActive(true);
        resAttackedCanva.gameObject.SetActive(true);
        resBastonCanva.gameObject.SetActive(true);
        bastonPanel bastonPanel = resBastonCanva.GetComponent<bastonPanel>();

        string s1 = "";
        string s2 = "";
        if(res < resAttacked) {
            s1 = Country.theTribes[CountryManager.instance.TourJoueur];
            s2 = CountryManager.instance.countrySelectedAttacked.country.tribe;
            if (Country.theTribes.Count > 4)
            {
                s1 = Country.theTribes[CountryManager.instance.TourJoueur + 4];
                s2 = Country.theTribes[Country.theTribes.IndexOf(CountryManager.instance.countrySelectedAttacked.country.tribe) + 4];
            }
            
            bastonPanel.infoBaston.text = "Le joueur " + s1 + " a gagné la baston !";
            bastonPanel.infoPertes.text = "Le joueur " + s2 +  " perd " + resAttacked + " troupes";
        }
        else if (res == resAttacked)
        {
            bastonPanel.infoBaston.text = "Egalité !";
            bastonPanel.infoPertes.text = "Les deux joueurs perdent une troupe";
        }
        else {
            s1 = Country.theTribes[CountryManager.instance.TourJoueur];
            s2 = CountryManager.instance.countrySelectedAttacked.country.tribe;
            if (Country.theTribes.Count > 4)
            {
                s1 = Country.theTribes[CountryManager.instance.TourJoueur + 4];
                s2 = Country.theTribes[Country.theTribes.IndexOf(CountryManager.instance.countrySelectedAttacked.country.tribe) + 4];
            }
            
            bastonPanel.infoBaston.text = "Le joueur " + s2 + " a gagné la baston !";
            bastonPanel.infoPertes.text = "Le joueur " + s1 + " perd " + res + " troupes";
        }

        if (fin)
        {
            GameManager.instance.battleWon = true;
        }
        else
        {
            GameManager.instance.battleWon = false;
        }

        resBastonPanel resBastonPanel = resAttackCanva.GetComponent<resBastonPanel>();
        resBastonPanel resBastonPanel2 = resAttackedCanva.GetComponent<resBastonPanel>();
        string resValue = "";
        string resValueAtt = "";
        for(int i = 0; i < resultat.Count; i++) {
            resValue += "= " + resultat[i] + "\n";
        }
        for(int i = 0; i < resultatAttacked.Count; i++) {
            resValueAtt += "= " + resultatAttacked[i] + "\n";
        }
        resBastonPanel.resDes.text = resValue;
        resBastonPanel2.resDes.text = resValueAtt;
    }   

    IEnumerator resBattle(List<int> resultat, List<int> resultatAttacked) {
        yield return new WaitForSeconds(17.0f);

        jetDes.SetActive(false);
        jetBVJ.SetActive(false);
        jetBVV.SetActive(false);
        jetJVR.SetActive(false);
        jetVVJ.SetActive(false);
        jetVVR.SetActive(false);

        resAttackCanva.gameObject.SetActive(false);
        resAttackedCanva.gameObject.SetActive(false);
        resBastonCanva.gameObject.SetActive(false);

        menu.SetActive(true);
        passer.SetActive(true);

        resultat.Clear();
        resultatAttacked.Clear();

        GameManager.instance.battleHasEnded = true;
        CountryManager.instance.finFight();
    }
}
