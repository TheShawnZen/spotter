using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Spotter.Modele;

namespace Spotter.Providers
{
	public class MembreProvider
	{
		private static DbConnection cnx;
		private static DbCommand cmd;
		const string requeteFindById = "SELECT * FROM `membre` WHERE `id` = @id";
		const string requeteGetAll = "SELECT * FROM `membre`";
		const string requeteInsert = "INSERT INTO `membre`(`email`,`username`,`motdepasse`)" +
			"VALUES(@email,@username,@motdepasse)";
		const string requeteDeleteById = "DELETE FROM `membre` WHERE id = @id";
		const string requeteGetByUserPass = "SELECT * FROM `membre` WHERE `username`=@username AND `motdepasse`=@mdp";

		public static Membre FindById(int id)
		{
			Membre membre = null;
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
				DbType = System.Data.DbType.String,
				Value = id
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			if (data.Read())
			{
				membre = new Membre
				{
					Id = Int32.Parse(data["id"].ToString()),
					Email = data["email"].ToString(),
					Username = data["username"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
			}
			cnx.Close();
			return membre;
		}

		public static Membre FindByUserPass(string user, string passw)
		{
			Membre membre = null;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteGetByUserPass;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "username",
				DbType = System.Data.DbType.String,
				Value = user
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "mdp",
				DbType = System.Data.DbType.String,
				Value = passw
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			if (data.Read())
			{
				membre = new Membre
				{
					Id = Int32.Parse(data["id"].ToString()),
					Email = data["email"].ToString(),
					Username = data["username"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
			}
			cnx.Close();
			return membre;
		}

		public static bool Creer(Membre membre)
		{
			bool resultat;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteInsert;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "email",
				DbType = System.Data.DbType.String,
				Value = membre.Email
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "username",
				DbType = System.Data.DbType.String,
				Value = membre.Username
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "motdepasse",
				DbType = System.Data.DbType.String,
				Value = membre.Motdepasse
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
				DbType = System.Data.DbType.String,
				Value = id
			};
			cmd.Parameters.Add(param);
			resultat = cmd.ExecuteNonQuery() > 0;
			cnx.Close();
			return resultat;
		}
	
		public static List<Membre> FindAll()
		{
			List<Membre> liste = new List<Membre>();
			Membre membre;
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
				membre = new Membre
				{
					Id = Int32.Parse(data["id"].ToString()),
					Email = data["email"].ToString(),
					Username = data["username"].ToString(),
					Motdepasse = data["motdepasse"].ToString()
				};
				liste.Add(membre);
			}
			cnx.Close();
			return liste;
		}
	}
}