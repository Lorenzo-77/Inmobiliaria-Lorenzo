using System.Drawing;
using MySql.Data.MySqlClient;

namespace InmobiliariaLorenzo.Models;

public class RepositorioUsuario : RepositorioBase, IRepositorioUsuario
{
    public RepositorioUsuario(IConfiguration configuration) : base(configuration)
    {
        //https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
    }
    public int Alta(Usuario usuario)
    {
        var res = -1;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"INSERT INTO USUARIOS(Apellido,Nombre,Mail,Password,Rol,Avatar)
            VALUES (@Apellido,@Nombre,@Mail,@Password,@Rol,@Avatar);
            SELECT LAST_INSERT_ID()";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Mail", usuario.Mail);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                if (string.IsNullOrEmpty(usuario.Avatar))
                {
                    cmd.Parameters.AddWithValue("@Avatar", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                }

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                usuario.Id_Usuario = res;
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
            var sql = @"DELETE FROM USUARIOS
            WHERE Id_Usuario = @id";

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

    public int Modificacion(Usuario usuario)
    {
        var res = -2;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE USUARIOS
            SET Apellido=@Apellido,Nombre=@Nombre,Mail=@Mail,Rol=@Rol,Avatar=@Avatar 
            WHERE Id_Usuario = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Mail", usuario.Mail);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                cmd.Parameters.AddWithValue("@id", usuario.Id_Usuario);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }
    }

    public IList<Usuario> ObtenerTodos()
    {
        var res = new List<Usuario>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            //Uso IFNULL(Avatar, '') por que me da error al traer los valores NULOS en las listas

            var sql = "SELECT Id_Usuario,Apellido,Nombre,Mail,Password,Rol,IFNULL(Avatar, '') Avatar FROM USUARIOS";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Usuario
                        {
                            Id_Usuario = reader.GetInt32("Id_Usuario"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Mail = reader.GetString("Mail"),
                            Password = reader.GetString("Password"),
                            Rol = reader.GetInt32("Rol"),
                            Avatar = reader.GetString("Avatar"),
                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public Usuario ObtenerPorId(int id)
    {
        var res = new Usuario();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @" SELECT Id_Usuario,Apellido,Nombre,Mail,Password,Rol,IFNULL(Avatar, '') Avatar  
            FROM USUARIOS
            WHERE Id_Usuario = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = (new Usuario
                        {
                            Id_Usuario = reader.GetInt32("Id_Usuario"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Mail = reader.GetString("Mail"),
                            Password = reader.GetString("Password"),
                            Rol = reader.GetInt32("Rol"),
                            Avatar = reader.GetString("Avatar"),

                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public Usuario ObtenerPorEmail(string mail)
    {
        var res = new Usuario();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @" SELECT Id_Usuario,Apellido,Nombre,Mail,Password,Rol,IFNULL(Avatar, '') Avatar
            FROM USUARIOS
            WHERE Mail = @mail";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@mail", mail);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res = (new Usuario
                        {
                            Id_Usuario = reader.GetInt32("Id_Usuario"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Mail = reader.GetString("Mail"),
                            Password = reader.GetString("Password"),
                            Rol = reader.GetInt32("Rol"),
                            Avatar = reader.GetString("Avatar"),

                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public int CambiarPassword(int id, String pass)
    {
        var res = -2;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE USUARIOS
            SET Password=@Password 
            WHERE Id_Usuario = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Password", pass);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }
    }

}

