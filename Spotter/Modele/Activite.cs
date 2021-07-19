using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spotter.Modele
{
    public class Activite
    {
        private int id, membre_id, categorie_id;
        private string nom, lieu, dsc, motdepasse;
        private string dateheure;

        [Required]
        public int Id { get => id; set => id = value; }

        public int Membre_id { get => membre_id; set => membre_id = value; }

        public int Categorie_id { get => categorie_id; set => categorie_id = value; }

        public string Nom { get => nom; set => nom = value; }

        public string Lieu { get => lieu; set => lieu = value; }

        [Display(Name = "Description")]
        public string Dsc { get => dsc; set => dsc = value; }

        [Display(Name = "Code d'accès")]
        public string Motdepasse { get => motdepasse; set => motdepasse = value; }

        public string Dateheure { get => dateheure; set => dateheure = value; }
    }
}