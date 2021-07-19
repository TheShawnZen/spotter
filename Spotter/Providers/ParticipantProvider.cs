using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using Spotter.Modele;

namespace Spotter.Providers
{
	public class ParticipantProvider
	{
		private static DbConnection cnx;
		private static DbCommand cmd;
		const string requeteFindById = "SELECT * FROM `participant` WHERE `id` = @id";
		const string requeteFindByActiviteId = "SELECT * FROM participant WHERE activite_id = @activite_id";
		const string requeteFindByActiviteMembreId = "SELECT * FROM participant WHERE activite_id = @activite_id and membre_id = @membre_id";
		const string requeteGetAll = "SELECT * FROM `participant`";
		const string requeteInsert = "INSERT INTO `participant`(`membre_id`,`activite_id`)" +
			"VALUES(@membre_id,@activite_id)";
		const string requeteDeleteById = "DELETE FROM `participant` WHERE id = @id";
		const string requeteDeleteByParticipantIdActiviteId = "DELETE FROM `participant` WHERE membre_id = @participant_id AND activite_id = @activite_id";

		public static Participant FindById(int id)
		{
			Participant participant = null;
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
				participant = new Participant
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Activite_id = Int32.Parse(data["activite_id"].ToString())
				};
			}
			cnx.Close();
			return participant;
		}
		public static List<Participant> FindByActiviteId(int id)
		{
			List<Participant> liste = null;
			Participant participant = null;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteFindByActiviteId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "activite_id",
				DbType = System.Data.DbType.Int32,
				Value = id
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			liste = new List<Participant>();
			while (data.Read())
			{
				participant = new Participant
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Activite_id = Int32.Parse(data["activite_id"].ToString())
				};
				liste.Add(participant);
			}
			cnx.Close();
			return liste;
		}
		public static Participant FindByActiviteMembreId(int activite, int membre)
		{
			Participant participant = null;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteFindByActiviteMembreId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "activite_id",
				DbType = System.Data.DbType.Int32,
				Value = activite
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "membre_id",
				DbType = System.Data.DbType.Int32,
				Value = membre
			};
			cmd.Parameters.Add(param);
			DbDataReader data = cmd.ExecuteReader();
			if (data.Read())
			{
				participant = new Participant
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Activite_id = Int32.Parse(data["activite_id"].ToString())
				};
			}
			cnx.Close();
			return participant;
		}

		public static bool Creer(Participant participant)
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
				ParameterName = "membre_id",
				DbType = System.Data.DbType.Int32,
				Value = participant.Membre_id
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "activite_id",
				DbType = System.Data.DbType.Int32,
				Value = participant.Activite_id
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

		public static List<Participant> FindAll()
		{
			List<Participant> liste = new List<Participant>();
			Participant participant;
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
				participant = new Participant
				{
					Id = Int32.Parse(data["id"].ToString()),
					Membre_id = Int32.Parse(data["membre_id"].ToString()),
					Activite_id = Int32.Parse(data["activite_id"].ToString())
				};
				liste.Add(participant);
			}
			cnx.Close();
			return liste;
		}

		public static bool SupprimerParParticipantIdActiviteId(int pid, int aid)
		{
			bool resultat;
			cnx = new MySqlConnection
			{
				ConnectionString = Config.CONNECTION_STRING
			};
			cnx.Open();
			cmd = cnx.CreateCommand();
			cmd.CommandText = requeteDeleteByParticipantIdActiviteId;
			cmd.CommandType = System.Data.CommandType.Text;
			DbParameter param = new MySqlParameter
			{
				ParameterName = "activite_id",
				DbType = System.Data.DbType.Int32,
				Value = aid
			};
			cmd.Parameters.Add(param);
			param = new MySqlParameter
			{
				ParameterName = "participant_id",
				DbType = System.Data.DbType.Int32,
				Value = pid
			};
			cmd.Parameters.Add(param);
			resultat = cmd.ExecuteNonQuery() > 0;
			cnx.Close();
			return resultat;
		}
	}
}
