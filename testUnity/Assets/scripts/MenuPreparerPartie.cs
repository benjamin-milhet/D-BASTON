using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuPreparerPartie : MonoBehaviour
{
    public Dropdown nbJoueur;

    public void chargerPartie()
    {
        this.Saving();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
    [System.Serializable]
    public class SaveData
    {
        public int nbJoueur;
    }
    
    public void Saving()
    {
        SaveData data = new SaveData();

        switch (nbJoueur.value)
        {
            case 0 :
                data.nbJoueur = 2;
                break;
            case 1 :
                data.nbJoueur = 3;
                break;
            case 2 :
                data.nbJoueur = 4;
                break;
            default:
                data.nbJoueur = 2;
                break;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFileMenu.json", FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
        print("Saved Game");

    }

    public static void DeleteSaveFile()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFileMenu.json"))
        {
            File.Delete(Application.persistentDataPath + "/SaveFileMenu.json");
            print("SaveFile Delete");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else
        {
            print("No SaveFileMenu Delete Found");
        }
    }
}
