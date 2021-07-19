using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spotter.Providers;
using Spotter.Modele;

namespace Spotter.Services
{
    public class ActiviteServices
    {
        public static bool Ajouter(Activite activite)
        {
            return ActiviteProvider.Creer(activite);
        }
        public static bool SupprimerParId(int id)
        {
            return ActiviteProvider.SupprimerById(id);
        }
        public static List<Activite> GetAll()
        {
            return ActiviteProvider.FindAll();
        }
        public static Activite GetById(int id)
        {
            return ActiviteProvider.FindById(id);
        }
        public static bool EditById(Activite ac)
        {
            return ActiviteProvider.EditById(ac);
        }
        public static List<Activite> GetByActivitePublic()
        {
            return ActiviteProvider.FindPublic();
        }
        public static List<Activite> GetByMemberId(int id)
        {
            return ActiviteProvider.FindByMemberId(id);
        }
        public static List<Activite> GetByParticipantId(int id)
        {
            return ActiviteProvider.FindByParticipantId(id);
        }
        public static Activite GetByParticipantIdActiviteId(int pid, int aid)
        {
            return ActiviteProvider.FindByParticipantIdActiviteId(pid, aid);
        }
        public static Activite GetByMotDePasse(string mdp)
        {
            return ActiviteProvider.FindByMdp(mdp);
        }
        public static List<Activite> GetByCategorieId(int id)
        {
            return ActiviteProvider.FindByCategorieId(id);
        }
    }
}