
using MySql.Data.MySqlClient;

namespace InmobiliariaLorenzo.Models;

public class RepositorioContrato : RepositorioBase, IRepositorioContrato
{
    public RepositorioContrato(IConfiguration configuration) : base(configuration) { }

    public int Alta(Contrato contrato)
    {
        int res = -1;
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO CONTRATOS 
                (Fecha_Inicio, Fecha_Fin, Monto, Id_Inmueble, Id_Inquilino, Id_Usuario_Creador)
                VALUES (@Fecha_Inicio, @Fecha_Fin, @Monto, @Id_Inmueble, @Id_Inquilino, @Id_Usuario_Creador);
                SELECT LAST_INSERT_ID();";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha_Inicio", contrato.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fecha_Fin", contrato.Fecha_Fin);
                cmd.Parameters.AddWithValue("@Monto", contrato.Monto);
                cmd.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                cmd.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
                cmd.Parameters.AddWithValue("@Id_Usuario_Creador", contrato.Id_Usuario_Creador);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
        }
        return res;
    }

    public int Baja(int id)
    {
        int res = -1;
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = "DELETE FROM CONTRATOS WHERE Id_Contrato = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        return res;
    }

    public int Modificacion(Contrato contrato)
    {
        int res = -1;
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE CONTRATOS SET 
                Fecha_Inicio=@Fecha_Inicio, 
                Fecha_Fin=@Fecha_Fin, 
                Monto=@Monto, 
                Id_Inmueble=@Id_Inmueble, 
                Id_Inquilino=@Id_Inquilino 
                WHERE Id_Contrato = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha_Inicio", contrato.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fecha_Fin", contrato.Fecha_Fin);
                cmd.Parameters.AddWithValue("@Monto", contrato.Monto);
                cmd.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                cmd.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
                cmd.Parameters.AddWithValue("@id", contrato.Id_Contrato);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        return res;
    }

    public IList<Contrato> ObtenerTodos()
    {
        var res = new List<Contrato>();
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT c.*, i.Direccion, inq.Nombre, inq.Apellido 
                           FROM CONTRATOS c
                           INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
                           INNER JOIN INQUILINOS inq ON c.Id_Inquilino = inq.Id_Inquilino";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Direccion = reader.GetString("Direccion")
                            },
                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido")
                            }
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }
    public Contrato ObtenerPorId(int id)
    {
        Contrato res = null;

        using (var conn = new MySqlConnection(connectionString))
        {
            var sql = @"
        SELECT c.Id_Contrato, c.Fecha_Inicio, c.Fecha_Fin, c.Monto,
               c.Id_Inmueble, c.Id_Inquilino,
               i.Direccion, i.Id_Propietario,
               p.Nombre AS PropNombre, p.Apellido AS PropApellido,
               inq.Nombre AS InqNombre, inq.Apellido AS InqApellido,
               uc.Nombre AS CreadorNombre, uc.Apellido AS CreadorApellido,
               uf.Nombre AS FinalizadorNombre, uf.Apellido AS FinalizadorApellido
        FROM CONTRATOS c
        INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
        INNER JOIN PROPIETARIOS p ON i.Id_Propietario = p.Id_Propietario
        INNER JOIN INQUILINOS inq ON c.Id_Inquilino = inq.Id_Inquilino
        LEFT JOIN USUARIOS uc ON c.Id_Usuario_Creador = uc.Id_Usuario
        LEFT JOIN USUARIOS uf ON c.Id_Usuario_Finalizador = uf.Id_Usuario
        WHERE c.Id_Contrato = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),

                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Direccion = reader.GetString("Direccion"),
                                Titular = new Propietario
                                {
                                    Id_Propietario = reader.GetInt32("Id_Propietario"),
                                    Nombre = reader.GetString("PropNombre"),
                                    Apellido = reader.GetString("PropApellido")
                                }
                            },

                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Nombre = reader.GetString("InqNombre"),
                                Apellido = reader.GetString("InqApellido")
                            },

                            UsuarioCreador = !reader.IsDBNull(reader.GetOrdinal("CreadorNombre")) ? new Usuario
                            {
                                Nombre = reader.GetString("CreadorNombre"),
                                Apellido = reader.GetString("CreadorApellido")
                            } : null,

                            UsuarioFinalizador = !reader.IsDBNull(reader.GetOrdinal("FinalizadorNombre")) ? new Usuario
                            {
                                Nombre = reader.GetString("FinalizadorNombre"),
                                Apellido = reader.GetString("FinalizadorApellido")
                            } : null
                        };
                    }
                }

                conn.Close();
            }
        }

        return res;
    }

    public int FinalizarContrato(int id, int idUsuarioFinalizador)
    {
        int res = -1;
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"UPDATE CONTRATOS 
                           SET Fecha_Fin = SYSDATE(), 
                               Id_Usuario_Finalizador = @idUsuarioFinalizador 
                           WHERE Id_Contrato = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idUsuarioFinalizador", idUsuarioFinalizador);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        return res;
    }

    public int CalcularMontoCancelacion(int id)
    {
        int dias_contrato = 0, dias_transcurridos = 0, monto = 0;
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT 
                            TIMESTAMPDIFF(DAY, Fecha_Inicio, Fecha_Fin) AS dias_contrato,
                            TIMESTAMPDIFF(DAY, Fecha_Inicio, SYSDATE()) AS dias_transcurridos,
                            Monto 
                           FROM CONTRATOS WHERE Id_Contrato = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dias_contrato = reader.GetInt32("dias_contrato");
                        dias_transcurridos = reader.GetInt32("dias_transcurridos");
                        monto = reader.GetInt32("Monto");
                    }
                }
                conn.Close();
            }
        }
        return dias_transcurridos > (dias_contrato / 2) ? monto : monto * 2;
    }

    public IList<Contrato> ObtenerContratosVigentes()
    {
        var res = new List<Contrato>();
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT c.*, i.Direccion, inq.Nombre, inq.Apellido 
                           FROM CONTRATOS c
                           INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
                           INNER JOIN INQUILINOS inq ON c.Id_Inquilino = inq.Id_Inquilino
                           WHERE c.Fecha_Inicio <= SYSDATE() AND c.Fecha_Fin >= SYSDATE()";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Direccion = reader.GetString("Direccion")
                            },
                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido")
                            }
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public IList<Contrato> ObtenerContratosPorInquilino(int id)
    {
        var res = new List<Contrato>();
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT c.*, i.Direccion, inq.Nombre, inq.Apellido 
                           FROM CONTRATOS c
                           INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
                           INNER JOIN INQUILINOS inq ON c.Id_Inquilino = inq.Id_Inquilino
                           WHERE c.Id_Inquilino = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Direccion = reader.GetString("Direccion")
                            },
                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido")
                            }
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public IList<Contrato> ObtenerContratosPorInmueble(ContratoBusqueda cb)
    {
        var res = new List<Contrato>();
        using (var conn = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT c.*, i.Direccion, inq.Nombre, inq.Apellido 
                           FROM CONTRATOS c
                           INNER JOIN INMUEBLES i ON c.Id_Inmueble = i.Id_Inmueble
                           INNER JOIN INQUILINOS inq ON c.Id_Inquilino = inq.Id_Inquilino
                           WHERE 1=1";

            if (cb.Id_Inmueble.HasValue)
                sql += " AND c.Id_Inmueble = @Id_Inmueble";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                if (cb.Id_Inmueble.HasValue)
                    cmd.Parameters.AddWithValue("@Id_Inmueble", cb.Id_Inmueble.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                                Direccion = reader.GetString("Direccion")
                            },
                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido")
                            }
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }
}
