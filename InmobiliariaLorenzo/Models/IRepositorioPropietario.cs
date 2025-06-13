using InmobiliariaLorenzo.Controllers;

namespace InmobiliariaLorenzo.Models
{
    public interface IRepositorioPropietario : IRepositorio<Propietario>
	{
		Propietario ObtenerPorEmail(string email);
		IList<Propietario> BuscarPorNombre(string nombre);
		IList<Propietario> ObtenerLista(int paginaNro, int tamPagina);
		int ObtenerCantidad();
	}    
   
}