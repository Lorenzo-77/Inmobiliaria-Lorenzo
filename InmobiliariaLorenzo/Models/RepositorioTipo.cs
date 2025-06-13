using MySqlConnector;

namespace InmobiliariaLorenzo.Models;

    public class RepositorioTipo : RepositorioBase, IRepositorioTipo
    {
        public RepositorioTipo(IConfiguration configuration) : base(configuration)
    {
        //https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
    }

        public int Alta(Tipo p)
        {
            throw new NotImplementedException();
        }

        public int Baja(int id)
        {
            throw new NotImplementedException();
        }

        public int Modificacion(Tipo p)
        {
            throw new NotImplementedException();
        }

        public Tipo ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Tipo> ObtenerTodos()
        {
            var res = new List<Tipo>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                var sql = "SELECT Id_Tipo,Nombre FROM TIPOS";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(new Tipo
                            {
                                Id_Tipo = reader.GetInt32("Id_Tipo"),
                                Nombre = reader.GetString("Nombre"),

                            });
                        }
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }
