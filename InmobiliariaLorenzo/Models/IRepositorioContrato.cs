namespace InmobiliariaLorenzo.Models
{
    public interface IRepositorioContrato : IRepositorio<Contrato>
	{				
		IList<Contrato> ObtenerContratosVigentes();
		IList<Contrato> ObtenerContratosPorInmueble(ContratoBusqueda cb);    
		IList<Contrato> ObtenerContratosPorInquilino(int id);    		
		int CalcularMontoCancelacion(int id);
		int FinalizarContrato(int id, int idUsuarioFinalizador);
	}   
}
