using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace InmobiliariaLorenzo.Models;

using System;
using Microsoft.Extensions.Configuration;

	public class RepositorioInquilino : RepositorioBase, IRepositorioInquilino
	{
		public RepositorioInquilino(IConfiguration configuration) : base(configuration)
		{
			//https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
		}
		public int Alta(Inquilino inquilino)
		{
			int res = -1;
			using (var connection = new MySqlConnection(connectionString))
			{
				var sql = @"INSERT INTO INQUILINOS(Apellido,Nombre,Dni,Telefono)
            VALUES (@Apellido,@Nombre,@Dni,@Telefono);
            SELECT LAST_INSERT_ID()"; ;//devuelve el id insertado (SCOPE_IDENTITY para sql)
				using (var cmd = new MySqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
					cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
					cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
					cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
					connection.Open();
					res = Convert.ToInt32(cmd.ExecuteScalar());
					inquilino.Id_Inquilino = res;
					connection.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (var connection = new MySqlConnection(connectionString))
			{
				string sql = @$"DELETE FROM INQUILINOS WHERE {nameof(Inquilino.Id_Inquilino)} = @id";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public int Modificacion(Inquilino inquilino)
		{
			int res = -1;
			using (var connection = new MySqlConnection(connectionString))
			{
				string sql = @$"UPDATE INQUILINOS 
					SET Apellido=@Apellido, Nombre=@Nombre, Dni=@Dni, Telefono=@Telefono 
					WHERE {nameof(Inquilino.Id_Inquilino)} = @id";
				using (var cmd = new MySqlCommand(sql, connection))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
					cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
					cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
					cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
					cmd.Parameters.AddWithValue("@id", inquilino.Id_Inquilino);
					connection.Open();
					res = cmd.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		
		public IList<Inquilino> ObtenerTodos()
		{
			IList<Inquilino> res = new List<Inquilino>();
			using (var connection = new MySqlConnection(connectionString))
			{
				var sql = "SELECT Id_Inquilino,Apellido,Nombre,Dni,Telefono FROM INQUILINOS";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inquilino inquilino = new Inquilino
						{
							Id_Inquilino = reader.GetInt32(nameof(Inquilino.Id_Inquilino)),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Dni = reader.GetString("Dni"),
							Telefono = reader.GetString("Telefono"),							
						};
						res.Add(inquilino);
					}
					connection.Close();
				}
			}
			return res;
		}
		
		virtual public Inquilino ObtenerPorId(int id)
		{
			Inquilino? inquilino = null;
			using (var connection = new MySqlConnection(connectionString))
			{
				var sql = @"SELECT Id_Inquilino,Apellido,Nombre,Dni,Telefono
            FROM INQUILINOS
            WHERE Id_Inquilino = @id";
				using (var command = new MySqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", DbType.Int32).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						inquilino = new Inquilino
						{
							Id_Inquilino = reader.GetInt32(nameof(Inquilino.Id_Inquilino)),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Dni = reader.GetString("Dni"),
							Telefono = reader.GetString("Telefono"),							
						};
					}
					connection.Close();
				}
			}
			return inquilino;
		}
	}

