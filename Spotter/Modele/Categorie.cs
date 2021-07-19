using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spotter.Modele
{
    public class Categorie
    {
        private int id;
        private string nom;

        [Required]
        public int Id { get => id; set => id = value; }

        public string Nom { get => nom; set => nom = value; }

        public IEnumerable<SelectListItem> CategorieList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = this.Id+"", Value = this.Nom}
                };
            }
        }
    }
}