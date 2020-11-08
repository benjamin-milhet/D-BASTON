using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Joueur
    {
        private int id;
        private String name;
        private List<Country> territoire;
        private Country.theTribes theTribe;

        public Joueur(int id, string name, Country.theTribes theTribes)
        {
            this.id = id;
            this.name = name;
            this.Territoire = new List<Country>();
            this.TheTribes = theTribes;
        }

        public List<Country> Territoire { get => territoire; set => territoire = value; }
        public Country.theTribes TheTribes { get => theTribe; set => theTribe = value; }
    }

