using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using TMPro;
using Toggle = UnityEngine.UI.Toggle;

public class MenuPreparerPartie : MonoBehaviour
{
    public Dropdown nbJoueur;
    public Dropdown choixMap;
    public Dropdown choixNbTroupe;
    public Dropdown choixNbAttaque;
    public Dropdown choixMode;
    public Slider volumeMusique;
    public AudioSource audioMusique;
    
    public TextMeshProUGUI textBonus;
    public Toggle toggleBonus;
    
    public TextMeshProUGUI joueur3;
    public TextMeshProUGUI joueur4;

    public TMP_InputField inputJ1;
    public TMP_InputField inputJ2;
    public TMP_InputField inputJ3;
    public TMP_InputField inputJ4;

    public GameObject menuPreparerPartie;
    public GameObject menuChangerPseudo;
    public GameObject mainMenu;
    
    private List<string> tribe = new List<string>();

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
            case 2 :
                SceneManager.LoadScene(3);
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
        public List<string> tribe;
        public int nbJoueur;
        public int map;
        public int nbTroupe;
        public int nbAttaque;
        public int choixMode;
        public int isBonus = 0;
    }
    
    /// <summary>
    /// Permet de sauvegarder dans un fichier json le nombre de joueur
    /// </summary>
    public void Saving()
    {
        SaveData data = new SaveData();

        //On recupere le nombre de joueur depuis le dropdown
        switch (this.nbJoueur.value)
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
        
        //On recupere la map depuis le dropdown
        switch (this.choixMap.value)
        {
            case 0 :
                data.map = 0;
                break;
            case 1 :
                data.map = 1;
                break;
            case 2 :
                data.map = 2;
                break;
            default:
                data.map = 0;
                break;
        }
        
        //On recupere le nombre de troupe depuis le dropdown
        switch (this.choixNbTroupe.value)
        {
            case 0 :
                data.nbTroupe = 10;
                break;
            case 1 :
                data.nbTroupe = 20;
                break;
            case 2 :
                data.nbTroupe = 30;
                break;
            case 3 :
                data.nbTroupe = 40;
                break;
            default:
                data.nbTroupe = 30;
                break;
        }

        data.tribe = this.tribe;
        
        //On recupere le nombre de combat depuis le dropdown
        switch (this.choixNbAttaque.value)
        {
            case 0 :
                data.nbAttaque = 3;
                break;
            case 1 :
                data.nbAttaque = 4;
                break;
            case 2 :
                data.nbAttaque = 5;
                break;
            case 3 :
                data.nbAttaque = 6;
                break;
            default:
                data.nbAttaque = 4;
                break;
        }
        
        //On recupere le mode de jeu depuis le dropdown
        switch (this.choixMode.value)
        {
            case 0 :
                data.choixMode = 1;
                break;
            case 1 :
                data.choixMode = 2;
                break;
            case 2 :
                data.nbAttaque = 3;
                break;
            default:
                data.nbAttaque = 1;
                break;
        }

        if (this.toggleBonus.IsActive())
        {
            if (this.toggleBonus.isOn)
            {
                data.isBonus = 1;
            }
            else
            {
                data.isBonus = 0;
            }
        }
        else
        {
            data.isBonus = 0;
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

    public void SetVolumeMusique()
    {
        this.audioMusique.volume = this.volumeMusique.value;
    }

    public void changerNom()
    {
        this.menuChangerPseudo.SetActive(true);
        
        int nbJoueur;
        //On recupere le nombre de joueur depuis le dropdown
        switch (this.nbJoueur.value)
        {
            case 0 :
                nbJoueur = 2;
                break;
            case 1 :
                nbJoueur = 3;
                break;
            case 2 :
                nbJoueur = 4;
                break;
            default:
                nbJoueur = 2;
                break;
        }

        this.inputJ1.text = this.tribe[0];
        this.inputJ2.text = this.tribe[1];
        this.inputJ3.text = this.tribe[2];
        this.inputJ4.text = this.tribe[3];

        if (nbJoueur == 3)
        {
            this.joueur3.gameObject.SetActive(true);
            this.inputJ3.gameObject.SetActive(true);
        }
        if(nbJoueur == 4)
        {
            this.joueur3.gameObject.SetActive(true);
            this.inputJ3.gameObject.SetActive(true);
            this.joueur4.gameObject.SetActive(true);
            this.inputJ4.gameObject.SetActive(true);
        }
        
    }

    public void RetourJoueur()
    {
        this.joueur3.gameObject.SetActive(false);
        this.inputJ3.gameObject.SetActive(false);
        this.joueur4.gameObject.SetActive(false);
        this.inputJ4.gameObject.SetActive(false);
        
        this.menuChangerPseudo.SetActive(false);
    }

    public void LoadMenuPreparerPartie()
    {
        this.textBonus.gameObject.SetActive(false);
        this.toggleBonus.gameObject.SetActive(false);
        
        this.mainMenu.SetActive(false);
        this.menuPreparerPartie.SetActive(true);
        
        this.tribe = new List<string>();
        this.tribe.Add("Allemagne");
        this.tribe.Add("France");
        this.tribe.Add("Angleterre");
        this.tribe.Add("Espagne");
    }

    public void changerMap()
    {
        this.tribe = new List<string>();
        switch (this.choixMap.value)
        {
            case 0:
                this.tribe.Add("Allemagne");
                this.tribe.Add("France");
                this.tribe.Add("Angleterre");
                this.tribe.Add("Espagne");
                break;
            case 1:
                this.tribe.Add("Empire");
                this.tribe.Add("Rebelle");
                this.tribe.Add("Mandalorien");
                this.tribe.Add("Jedi");
                break;
            case 2:
                this.tribe.Add("Empire");
                this.tribe.Add("Rebelle");
                this.tribe.Add("Mandalorien");
                this.tribe.Add("Jedi");
                break;
            default:
                this.tribe.Add("Allemagne");
                this.tribe.Add("France");
                this.tribe.Add("Angleterre");
                this.tribe.Add("Espagne");
                break;
        }
    }

    public void ValiderPseudo()
    {
        this.joueur3.gameObject.SetActive(false);
        this.inputJ3.gameObject.SetActive(false);
        this.joueur4.gameObject.SetActive(false);
        this.inputJ4.gameObject.SetActive(false);
        
        this.menuChangerPseudo.SetActive(false);
        
        this.tribe.Add(this.inputJ1.text);
        this.tribe.Add(this.inputJ2.text);
        this.tribe.Add(this.inputJ3.text);
        this.tribe.Add(this.inputJ4.text);

    }

    public void switchDropdown()
    {
        switch (this.choixMap.value)
        {
            case 0:
                this.textBonus.gameObject.SetActive(false);
                this.toggleBonus.gameObject.SetActive(false);
                this.choixMode.options.Clear();
                Dropdown.OptionData info1 = new Dropdown.OptionData();
                info1.text = "Standard";
                Dropdown.OptionData info11 = new Dropdown.OptionData();
                info11.text = "Cartes";
                this.choixMode.options.Add(info1);
                this.choixMode.options.Add(info11);
                this.choixMode.RefreshShownValue();
                this.choixMap.RefreshShownValue();
                break;
            case 1:
                this.choixMode.options.Clear();
                Dropdown.OptionData info2 = new Dropdown.OptionData();
                info2.text = "Standard";
                Dropdown.OptionData info22 = new Dropdown.OptionData();
                info22.text = "Cartes";
                this.choixMode.options.Add(info2);
                this.choixMode.options.Add(info22);
                this.textBonus.gameObject.SetActive(true);
                this.toggleBonus.gameObject.SetActive(true);
                this.choixMode.RefreshShownValue();
                this.choixMap.RefreshShownValue();
                break;
            case 2:
                this.textBonus.gameObject.SetActive(false);
                this.toggleBonus.gameObject.SetActive(false);
                this.choixMode.options.Clear();
                Dropdown.OptionData info4 = new Dropdown.OptionData();
                info4.text = "Conquête";
                this.choixMode.options.Add(info4);
                this.choixMode.RefreshShownValue();
                this.choixMap.RefreshShownValue();
                break;
            default:
                this.textBonus.gameObject.SetActive(false);
                this.toggleBonus.gameObject.SetActive(false);
                this.choixMode.options.Clear();
                Dropdown.OptionData info3 = new Dropdown.OptionData();
                info3.text = "Standard";
                Dropdown.OptionData info33 = new Dropdown.OptionData();
                info33.text = "Cartes";
                this.choixMode.options.Add(info3);
                this.choixMode.options.Add(info33);
                this.choixMode.RefreshShownValue();
                this.choixMap.RefreshShownValue();
                break;
        } 
    }
    public void switchDropdownParRapportAuNbJoueur()
    {
        Dropdown.OptionData info3 = new Dropdown.OptionData();
        Dropdown.OptionData info33 = new Dropdown.OptionData();
        switch (this.nbJoueur.value)
        {
            case 0:
                this.choixMap.options.Clear();
                Dropdown.OptionData info1 = new Dropdown.OptionData();
                info1.text = "Europe";
                Dropdown.OptionData info11 = new Dropdown.OptionData();
                info11.text = "Star Wars";
                Dropdown.OptionData info111 = new Dropdown.OptionData();
                info111.text = "Star Wars Conquête";
                this.choixMap.options.Add(info1);
                this.choixMap.options.Add(info11);
                this.choixMap.options.Add(info111);
                this.choixMap.RefreshShownValue();
                break;
            case 1:
                this.choixMap.options.Clear();
                info3 = new Dropdown.OptionData();
                info33 = new Dropdown.OptionData();
                info3.text = "Europe";
                info33.text = "Star Wars";
                this.choixMap.options.Add(info3);
                this.choixMap.options.Add(info33);
                this.choixMap.RefreshShownValue();
                break;
            case 2:
                this.choixMap.options.Clear();
                info3 = new Dropdown.OptionData();
                info33 = new Dropdown.OptionData();
                info3.text = "Europe";
                info33.text = "Star Wars";
                this.choixMap.options.Add(info3);
                this.choixMap.options.Add(info33);
                this.choixMap.RefreshShownValue();
                break;
            case 3:
                this.choixMap.options.Clear();
                info3 = new Dropdown.OptionData();
                info33 = new Dropdown.OptionData();
                info3.text = "Europe";
                info33.text = "Star Wars";
                this.choixMap.options.Add(info3);
                this.choixMap.options.Add(info33);
                this.choixMap.RefreshShownValue();
                break;
            default:
                this.choixMap.options.Clear();
                info3 = new Dropdown.OptionData();
                info33 = new Dropdown.OptionData();
                info3.text = "Europe";
                info33.text = "Star Wars";
                this.choixMap.options.Add(info3);
                this.choixMap.options.Add(info33);
                this.choixMap.RefreshShownValue();
                
                break;
        }

        this.choixMap.value = 0;
        this.choixMap.RefreshShownValue();
        this.switchDropdown();
    }
    
}
