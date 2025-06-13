using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;


public class InmuebleBusqueda
{
  public int? Ambientes { get; set; }

  public decimal? Precio { get; set; }

  [Display(Name = "Uso")]
  public int? Id_Uso { get; set; }

  [Display(Name = "Tipo")]
  public int? Id_Tipo { get; set; }

  [Display(Name = "Fecha de Inicio")]
  public DateTime Fecha_Inicio { get; set; }

  [Display(Name = "Fecha de Finalización")]
  public DateTime Fecha_Fin { get; set; }

  [Display(Name = "Titular")]
  public int? Id_Propietario { get; set; }

  public int? Activo { get; set; }
}