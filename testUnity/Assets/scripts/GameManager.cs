using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string attackedCountry;

    public bool battleHasEnded;
    public bool battleWon;

    public int exp;
    public int money;

    [System.Serializable]
    public class SaveData
    {
        
        private List<Country> savedCountry = new List<Country>();
        public int cur_money;
        public int cur_exp;

        public List<Country> SavedCountry { get => savedCountry; set => savedCountry = value; }
    }
    
    void Start()
    {
    }

    void Awake()
    {
        //DeleteSaveFile();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Saving()
    {
        
        SaveData data = new SaveData();
        for (int i = 0; i < CountryManager.instance.countryList.Count; i++)
        {
            data.SavedCountry.Add(CountryManager.instance.countryList[i].GetComponent<CountryHandler>().country);
        }
        //money and exp
        data.cur_exp = exp;
        data.cur_money = money;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFile.json", FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
        print("Saved Game");

    }

    public void Loading()
    {
        
        if (File.Exists(Application.persistentDataPath + "/SaveFile.json"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/SaveFile.json",FileMode.Open) ;

            SaveData data = (SaveData)bf.Deserialize(stream);
            stream.Close();

            for (int i = 0; i < data.SavedCountry.Count; i++)
            {
                for (int j = 0; j < CountryManager.instance.countryList.Count; j++)
                {
                    if (data.SavedCountry[i].name == CountryManager.instance.countryList[j].GetComponent<CountryHandler>().country.name)
                    {
                        CountryManager.instance.countryList[j].GetComponent<CountryHandler>().country = data.SavedCountry[i];
                    }
                }
            }

            exp = data.cur_exp;
            money = data.cur_money;

            CountryManager.instance.TintCountries();
            print("Game loaded");
        }
        else
        {
            print("No SaveFile Found");
        }
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile.json"))
        {
            File.Delete(Application.persistentDataPath + "/SaveFile.json");
            print("SaveFile Delete");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       

        }
        else
        {
            print("No SaveFile Delete Found");
        }
    }


}
