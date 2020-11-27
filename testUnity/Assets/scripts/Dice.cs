using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    //Dé
    public GameObject dice;

    //Gère les sprites et les faces
    private Sprite[] diceSides;
    private SpriteRenderer rend;

    //Variables des faces du dé
    private int r1;
    private int r2;
    private int r3;
    private int r4;
    private int r5;
    private int r6;
    private int r7;
    private int r8;

    //Variable qui permet de renvoyer la valeur finale du dé
    private static int aled;

    //Position originale du dé
    private static Vector2 originalPos;

    private void Awake() {

        //Initialisation des valeurs du dé
        r1 = Random.Range(0, 5);
        r2 = Random.Range(0, 5);
        r3 = Random.Range(0, 5);
        r4 = Random.Range(0, 5);
        r5 = Random.Range(0, 5);
        r6 = Random.Range(0, 5);
        r7 = Random.Range(0, 5);
        r8 = Random.Range(0, 5);

        aled = r7;
    }
    
    //Start is called before the first frame update
    private void Start () {

        //Initialisation des sprites pour chaque face depuis le dossier "Dice" dans les ressources
        rend = GetComponent<SpriteRenderer>();
        
        if(dice.name.Contains("R")) {
            diceSides = Resources.LoadAll<Sprite>("Dice/");
        }  
        else if(dice.name.Contains("B")) {
            diceSides = Resources.LoadAll<Sprite>("Dice2/");
        }
        else if(dice.name.Contains("V")) {
            diceSides = Resources.LoadAll<Sprite>("Dice3/");
        }
        else if(dice.name.Contains("J")) {
            diceSides = Resources.LoadAll<Sprite>("Dice4/");
        }

        originalPos = new Vector2(transform.position.x, transform.position.y);
    }

    //Update is called once per frame
    private void Update()
    {
        //Réinitialisation de la rotation à chaque frame
        transform.rotation = new Quaternion (0, 0, 0, 0);

        //Valeurs de vitesse de l'animation
        float speedX = 7.5f;
        float speedY = 7.5f;

        //Conditions de direction de déplacement en fonction de la position actuelle 
        if (transform.position.x <= 4.94 && transform.position.x >= 3.5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r1];
        }
        else if (transform.position.x <= 3.5 && transform.position.x >= -1.50)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r2];
        }
        else if (transform.position.x <= -1.50 && transform.position.x >= -3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r3];
        }
        else if (transform.position.x <= -3 && transform.position.x >= -3.3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r4];
        }
        else if (transform.position.x <= -3.3 && transform.position.x >= -3.6)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r5];
        }
        else if (transform.position.x <= -3.6 && transform.position.x >= -3.9)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r6];
        }
        else if (transform.position.x <= -3.9)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r7];
        }
        else { 
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedY, GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, GetComponent<Rigidbody2D>().velocity.x);
            rend.sprite = diceSides[r8];
        }
    }

    //Getter de la valeur finale du dé
    public int value() {
        return aled;
    }
}
