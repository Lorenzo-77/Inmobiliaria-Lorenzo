using MySqlConnector;

namespace InmobiliariaLorenzo.Models;

    public class RepositorioUso : RepositorioBase, IRepositorioUso
    {
        public RepositorioUso(IConfiguration configuration) : base(configuration)
    {
        //https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/
    }

        public int Alta(Uso p)
        {
            throw new NotImplementedException();
        }

        public int Baja(int id)
        {
            throw new NotImplementedException();
        }

        public int Modificacion(Uso p)
        {
            throw new NotImplementedException();
        }

        public Uso ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }   

        public IList<Uso> ObtenerTodos()
        {
            var res = new List<Uso>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                var sql = "SELECT Id_Uso,Nombre FROM USOS";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(new Uso
                            {
                                Id_Uso = reader.GetInt32("Id_Uso"),
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