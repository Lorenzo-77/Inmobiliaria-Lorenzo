using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;

public class Pago
{
    [Key]
    [Display(Name = "N°")]
    public int Id_Pago { get; set; } 

    [Required, Display(Name = "Fecha de Pago")]
    public DateTime Fecha { get; set; }


    [Required]
    public Decimal Importe { get; set; }


    [Required,Display(Name ="Contrato")]
    public int Id_Contrato{ get; set; }
    [ForeignKey(nameof(Id_Contrato))]
    public Contrato? Contrato { get; set; }

}