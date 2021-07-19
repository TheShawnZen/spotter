using MySql.Data.MySqlClient;
using System;
using Spotter.Modele;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Spotter.Providers
{
    public class CategorieProvider
    {
		private static DbConnection cnx;
		private static DbCommand cmd;
		const string requeteFindById = "SELECT * FROM `categorie` WHERE `id` = @id";
		const string requeteGetAll = "SELECT * FROM `categorie`";
		const string requeteInsert = "INSERT INTO `categorie`(`nom`)" +
			"VALUES(@nom)";
		const string requeteDeleteById = "DELETE FROM `categorie` WHERE id = @id";

		public static Categorie FindById(int id)
		{
			Categorie categorie = null;
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
				categorie = new Categorie
				{
					Id = Int32.Parse(data["id"].ToString()),
					Nom = data["nom"].ToString()
				};
			}
			cnx.Close();
			return categorie;
		}

		public static bool Creer(Categorie categorie)
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
				ParameterName = "nom",
				DbType = System.Data.DbType.String,
				Value = categorie.Nom
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

		public static List<Categorie> FindAll()
		{
			List<Categorie> liste = new List<Categorie>();
			Categorie categorie;
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
				categorie = new Categorie
				{
					Id = Int32.Parse(data["id"].ToString()),
					Nom = data["nom"].ToString()
				};
				liste.Add(categorie);
			}
			cnx.Close();
			return liste;
        }
	}
}