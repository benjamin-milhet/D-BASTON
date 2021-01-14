using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de gerer l'opacité du fond de la carte europe
/// </summary>
public class opacity : MonoBehaviour
{
    public float alphaLevel = .07f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer> ().color = new Color (1,1,1,alphaLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
