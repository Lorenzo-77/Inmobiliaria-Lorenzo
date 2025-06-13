using InmobiliariaLorenzo.Controllers;

namespace InmobiliariaLorenzo.Models
{
    public interface IRepositorioPago : IRepositorio<Pago>
	{		
		IList<Pago> PagosPorContrato(int id);
		IList<Pago> PagosContratoPorInquilino(int id);
	}   
   
}