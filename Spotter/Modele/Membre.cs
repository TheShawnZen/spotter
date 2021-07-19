using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spotter.Modele
{
    public class Membre
    {
        private int id;
        private string email, username, motdepasse;

        [Required]
        public int Id { get => id; set => id = value; }
        
        [Display(Name = "Courriel")]
        public string Email { get => email; set => email = value; }
        
        [Required(ErrorMessage = "Mauvais nom d'utilisateur fourni.")]
        [Display(Name = "Nom d'utilisateur")]
        public string Username { get => username; set => username = value; }
        
        [Required(ErrorMessage = "Mauvais mot de passe fourni.")]
        [Display(Name = "Mot de passe")]
        public string Motdepasse { get => motdepasse; set => motdepasse = value; }
    }
}