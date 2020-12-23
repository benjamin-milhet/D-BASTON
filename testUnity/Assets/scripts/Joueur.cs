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
        private string theTribe;

        public Joueur(int id, string name, string theTribes)
        {
            this.id = id;
            this.name = name;
            this.Territoire = new List<Country>();
            this.TheTribes = theTribes;
        }

        public List<Country> Territoire { get => territoire; set => territoire = value; }
        public string TheTribes { get => theTribe; set => theTribe = value; }
    }

