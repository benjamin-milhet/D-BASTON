using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    //Gère les sprites et les faces
    private Sprite[] diceSides;
    private SpriteRenderer rend;

    private int r1;
    private int r2;
    private int r3;
    private int r4;
    private int r5;
    private int r6;
    private int r7;
    private int r8;

    private static Vector2 originalPos;
    
    //Start is called before the first frame update
    private void Start () {

        //Initialisation des sprites pour chaque face depuis le dossier "Dice" dans les ressources
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("Dice/");

        //Assignation d'une valeur aléatoire correspondant à un sprite
        r1 = Random.Range(0, 5);
        r2 = Random.Range(0, 5);
        r3 = Random.Range(0, 5);
        r4 = Random.Range(0, 5);
        r5 = Random.Range(0, 5);
        r6 = Random.Range(0, 5);
        r7 = Random.Range(0, 5);
        r8 = Random.Range(0, 5);

        originalPos = new Vector2(transform.position.x, transform.position.y);
    }
    
    public double value() => r7;

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
}
