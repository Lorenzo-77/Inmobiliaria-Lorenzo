using System.ComponentModel.DataAnnotations;
namespace InmobiliariaLorenzo.Models;

public class Inquilino
{
    [Key]
    [Display(Name = "Código")]
    public int Id_Inquilino { get; set; }
    [Required]
    public string? Apellido { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Dni { get; set; }
    public string? Telefono { get; set; }

}
