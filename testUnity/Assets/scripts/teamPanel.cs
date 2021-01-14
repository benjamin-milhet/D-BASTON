using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{    
    /// <summary>
    /// Panel qui affiche le nom des equipes, leur nombre de territoire et de troupe ainsi qu'un fond qui correspond a sa couleur
    /// </summary>
    public class teamPanel : MonoBehaviour
    {
        public Text nomTeam;//Nom de l'equipe du joueur 
        public Text nbTroupe;//Son nombre de troupe
        public Text nbCountry;//Son nombre de territoire
        public Image bgcolor;//Sa couleur attitré
    }
}