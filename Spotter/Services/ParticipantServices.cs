using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spotter.Providers;
using Spotter.Modele;


namespace Spotter.Services
{
    public class ParticipantServices
    {
        public static bool Ajouter(Participant participant)
        {
            return ParticipantProvider.Creer(participant);
        }
        public static bool SupprimerParId(int id)
        {
            return ParticipantProvider.SupprimerById(id);
        }
        public static List<Participant> GetAll()
        {
            return ParticipantProvider.FindAll();
        }
        public static Participant GetById(int id)
        {
            return ParticipantProvider.FindById(id);
        }
        public static List<Participant> GetByActiviteId(int id)
        {
            return ParticipantProvider.FindByActiviteId(id);
        }
        public static bool SupprimerByParticipantIdActiviteId(int pid, int aid)
        {
            return ParticipantProvider.SupprimerParParticipantIdActiviteId(pid, aid);
        }
        public static Participant GetByActiviteMembreId(int activite_id, int membre_id)
        {
            return ParticipantProvider.FindByActiviteMembreId(activite_id,membre_id);
        }
    }
}
