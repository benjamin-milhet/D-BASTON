using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


    public class Partie
    {
        private int nbJoueur;
        private List<Joueur> listJoueurs;
        public Partie(int nbJoueur)
        {
            this.nbJoueur = nbJoueur;
            this.listJoueurs = new List<Joueur>();
            this.Initialisation();
        }

        public List<Joueur> ListJoueurs { get => listJoueurs; set => listJoueurs = value; }

        public void Initialisation()
        {
            for (int i = 0; i < nbJoueur; i++)
            {
                this.listJoueurs.Add(new Joueur(i, "test",(Country.theTribes)i));
            }
            
        }

       
    }

