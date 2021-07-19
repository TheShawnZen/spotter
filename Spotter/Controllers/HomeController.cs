using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spotter.Services;
using Spotter.Modele;


namespace Spotter.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Accueil - Connexion";
			return View();
		}

		[HttpPost]
		public ActionResult Signup()
		{
			Membre membre = new Membre
			{
				Email = Request.Form["Email"],
				Username = Request.Form["Username"],
				Motdepasse = Request.Form["Motdepasse"]
			};
			if (membre.Email.Contains('@') == false)
			{
				ViewBag.Erreur = "L'email est invalide, veuillez réessayer.";
				return View("Inscription");
			}
			else if ((membre.Email).Length < 5)
			{
				ViewBag.Erreur = "L'email est invalide, veuillez réessayer.";
				return View("Inscription");
			}
			else if ((membre.Motdepasse).Length < 5)
			{
				ViewBag.Erreur = "Le mot de passe doit contenir au moins cinq (5) caractères.";
				return View("Inscription");
			}
			else if (Request.Form["confirmation"] != membre.Motdepasse)
			{
				ViewBag.Erreur = "Les deux mots de passe doivent être identiques.";
				return View("Inscription");
			}
			MembreServices.Ajouter(membre);
			return RedirectToAction("Index");
		}

		public ActionResult Logout()
		{
			Session["utilisateur"] = null;
			return RedirectToAction("Index");
		}

		public ActionResult SupprimerMembre(string id)
		{
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir supprimer votre compte."; return View("Index"); };
			MembreServices.SupprimerParId(Int32.Parse(id));
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";
			return View();
		}

		public ActionResult Inscription()
		{
			ViewBag.Message = "Inscription";
			return View();
		}

		[HttpPost]
		public ActionResult Connexion()
		{
			ViewBag.Erreur = "aucune";
			ViewBag.Message = "Page de connexion.";
			Membre membre = new Membre
			{
				Username = Request.Form["Username"],
				Motdepasse = Request.Form["Motdepasse"]
			};
			Membre utilisateur = MembreServices.GetByUserPass(membre.Username, membre.Motdepasse);
			if (utilisateur != null)
			{
				Session["utilisateur"] = utilisateur;
				return RedirectToAction("MenuAllActivite");
			}
			else
			{
				ViewBag.Erreur = "Le nom d'utilisateur ou le mot de passe est invalide.";
				return View("Index");
			}
		}

		public ActionResult GererActivites()
		{
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir gérer vos activités."; return View("Index"); };
			ViewBag.Message = "Page de gestion d'activités.";
			ViewBag.Activites = ActiviteServices.GetAll();
			return View();
		}

		public ActionResult ModifierActiviteGet()
		{
			ViewBag.Erreur = "aucune";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir modifier vos activités."; return View("Index"); };
			ViewBag.Message = "Page du formulaire de modification d'activité.";
			int parametreId;
			if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out parametreId))
				ViewBag.Activite = ActiviteServices.GetById(parametreId);
			else
				ViewBag.Erreur = "Aucun ID valide n'a été spécifié pour l'acquisition d'une activité.";
			return View();
		}

		[HttpPost]
		public ActionResult ModifierActivitePost()
		{
			ViewBag.Erreur = "aucune";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir modifier vos activités."; return View("Index"); };
			Activite activite = new Activite
			{
				Id = int.Parse(Request.Form["Id"]),
				Nom = Request.Form["Nom"],
				Categorie_id = int.Parse(Request.Form["categorieid"]),
				Dateheure = Request.Form["date"]+" "+ Request.Form["heure"]+":00",
				Lieu = Request.Form["Lieu"],
				Dsc = Request.Form["Dsc"],
				Motdepasse = Request.Form["Motdepasse"]
			};

			if (!ActiviteServices.EditById(activite))
			{
				ViewBag.Erreur = "La modification n'a pas été effectuée. Veuillez vérifier les paramètres et réessayer.";
			}
			ViewBag.Message = "Modifie les attributs d'une activité.";
			return RedirectToAction("GererActivites");
		}
		
		[HttpPost]
		public ActionResult AjouterActivite()
		{
			ViewBag.Erreur = "aucune";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir ajouter des activités."; return View("Index"); };

			bool val0 = int.TryParse(Request.QueryString["categorieid"], out _);
			bool val1 = int.TryParse(Request.Form["heure"].Replace(":", ""), out _);
			
			if (val0)
			{
				ViewBag.Erreur = "L'ajout n'a pas été effectué. Veuillez choisir une catégorie.";
				ViewBag.Activites = ActiviteServices.GetAll();
				return View("GererActivites");
			} else if((Request.Form["heure"].Length<2 
				|| Request.Form["heure"].Length > 5) 
				&& val1)
			{
				ViewBag.Erreur = "L'ajout n'a pas été effectué. Veuillez saisir une heure valide.";
				ViewBag.Activites = ActiviteServices.GetAll();
				return View("GererActivites");
			}
			ViewBag.Activites = ActiviteServices.GetAll();
			Activite activite = new Activite
			{
				Membre_id = ((Membre)Session["utilisateur"]).Id,
				Nom = Request.Form["Nom"],
				Categorie_id = int.Parse(Request.Form["categorieid"]),
				Dateheure = Request.Form["date"] + " " + Request.Form["heure"] + ":00",
				Lieu = Request.Form["Lieu"],
				Dsc = Request.Form["Dsc"],
				Motdepasse = Request.Form["Motdepasse"]
			};

			if (!ActiviteServices.Ajouter(activite))
			{
				ViewBag.Erreur = "L'ajout n'a pas été effectué. Veuillez vérifier les informations et réessayer.";
			}
			ViewBag.Message = "Ajout d'une activité.";
			return RedirectToAction("GererActivites");
		}

		[HttpGet]
		public ActionResult SupprimerActivite()
		{
			ViewBag.Erreur = "aucune";
			ViewBag.Message = "Supprime une activité.";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir supprimer des activités."; return View("Index"); };
			if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int parametreId))
			{
				if (((Membre)Session["utilisateur"]).Id == ActiviteServices.GetById(parametreId).Membre_id)
				{
					ActiviteServices.SupprimerParId(parametreId);
				} else if (ActiviteServices.GetByParticipantIdActiviteId(((Membre)Session["utilisateur"]).Id, parametreId)!=null)
				{
					ParticipantServices.SupprimerByParticipantIdActiviteId(((Membre)Session["utilisateur"]).Id, ActiviteServices.GetById(parametreId).Id);
				}
				ViewBag.Activite = ActiviteServices.GetById(parametreId);
			}
			else
			{
				ViewBag.Erreur = "Aucun ID valide n'a été spécifié pour l'acquisition d'une activité.";
				return View("GererActivites");
			}
			return RedirectToAction("GererActivites");
		}

		public ActionResult MenuActivite(List<Activite> liste)
		{
			ViewBag.Erreur = "aucune";
			ViewBag.Message = "Liste d'activite";
			ViewBag.categories = (List<Categorie>)CategorieServices.GetAll();
			ViewBag.activites = liste;
			return View("MenuActivite");
		}
		public ActionResult MenuAllActivite()
		{
			Session["categorie_Id"] = null;
			List<Activite> liste = ActiviteServices.GetByActivitePublic();
			return MenuActivite(liste);
		}
		public ActionResult MenuTrierActivite()
		{
			int categorie_id = int.Parse(Request.QueryString["id"]);
			Session["categorie_Id"] = "" + categorie_id;
			List<Activite> liste = ActiviteServices.GetByCategorieId(categorie_id);
			return MenuActivite(liste);
		}
		

		[HttpPost]
		public ActionResult RejoindreActivite()
		{
			string page = Request.Form["page"];
			ViewBag.Erreur = "aucune";
			ViewBag.Message = "Rejoint une activité.";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir rejoindre des activités."; return View("Index"); };
			List<Activite> liste = ActiviteServices.GetByActivitePublic();
			ViewBag.activites = liste;
			string code = Request.Form["Motdepasse"];
			if (code != null)
			{
				Activite act = ActiviteServices.GetByMotDePasse(code);
				if (act != null)
				{
					Participant part = new Participant
					{
						Activite_id = act.Id,
						Membre_id = ((Membre)Session["utilisateur"]).Id
					};
                    if (ParticipantServices.GetByActiviteMembreId(part.Activite_id, part.Membre_id)!=null)
                    {
						ViewBag.Erreur = "Vous êtes déjà inscrit à cette activité.";
						return View(page);
					} else
                    {
						ParticipantServices.Ajouter(part);
                    }
				} else
				{
					ViewBag.Erreur = "Aucune activité trouvée ayant ce code d'accès. Assurez-vous de bien l'avoir copié.";
					return View(page);
				}
			}
			else
			{
				ViewBag.Erreur = "Aucun code d'accès valide n'a été spécifié pour l'acquisition d'une activité.";
				return View(page);
			}
			//return RedirectToAction("GererActivites");
			return RedirectToAction(page);
		}

		public ActionResult GererParticipantsGet()
        {
			ViewBag.Erreur = "aucune";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir gèrer les participants de vos activités."; return View("Index"); };
			ViewBag.Message = "Page de la liste des participants d'une activité.";
			int parametreId = -1;
			bool parid = Request.QueryString["idact"] != null && int.TryParse(Request.QueryString["idact"], out parametreId);
			Activite activ = ActiviteServices.GetById(parametreId);
			if (parid && activ!=null)
            {
				ViewBag.Participants = ParticipantServices.GetByActiviteId(parametreId);
				ViewBag.Activite = activ;
			}
			else
				ViewBag.Erreur = "Aucun ID valide n'a été spécifié pour l'acquisition d'une activité.";
			return View();
		}

		
		public ActionResult Desinscription()
        {
			ViewBag.Erreur = "aucune";
			ViewBag.Message = "Désinscrit un participant d'une activité.";
			if (!VerificationSession(Session)) { ViewBag.Erreur = "Veuillez vous connecter pour pouvoir supprimer des activités."; return View("Index"); };
			int parametreIdMem, parametreIdAct;
			if (Request.QueryString["idmem"] != null && int.TryParse(Request.QueryString["idmem"], out parametreIdMem))
			{
				if (Request.QueryString["idact"] != null && int.TryParse(Request.QueryString["idact"], out parametreIdAct))
                {
					Activite activ = ActiviteServices.GetByParticipantIdActiviteId(parametreIdMem, parametreIdAct);
                    if (activ != null && ((Membre)Session["utilisateur"]).Id == activ.Membre_id)
                    {
						if(ParticipantServices.SupprimerByParticipantIdActiviteId(parametreIdMem, parametreIdAct)==false)
                        {
							ViewBag.Erreur = "Le participant n'a pas été désinscrit, veuillez réessayer.";
							return View("GererActivites");
						} else
                        {
							return RedirectToAction("GererParticipantsGet", new { @idact = parametreIdAct });
						}
					}
				} else
                {
					ViewBag.Erreur = "L'identifiant de l'activité spécifiée n'est pas valide, veuillez réessayer.";
					return View("GererActivites");
				}
			}
			else
			{
				ViewBag.Erreur = "L'identifiant du membre spécifié n'est pas valide, veuillez réessayer.";
				return View("GererActivites");
			}
			return RedirectToAction("GererParticipantsGet", new { @idact = parametreIdAct });
		}


		public static bool VerificationSession(HttpSessionStateBase session)
		{
			return session["utilisateur"] != null;
		}


		public ActionResult Participer()
		{
			Participant nouveau = new Participant();
			nouveau.Membre_id = ((Membre)Session["utilisateur"]).Id;
			nouveau.Activite_id = int.Parse(Request.QueryString["id"]);
			ParticipantServices.Ajouter(nouveau);
			return RedirectToAction("MenuAllActivite");
		}
		public ActionResult AnnulerParticiper()
		{
			ParticipantServices.SupprimerParId(int.Parse(Request.QueryString["id"]));
			return RedirectToAction("MenuAllActivite");
		}
	}
}