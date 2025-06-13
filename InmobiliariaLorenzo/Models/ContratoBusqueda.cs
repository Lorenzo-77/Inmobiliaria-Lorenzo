using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;

public class ContratoBusqueda
{
    [Display(Name = "Fecha de Inicio")]
    public DateTime? Fecha_Inicio { get; set; }

    [Display(Name = "Fecha de Finalización")]
    public DateTime? Fecha_Fin { get; set; }

    [Display(Name = "Inmueble")]
    public int? Id_Inmueble{ get; set; }

}