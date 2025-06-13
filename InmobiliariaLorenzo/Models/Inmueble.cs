using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;

public class Inmueble
{
    [Key]
    [Display(Name = "N°")]
    public int Id_Inmueble { get; set; } // ? = permite valores nulos

    [Required, Display(Name = "Dirección")]
    public string? Direccion { get; set; }

    [Required]
    public int Ambientes { get; set; }

    [Required]
    public decimal Latitud { get; set; }

    [Required]
    public decimal Longitud { get; set; }

    [Required]
    public decimal Precio { get; set; }

    [Required]
    public bool? Activo { get; set; }

    [Display(Name = "Uso")]
    public int Id_Uso { get; set; }
    [ForeignKey(nameof(Id_Uso))]
    public Uso? Uso { get; set; }

    [Display(Name = "Tipo")]
    public int Id_Tipo { get; set; }
    [ForeignKey(nameof(Id_Tipo))]
    public Tipo? Tipo { get; set; }

    [Display(Name = "Titular")]
    public int Id_Propietario { get; set; }
    [ForeignKey(nameof(Id_Propietario))]
    public Propietario? Titular { get; set; }
    
}