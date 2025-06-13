using InmobiliariaLorenzo.Controllers;

namespace InmobiliariaLorenzo.Models
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
	{		
		Usuario ObtenerPorEmail(string mail);
		int CambiarPassword(int id, String pass);
	}   
   
}