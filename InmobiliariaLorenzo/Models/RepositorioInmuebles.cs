using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;


namespace InmobiliariaLorenzo.Models;

public class RepositorioInmueble : RepositorioBase, IRepositorioInmueble
{
	public RepositorioInmueble(IConfiguration configuration) : base(configuration)
	{
		//https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
	}


	public int Alta(Inmueble inmueble)
	{
		var res = -1;
		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{

			var sql = @"INSERT INTO INMUEBLES(Direccion,Id_Uso,Id_Tipo,Ambientes,Latitud,Longitud,Precio,Activo,Id_Propietario)
            VALUES (@Direccion,@Id_Uso,@Id_Tipo,@Ambientes,@Latitud,@Longitud,@Precio,@Activo,@Id_Propietario);
            SELECT LAST_INSERT_ID()";

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
				cmd.Parameters.AddWithValue("@Id_Uso", inmueble.Id_Uso);
				cmd.Parameters.AddWithValue("@Id_Tipo", inmueble.Id_Tipo);
				cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
				cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
				cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
				cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
				cmd.Parameters.AddWithValue("@Activo", inmueble.Activo);
				cmd.Parameters.AddWithValue("@Id_Propietario", inmueble.Id_Propietario);

				conn.Open();
				res = Convert.ToInt32(cmd.ExecuteScalar());
				inmueble.Id_Inmueble = res;
				conn.Close();
			}
			return res;
		}

	}

	public int Baja(int id)
	{
		var res = -3;

		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"DELETE FROM INMUEBLES
            WHERE Id_Inmueble = @id";

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				conn.Open();
				res = cmd.ExecuteNonQuery();
				conn.Close();
			}
		}
		return res;
	}

	public int Modificacion(Inmueble inmueble)
	{
		var res = -2;

		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"UPDATE INMUEBLES
            SET Direccion=@Direccion,Id_Uso=@Id_Uso,Id_Tipo=@Id_Tipo,Ambientes=@Ambientes,Latitud=@Latitud,Longitud=@Longitud,
            Precio=@Precio,Activo=@Activo,Id_Propietario=@Id_Propietario 
            WHERE Id_Inmueble = @id";

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
				cmd.Parameters.AddWithValue("@Id_Uso", inmueble.Id_Uso);
				cmd.Parameters.AddWithValue("@Id_Tipo", inmueble.Id_Tipo);
				cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
				cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
				cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
				cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
				cmd.Parameters.AddWithValue("@Activo", inmueble.Activo);
				cmd.Parameters.AddWithValue("@Id_Propietario", inmueble.Id_Propietario);
				cmd.Parameters.AddWithValue("@id", inmueble.Id_Inmueble);
				conn.Open();
				res = cmd.ExecuteNonQuery();
				conn.Close();
			}
			return res;
		}
	}

	public IList<Inmueble> ObtenerTodos()
	{
		var res = new List<Inmueble>();

		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"
            SELECT i.Id_Inmueble,i.Direccion,i.Id_Uso,i.Id_Tipo,i.Ambientes,i.Latitud,i.Longitud,i.Precio,i.Activo,i.Id_Propietario,
            p.Nombre, p.Apellido, u.Nombre 'NombreUso', t.Nombre 'NombreTipo'
            FROM INMUEBLES i
            INNER JOIN PROPIETARIOS p ON i.Id_Propietario = p.Id_Propietario
            INNER JOIN USOS u ON i.Id_Uso = u.Id_Uso
            INNER JOIN TIPOS t ON i.Id_Tipo = t.Id_Tipo
            WHERE i.Activo = 1";

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				conn.Open();
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						res.Add(
							new Inmueble
							{
								Id_Inmueble = reader.GetInt32("Id_Inmueble"),
								Direccion = reader.GetString("Direccion"),
								Id_Uso = reader.GetInt32("Id_Uso"),
								Id_Tipo = reader.GetInt32("Id_Tipo"),
								Ambientes = reader.GetInt32("Ambientes"),
								Latitud = reader.GetDecimal("Latitud"),
								Longitud = reader.GetDecimal("Longitud"),
								Precio = reader.GetDecimal("Precio"),
								Activo = reader.GetBoolean("Activo"),
								Id_Propietario = reader.GetInt32("Id_Propietario"),
								Titular = new Propietario
								{
									Id_Propietario = reader.GetInt32("Id_Propietario"),
									Nombre = reader.GetString("Nombre"),
									Apellido = reader.GetString("Apellido"),
								},
								Uso = new Uso
								{
									Id_Uso = reader.GetInt32("Id_Uso"),
									Nombre = reader.GetString("NombreUso"),
								},
								Tipo = new Tipo
								{
									Id_Tipo = reader.GetInt32("Id_Tipo"),
									Nombre = reader.GetString("NombreTipo"),
								}
							});
					}
				}
				conn.Close();
			}
		}

		return res;
	}

	public Inmueble ObtenerPorId(int id)
	{
		var res = new Inmueble();

		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"SELECT i.Id_Inmueble,i.Direccion,i.Id_Uso,i.Id_Tipo,i.Ambientes,i.Latitud,i.Longitud,i.Precio,i.Activo,i.Id_Propietario,
            p.Nombre, p.Apellido, u.Nombre 'NombreUso', t.Nombre 'NombreTipo'
            FROM INMUEBLES i
            INNER JOIN PROPIETARIOS p ON i.Id_Propietario = p.Id_Propietario
            INNER JOIN USOS u ON i.Id_Uso = u.Id_Uso
            INNER JOIN TIPOS t ON i.Id_Tipo = t.Id_Tipo
            WHERE Id_Inmueble = @id";

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				conn.Open();
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						res = (new Inmueble
						{
							Id_Inmueble = reader.GetInt32("Id_Inmueble"),
							Direccion = reader.GetString("Direccion"),
							Id_Uso = reader.GetInt32("Id_Uso"),
							Id_Tipo = reader.GetInt32("Id_Tipo"),
							Ambientes = reader.GetInt32("Ambientes"),
							Latitud = reader.GetDecimal("Latitud"),
							Longitud = reader.GetDecimal("Longitud"),
							Precio = reader.GetDecimal("Precio"),
							Activo = reader.GetBoolean("Activo"),
							Id_Propietario = reader.GetInt32("Id_Propietario"),
							Titular = new Propietario
							{
								Id_Propietario = reader.GetInt32("Id_Propietario"),
								Nombre = reader.GetString("Nombre"),
								Apellido = reader.GetString("Apellido"),
							},
							Uso = new Uso
							{
								Id_Uso = reader.GetInt32("Id_Uso"),
								Nombre = reader.GetString("NombreUso"),
							},
							Tipo = new Tipo
							{
								Id_Tipo = reader.GetInt32("Id_Tipo"),
								Nombre = reader.GetString("NombreTipo"),
							}
						});
					}
				}
				conn.Close();
			}
		}
		return res;
	}

	public IList<Inmueble> BuscarInmuebles(InmuebleBusqueda ib)
	{
		var res = new List<Inmueble>();

		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"
            SELECT DISTINCT i.Id_Inmueble,i.Direccion,i.Id_Uso,i.Id_Tipo,i.Ambientes,i.Latitud,i.Longitud,i.Precio,i.Activo,i.Id_Propietario, p.Nombre, p.Apellido, u.Nombre 'NombreUso', t.Nombre 'NombreTipo' 
            FROM INMUEBLES i
            INNER JOIN PROPIETARIOS p ON i.Id_Propietario = p.Id_Propietario 
            INNER JOIN USOS u ON i.Id_Uso = u.Id_Uso 
            INNER JOIN TIPOS t ON i.Id_Tipo = t.Id_Tipo 
            LEFT JOIN CONTRATOS c ON c.Id_Inmueble = i.Id_Inmueble
            WHERE Activo = 1
            AND (
                (c.Fecha_Inicio > @fecha_fin OR c.Fecha_Fin < @fecha_inicio)
                OR c.Id_Contrato IS NULL
            )";

			if (ib.Id_Tipo.HasValue)
			{
				sql += " AND i.Id_Tipo = @id_tipo";
			}

			if (ib.Id_Uso.HasValue)
			{
				sql += " AND i.Id_Uso = @id_uso";
			}

			if (ib.Ambientes.HasValue)
			{
				sql += " AND i.Ambientes = @ambientes";
			}

			if (ib.Precio.HasValue)
			{
				sql += " AND i.Precio = @precio";
			}

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{

				cmd.Parameters.AddWithValue("@fecha_inicio", ib.Fecha_Inicio);
				cmd.Parameters.AddWithValue("@fecha_fin", ib.Fecha_Fin);

				if (ib.Id_Tipo.HasValue)
				{
					cmd.Parameters.AddWithValue("@id_tipo", ib.Id_Tipo);
				}

				if (ib.Id_Uso.HasValue)
				{
					cmd.Parameters.AddWithValue("@id_uso", ib.Id_Uso);
				}

				if (ib.Ambientes.HasValue)
				{
					cmd.Parameters.AddWithValue("@ambientes", ib.Ambientes);
				}

				if (ib.Precio.HasValue)
				{
					cmd.Parameters.AddWithValue("@precio", ib.Precio);
				}

				conn.Open();
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						res.Add(
							new Inmueble
							{
								Id_Inmueble = reader.GetInt32("Id_Inmueble"),
								Direccion = reader.GetString("Direccion"),
								Id_Uso = reader.GetInt32("Id_Uso"),
								Id_Tipo = reader.GetInt32("Id_Tipo"),
								Ambientes = reader.GetInt32("Ambientes"),
								Latitud = reader.GetDecimal("Latitud"),
								Longitud = reader.GetDecimal("Longitud"),
								Precio = reader.GetDecimal("Precio"),
								Activo = reader.GetBoolean("Activo"),
								Id_Propietario = reader.GetInt32("Id_Propietario"),
								Titular = new Propietario
								{
									Id_Propietario = reader.GetInt32("Id_Propietario"),
									Nombre = reader.GetString("Nombre"),
									Apellido = reader.GetString("Apellido"),
								},
								Uso = new Uso
								{
									Id_Uso = reader.GetInt32("Id_Uso"),
									Nombre = reader.GetString("NombreUso"),
								},
								Tipo = new Tipo
								{
									Id_Tipo = reader.GetInt32("Id_Tipo"),
									Nombre = reader.GetString("NombreTipo"),
								}
							});
					}
				}
				conn.Close();
			}
		}

		return res;
	}

	public IList<Inmueble> BuscarInmueblesEstado(InmuebleBusqueda ib)
	{
		var res = new List<Inmueble>();


		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"
            SELECT * 
            FROM INMUEBLES i
            LEFT JOIN CONTRATOS c ON c.Id_Inmueble = i.Id_Inmueble
            WHERE Activo = 1
            AND (
                (c.Fecha_Inicio > @fecha_fin OR c.Fecha_Fin < @fecha_inicio)
                OR c.Id_Contrato IS NULL
            )";

			if (ib.Activo.HasValue)
			{
				sql += " AND i.Activo = @activo";
			}

			if (ib.Id_Propietario.HasValue)
			{
				sql += " AND i.Id_Propietario = @id_propietario";
			}

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{

				if (ib.Activo.HasValue)
				{
					cmd.Parameters.AddWithValue("@activo", ib.Activo);
				}

				if (ib.Id_Propietario.HasValue)
				{
					cmd.Parameters.AddWithValue("@id_propietario", ib.Id_Propietario);
				}

				conn.Open();
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						res.Add(
							new Inmueble
							{
								Id_Inmueble = reader.GetInt32("Id_Inmueble"),
								Direccion = reader.GetString("Direccion"),
								Id_Uso = reader.GetInt32("Id_Uso"),
								Id_Tipo = reader.GetInt32("Id_Tipo"),
								Ambientes = reader.GetInt32("Ambientes"),
								Latitud = reader.GetDecimal("Latitud"),
								Longitud = reader.GetDecimal("Longitud"),
								Precio = reader.GetDecimal("Precio"),
								Activo = reader.GetBoolean("Activo"),
								Id_Propietario = reader.GetInt32("Id_Propietario"),
								Titular = new Propietario
								{
									Id_Propietario = reader.GetInt32("Id_Propietario"),
									Nombre = reader.GetString("Nombre"),
									Apellido = reader.GetString("Apellido"),
								},
								Uso = new Uso
								{
									Id_Uso = reader.GetInt32("Id_Uso"),
									Nombre = reader.GetString("NombreUso"),
								},
								Tipo = new Tipo
								{
									Id_Tipo = reader.GetInt32("Id_Tipo"),
									Nombre = reader.GetString("NombreTipo"),
								}
							});
					}
				}
				conn.Close();
			}
		}

		return res;
	}

	public Boolean VerificarDisponibilidad(int id, DateTime fecha_inicio, DateTime fecha_fin)
	{
		var res = 0;

		using (MySqlConnection conn = new MySqlConnection(connectionString))
		{
			var sql = @"SELECT count(1) cantidad
            FROM CONTRATOS c
            WHERE c.Id_Inmueble = @id
            AND (
                (c.Fecha_Inicio <= @fecha_fin AND c.Fecha_Fin >= @fecha_inicio)
            )";

			using (MySqlCommand cmd = new MySqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
				cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);
				cmd.Parameters.AddWithValue("@id", id);

				conn.Open();
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						res = reader.GetInt32("cantidad");
					}
				}
				conn.Close();
			}
		}
		// Si hay 0 contratos que se solapan, está disponible.
		return res == 0;
	}

}