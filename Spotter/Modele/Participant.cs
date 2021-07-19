using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spotter.Modele
{
    public class Participant
    {
        private int id, membre_id, activite_id;

        [Required]
        public int Id { get => id; set => id = value; }


        public int Membre_id { get => membre_id; set => membre_id = value; }


        public int Activite_id { get => activite_id; set => activite_id = value; }
    }
}