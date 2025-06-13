using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;

public class Uso
{
    [Key]
    public int Id_Uso { get; set; } 
    [Required, Display(Name = "Uso del Inmueble")]
    public string? Nombre { get; set; }
    
}