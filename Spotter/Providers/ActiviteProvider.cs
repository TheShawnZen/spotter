using System;
using Spotter.Modele;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spotter.Providers
{
	public class ActiviteProvider
	{
		private static DbConnection cnx;
		private static DbCommand cmd;
		const string requeteInsert = "INSERT INTO `activite`(membre_id, nom, categorie_id, dateheure,lieu,dsc,motdepasse) " +
				"VALUES (@membre_id,@nom,@categorie_id,@dateheure,@lieu,@dsc,@motdepasse)";
		const string requeteDeleteById = "DELETE FROM `activite` WHERE id = @id";
		const string requeteGetAll = "SELECT * FROM `activite`";
		const string requeteFindById = "SELECT * FROM `activite` WHERE `id` = @id";
		const string requeteFindByMdp = "SELECT * FROM `activite` WHERE `motdepasse` = @mdp";
		const string requeteFindPublic = "SELECT * FROM `activite` WHERE `motdepasse` IS NULL";
		const string requeteEditById = "UPDATE `spotter`.`activite` SET `nom` = @nom, `categorie_id` = @categorieid, `dateheure` = @dateheure, `lieu` = @lieu, `motdepasse` = @motdepasse, `dsc` = @description WHERE (`id` = @id)";
		const string requeteGetByMemberId = "SELECT * FROM `spotter`.`activite` WHERE `membre_id` = @id";
		const string requeteGetByParticipantId = "SELECT activite.id AS `id`, activite.membre_id AS `membre_id`, activite.categorie_id AS `categorie_id`, activite.nom AS `nom`, activite.dateheure AS `dateheure`, activite.lieu AS `lieu`, activite.dsc AS `description`, activite.motdepasse AS `motdepasse` FROM spotter.participant JOIN membre ON membre.id=participant.membre_id JOIN activite ON activite.id=participant.activite_id WHERE participant.membre_id=@id";
		const string requeteGetByParticipantIdActiviteId = "SELECT activite.id AS `id`, activite.membre_id AS `membre_id`, activite.categorie_id AS `categorie_id`, activite.nom AS `nom`, activite.dateheure AS `dateheure`, activite.lieu AS `lieu`, activite.dsc AS `description`, activite.motdepasse AS `motdepasse` FROM spotter.participant JOIN membre ON membre.id=participant.membre_id JOIN activite ON activite.id=participant.activite_id WHERE participant.membre_id=@participant_id AND activite.id=@activite_id";
		const string requeteGetByCategorieId = "SELECT * FROM `activite` WHERE `categorie_id` = @id";

		public static bool Creer(Activite activite)
		{
			bool resultat;
			cnx = new MySqlConnection();
			cnx.ConnectionString = Config.CONNECTION_STRING;
			cnx.Open();
			cmd = cnx.CreateCommand();
			string requete = requeteInsert;
			cmd.CommandText = requete;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "membre_id",
				DbType = System.Data.DbType.Int32,
				Value = activite.Membre_id
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "nom",
				DbType = System.Data.DbType.String,
				Value = activite.Nom
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "categorie_id",
				DbType = System.Data.DbType.Int32,
				Value = activite.Categorie_id
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "dateheure",
				DbType = System.Data.DbType.String,
				Value = activite.Dateheure
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "lieu",
				DbType = System.Data.DbType.String,
				Value = activite.Lieu
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "dsc",
				DbType = System.Data.DbType.String,
				Value = activite.Dsc
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "motdepasse",
				DbType = System.Data.DbType.String,
				Value = activite.Motdepasse
			};
			cmd.Parameters.Add(param);
			resultat = cmd.ExecuteNonQuery() > 0;
			cnx.Close();
			return resultat;
		}
		
		public static bool SupprimerById(int id)
		{
			bool resultat;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteDeleteById;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "id",
				DbType = System.Data.DbType.Int32,
				Value = id
			};
			cmd.Parameters.Add(param);
			resultat = cmd.ExecuteNonQuery() > 0;
			cnx.Close();
			return resultat;
		}

		public static List<Activite> FindAll()
		{
			List<Activite> liste = new List<Activite>();
			Activite activite;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteGetAll;
			cmd.CommandType = System.Data.CommandType.Text;
			DbDataReader data = cmd.ExecuteReader();
			while (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["dsc"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
				liste.Add(activite);
			}
			cnx.Close();
			return liste;
		}

		public static Activite FindById(int id)
		{
			Activite activite = null;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteFindById;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "id",
				DbType = System.Data.DbType.Int32,
				Value = id
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			if (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["dsc"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
			}
			cnx.Close();
			return activite;
		}

		public static Activite FindByMdp(string mdp)
		{
			Activite activite = null;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteFindByMdp;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "mdp",
				DbType = System.Data.DbType.String,
				Value = mdp
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			if (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["dsc"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
			}
			cnx.Close();
			return activite;
		}
		public static List<Activite> FindPublic()
		{
			List<Activite> liste = new List<Activite>();
			Activite activite;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteFindPublic;
			cmd.CommandType = System.Data.CommandType.Text;
			DbDataReader data = cmd.ExecuteReader();
			while (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["dsc"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
				liste.Add(activite);
			}
			cnx.Close();
			return liste;
		}

		public static bool EditById(Activite activite)
		{
			bool resultat;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteEditById;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "nom",
				DbType = System.Data.DbType.String,
				Value = activite.Nom
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "categorieid",
				DbType = System.Data.DbType.Int32,
				Value = activite.Categorie_id
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "dateheure",
				DbType = System.Data.DbType.DateTime,
				Value = activite.Dateheure
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "lieu",
				DbType = System.Data.DbType.String,
				Value = activite.Lieu
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "description",
				DbType = System.Data.DbType.String,
				Value = activite.Dsc
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "motdepasse",
				DbType = System.Data.DbType.String,
				Value = activite.Motdepasse
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "id",
				DbType = System.Data.DbType.Int32,
				Value = activite.Id
			};
			cmd.Parameters.Add(param);
			resultat = cmd.ExecuteNonQuery() > 0;
			cnx.Close();
			return resultat;
		}

		public static List<Activite> FindByMemberId(int id)
		{
			List<Activite> liste = new List<Activite>();
			Activite activite;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteGetByMemberId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "id",
				DbType = System.Data.DbType.Int32,
				Value = id
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			while (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["dsc"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
				liste.Add(activite);
			}
			cnx.Close();
			return liste;
		}

		public static List<Activite> FindByParticipantId(int id)
		{
			List<Activite> liste = new List<Activite>();
			Activite activite;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteGetByParticipantId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "id",
				DbType = System.Data.DbType.Int32,
				Value = id
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			while (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["description"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
				liste.Add(activite);
			}
			cnx.Close();
			return liste;
		}

		public static Activite FindByParticipantIdActiviteId(int pid, int aid)
		{
			Activite activite = null;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteGetByParticipantIdActiviteId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "participant_id",
				DbType = System.Data.DbType.Int32,
				Value = pid
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "activite_id",
				DbType = System.Data.DbType.Int32,
				Value = aid
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			if (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["description"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
			}
			cnx.Close();
			return activite;
		}

		public static List<Activite> FindByCategorieId(int id)
		{
			List<Activite> liste = new List<Activite>();
			Activite activite;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteGetByCategorieId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "id",
				DbType = System.Data.DbType.Int32,
				Value = id
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			while (data.Read())
			{
				activite = new Activite
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Nom = data["nom"].ToString(),
					Categorie_id = Int32.Parse(data["categorie_id"].ToString()),
					Dateheure = data["dateheure"].ToString(),
					Lieu = data["lieu"].ToString(),
					Dsc = data["dsc"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
				liste.Add(activite);
			}
			cnx.Close();
			return liste;
		}
	}
}
