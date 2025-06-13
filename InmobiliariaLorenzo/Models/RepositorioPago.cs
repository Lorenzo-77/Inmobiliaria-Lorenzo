using MySql.Data.MySqlClient;

namespace InmobiliariaLorenzo.Models;

public class RepositorioPago : RepositorioBase, IRepositorioPago
{
    public RepositorioPago(IConfiguration configuration) : base(configuration)
    {
        //https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
    }
    public int Alta(Pago pago)
    {
        var res = -1;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO PAGOS(Fecha,Importe,Id_Contrato)
            VALUES (@Fecha,@Importe,@Id_Contrato);
            SELECT LAST_INSERT_ID()";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha", pago.Fecha);
                cmd.Parameters.AddWithValue("@Importe", pago.Importe);
                cmd.Parameters.AddWithValue("@Id_Contrato", pago.Id_Contrato);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                pago.Id_Pago = res;
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
            var sql = @"DELETE FROM PAGOS
            WHERE Id_Pago= @id";

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
    public int Modificacion(Pago pago)
    {
        var res = -2;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE PAGOS
            SET Fecha=@Fecha,Importe=@Importe,Id_Contrato=@Id_Contrato
            WHERE Id_Pago = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha", pago.Fecha);
                cmd.Parameters.AddWithValue("@Importe", pago.Importe);
                cmd.Parameters.AddWithValue("@Id_Contrato", pago.Id_Contrato);
                cmd.Parameters.AddWithValue("@id", pago.Id_Pago);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }
    }

    public IList<Pago> ObtenerTodos()
    {
        var res = new List<Pago>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM PAGOS p
            INNER JOIN CONTRATOS c ON p.Id_Contrato = c.Id_Contrato";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Pago
                            {
                                Id_Pago = reader.GetInt32("Id_Pago"),
                                Fecha = reader.GetDateTime("Fecha"),
                                Importe = reader.GetDecimal("Importe"),
                                Id_Contrato = reader.GetInt32("Id_Contrato"),

                            });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

 public Pago ObtenerPorId(int id)
    {
        var res = new Pago();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM PAGOS p
            INNER JOIN CONTRATOS c ON p.Id_Contrato = c.Id_Contrato
            WHERE Id_Pago = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = (new Pago
                        {
                            Id_Pago = reader.GetInt32("Id_Pago"),
                            Fecha = reader.GetDateTime("Fecha"),
                            Importe = reader.GetDecimal("Importe"),
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public IList<Pago> PagosPorContrato(int id)
    {
        var res = new List<Pago>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM PAGOS p
            INNER JOIN CONTRATOS c ON p.Id_Contrato = c.Id_Contrato";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Pago
                            {
                                Id_Pago = reader.GetInt32("Id_Pago"),
                                Fecha = reader.GetDateTime("Fecha"),
                                Importe = reader.GetDecimal("Importe"),
                                Id_Contrato = reader.GetInt32("Id_Contrato"),

                            });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }


    public IList<Pago> PagosContratoPorInquilino(int id)
    {
        var res = new List<Pago>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM PAGOS p
            INNER JOIN CONTRATOS c ON p.Id_Contrato = c.Id_Contrato
            WHERE c.Id_Contrato = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(
                            new Pago
                            {
                                Id_Pago = reader.GetInt32("Id_Pago"),
                                Fecha = reader.GetDateTime("Fecha"),
                                Importe = reader.GetDecimal("Importe"),
                                Id_Contrato = reader.GetInt32("Id_Contrato"),
                            });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }
}