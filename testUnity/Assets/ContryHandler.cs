using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]

public class ContryHandler : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Color32 oldColor;
    public Color32 hoverColor;
    public Color32 startColor;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = startColor;
    }


    private void OnMouseEnter()
    {
        oldColor = sprite.color;
        sprite.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sprite.color = oldColor;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
