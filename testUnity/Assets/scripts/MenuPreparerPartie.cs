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
    public Dropdown choixMap;

    /// <summary>
    /// Lance la partie avec le bon nombre de joueur
    /// </summary>
    public void chargerPartie()
    {
        this.Saving();
        
        switch (choixMap.value)
        {
            case 0 :
                SceneManager.LoadScene(1);
                break;
            case 1 :
                SceneManager.LoadScene(2);
                break;
            default:
                SceneManager.LoadScene(1);
                break;
        }
        
    }

    /// <summary>
    ///Quitte le jeu
    /// </summary>
    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    
    /// <summary>
    /// Donnee a sauvegarder entre 2 scenes
    /// </summary>
    [System.Serializable]
    public class SaveData
    {
        public int nbJoueur;
    }
    
    /// <summary>
    /// Permet de sauvegarder dans un fichier json le nombre de joueur
    /// </summary>
    public void Saving()
    {
        SaveData data = new SaveData();

        //On recupere le nombre de joueur depuis le dropdown
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

    }

    /// <summary>
    /// Permet de supprimer le fichier json
    /// </summary>
    public static void DeleteSaveFile()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFileMenu.json"))
        {
            File.Delete(Application.persistentDataPath + "/SaveFileMenu.json");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
