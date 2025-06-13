using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;

public class Contrato
{
    [Key]
    [Display(Name = "N°")]
    public int Id_Contrato { get; set; }

    [Required, Display(Name = "Fecha de Inicio")]
    public DateTime Fecha_Inicio { get; set; }

    [Required, Display(Name = "Fecha de Finalización")]
    public DateTime Fecha_Fin { get; set; }

    [Required, Display(Name = "Monto Mensual")]
    public decimal Monto { get; set; }

    [Required, Display(Name = "Inmueble")]
    public int Id_Inmueble { get; set; }

    [ForeignKey(nameof(Id_Inmueble))]
    public Inmueble? Inmueble { get; set; }

    [Required, Display(Name = "Inquilino")]
    public int Id_Inquilino { get; set; }

    [ForeignKey(nameof(Id_Inquilino))]
    public Inquilino? Inquilino { get; set; }

    [Display(Name = "Creado por")]
    public int Id_Usuario_Creador { get; set; }

    [ForeignKey(nameof(Id_Usuario_Creador))]
    public Usuario? UsuarioCreador { get; set; }

    [Display(Name = "Finalizado por")]
    public int? Id_Usuario_Finalizador { get; set; }

    [ForeignKey(nameof(Id_Usuario_Finalizador))]
    public Usuario? UsuarioFinalizador { get; set; }
}
