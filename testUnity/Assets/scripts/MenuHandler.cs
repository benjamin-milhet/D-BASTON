using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(PolygonCollider2D))]
public class MenuHandler : MonoBehaviour
{
    public string name;
    void OnMouseUpAsButton()
    {
        
        if (name == "menu")
        {
            MenuManager.instance.ShowMenu();
        }
        else if (name == "passer")
        {
            MenuManager.instance.NextAction();
        }
        else if (name == "carte")
        {
            MenuManager.instance.ShowMenuCarte();
        }
        
    }
    
    private void OnDrawGizmos()
    {
        this.name = name;
        this.tag = "Menu";
    }
}
