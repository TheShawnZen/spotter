using System;
using System.Collections.Generic;
using System.Text;

namespace SpotterClasses
{
    public class Activite
    {
        private int id, membre_id, categorie_id;
        private string nom, lieu, dsc, motdepasse;
        private string dateheure;

        public int Id { get => id; set => id = value; }
        public int Membre_id { get => membre_id; set => membre_id = value; }
        public int Categorie_id { get => categorie_id; set => categorie_id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Lieu { get => lieu; set => lieu = value; }
        public string Dsc { get => dsc; set => dsc = value; }
        public string Motdepasse { get => motdepasse; set => motdepasse = value; }
        public string Dateheure { get => dateheure; set => dateheure = value; }
    }
}
