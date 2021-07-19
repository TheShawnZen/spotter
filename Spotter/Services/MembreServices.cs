using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spotter.Providers;
using Spotter.Modele;


namespace Spotter.Services
{
    public class MembreServices
    {
        public static bool Ajouter(Membre membre)
        {
            return MembreProvider.Creer(membre);
        }
        public static bool SupprimerParId(int id)
        {
            return MembreProvider.SupprimerById(id);
        }
        public static List<Membre> GetAll()
        {
            return MembreProvider.FindAll();
        }
        public static Membre GetById(int id)
        {
            return MembreProvider.FindById(id);
        }
        public static Membre GetByUserPass(string user, string pass)
        {
            return MembreProvider.FindByUserPass(user, pass);
        }
    }
}
