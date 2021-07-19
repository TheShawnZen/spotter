using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spotter.Providers;
using Spotter.Modele;


namespace Spotter.Services
{
    public class CategorieServices
    {
        public static bool Ajouter(Categorie categorie)
        {
            return CategorieProvider.Creer(categorie);
        }
        public static bool SupprimerParId(int id)
        {
            return CategorieProvider.SupprimerById(id);
        }
        public static List<Categorie> GetAll()
        {
            return CategorieProvider.FindAll();
        }
        public static Categorie GetById(int id)
        {
            return CategorieProvider.FindById(id);
        }
    }
}