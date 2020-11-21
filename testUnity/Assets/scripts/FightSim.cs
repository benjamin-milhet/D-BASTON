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

    //Dés 
    public GameObject dice;
    private GameObject dice2;
    private GameObject dice3;
    private GameObject dice7;

    //Dés adverses 
    public GameObject dice4;
    private GameObject dice5;
    private GameObject dice6;
    private GameObject dice8;

    //Valeur renvoyée par les dés 
    private static double r = 0;
    private static double r2 = 0;
    private static double r3 = 0;

    //Valeur renvoyée par les dés adverses
    private static double r4 = 0;
    private static double r5 = 0;
    private static double r6 = 0;

    //Variables permettant de récupérer une instance de dé
    private Dice value;
    private Dice value2;
    private Dice value3;

    //Variables permettant de récupérer une instance de dé adverse
    private Dice2 value4;
    private Dice2 value5;
    private Dice2 value6;

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

    public GameObject menu;
    public GameObject passer;
    public GameObject jetDes;

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

        int tourJoueur = CountryManager.instance.TourJoueur;

        if(tourJoueur == 0) {
            nbDe = this.valueDe;
            nbAt = this.valueDeAttacked;
        }
        else {
            nbDe = this.valueDeAttacked;
            nbAt = this.valueDe;
        }
    } 

    //Méthode qui gère les dés et le résultat
    void Update() {
        if(active) {

            dice2 = Instantiate<GameObject>(dice);
            dice3 = Instantiate<GameObject>(dice);
            dice7 = Instantiate<GameObject>(dice);

            dice5 = Instantiate<GameObject>(dice4);
            dice6 = Instantiate<GameObject>(dice4);
            dice8 = Instantiate<GameObject>(dice4);

            int tourJoueur = CountryManager.instance.TourJoueur;

            List<int> resultat = new List<int>();
            List<int> resultatAttacked = new List<int>();

            passer.SetActive(false);
            jetDes.SetActive(true);

            //Instancie le nombre de dés en fonction de la valeur du slider et récupère son résultat
            if(tourJoueur == 0) {
                dice7.SetActive(true);
            }
            value = dice7.GetComponent<Dice>();
            r = value.value();
            resultat.Add((int)r+1);

            if(nbDe == 2) {
                if(tourJoueur == 0) {
                    dice2.SetActive(true);
                }   
                value2 = dice2.GetComponent<Dice>();
                r2 = value2.value();
                resultat.Add((int)r2+1);
            }
            else if(nbDe == 3) {
                if(tourJoueur == 0) {
                    dice2.SetActive(true);
                    dice3.SetActive(true);
                } 
                value2 = dice2.GetComponent<Dice>();
                r2 = value2.value();
                value3 = dice3.GetComponent<Dice>();
                r3 = value3.value();
                resultat.Add((int)r2+1);
                resultat.Add((int)r3+1);
            }


            if(tourJoueur == 1) {
                menu.SetActive(false);
                dice8.SetActive(true);
            }
            value4 = dice8.GetComponent<Dice2>();
            r4 = value4.value();
            resultatAttacked.Add((int)r4+1);

            if(nbAt == 2) {
                if(tourJoueur == 1) {
                    dice5.SetActive(true);
                }
                value5 = dice5.GetComponent<Dice2>();
                r5 = value5.value();
                resultatAttacked.Add((int)r5+1);
            }
            else if(nbAt == 3) {
                if(tourJoueur == 1) {
                    dice5.SetActive(true);
                    dice6.SetActive(true);
                }
                value5 = dice5.GetComponent<Dice2>();
                r5 = value5.value();
                value6 = dice6.GetComponent<Dice2>();
                r6 = value6.value();
                resultatAttacked.Add((int)r5+1);
                resultatAttacked.Add((int)r6+1);
            }

            coroutine4 = jet2(tourJoueur);
            StartCoroutine(coroutine4);

            resultat.Sort();
            resultatAttacked.Sort();

            //Démarre les 2 couroutines 
            coroutine = wait(tourJoueur);
            StartCoroutine(coroutine);
            coroutine2 = wait2(tourJoueur);
            StartCoroutine(coroutine2);
            coroutine3 = battle(resultat, resultatAttacked);
            StartCoroutine(coroutine3);

            //Les territoires redevienent actifs 
            active = false;
        }
    }

    //Couroutine qui après 5 secondes, lance les dés adverses
    IEnumerator jet2(int TJ)
    {
        yield return new WaitForSeconds(5.0f);

        //Instancie le nombre de dés adverses en fonction de la valeur du slider et récupère son résultat
        if(TJ == 0) {
            menu.SetActive(false);
            dice8.SetActive(true);

            if(nbAt == 2) {
                dice5.SetActive(true);
            }
            else if(nbAt == 3) {
                dice5.SetActive(true);
                dice6.SetActive(true);
            }
        }
        else {
            dice7.SetActive(true);

            if(nbDe == 2) {
                dice2.SetActive(true);
            }
            else if(nbDe == 3) {
                dice2.SetActive(true);
                dice3.SetActive(true);
            }
        }
    }   

    //Couroutine qui après 5 secondes, désactive les dés et réinitialise leur position
    IEnumerator wait(int TJ)
    {
        yield return new WaitForSeconds(5.0f);

        if(TJ == 0) {
            dice7.SetActive(false);
            dice7.transform.position = new Vector2(8.9f, 6.83f);
            dice2.SetActive(false);
            dice2.transform.position = new Vector2(8.9f, 6.83f);
            dice3.SetActive(false);
            dice3.transform.position = new Vector2(8.9f, 6.83f);
        }
        else {
            dice8.SetActive(false);
            dice8.transform.position = new Vector2(-8.9f, 6.83f);
            dice5.SetActive(false);
            dice5.transform.position = new Vector2(-8.9f, 6.83f);
            dice6.SetActive(false);
            dice6.transform.position = new Vector2(-8.9f, 6.83f);
            menu.SetActive(true);
        }
    }   

    //Couroutine qui après 10 secondes, désactive les dés adverses et réinitialise leur position
    IEnumerator wait2(int TJ)
    {
        yield return new WaitForSeconds(10.0f);

        if(TJ == 0) {
            dice8.SetActive(false);
            dice8.transform.position = new Vector2(-8.9f, 6.83f);
            dice5.SetActive(false);
            dice5.transform.position = new Vector2(-8.9f, 6.83f);
            dice6.SetActive(false);
            dice6.transform.position = new Vector2(-8.9f, 6.83f);
            menu.SetActive(true);
        }
        else {
            dice7.SetActive(false);
            dice7.transform.position = new Vector2(8.9f, 6.83f);
            dice2.SetActive(false);
            dice2.transform.position = new Vector2(8.9f, 6.83f);
            dice3.SetActive(false);
            dice3.transform.position = new Vector2(8.9f, 6.83f);
        }

        passer.SetActive(true);
        jetDes.SetActive(false);
    }   

    //Couroutine qui calcul les points en moins des deux territoires 
    IEnumerator battle(List<int> resultat, List<int> resultatAttacked)
    {
        yield return new WaitForSeconds(10.0f);
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

        if (fin)
        {
            GameManager.instance.battleWon = true;
        }
        else
        {
            GameManager.instance.battleWon = false;
        }
        GameManager.instance.battleHasEnded = true;
        CountryManager.instance.finFight();
    }   
}
