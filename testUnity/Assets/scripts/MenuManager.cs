using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject menu;

    public List<GameObject> menuList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       AddMenuData(); 
    }

    private void Awake()
    {
        instance = this;
        
    }


    void AddMenuData()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Menu") as GameObject[];
        foreach(GameObject menu in theArray)
        {
            menuList.Add(menu);
        }
        TintMenu();
    }
    
    public void TintMenu()
    {

        for(int i = 0; i < menuList.Count; i++)
        {
            menuList[i].SetActive(true);
        }
        
    }
    
    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void DisableMenu()
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        menu.SetActive(false);
        GameManager.instance.DeleteSaveFile();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
