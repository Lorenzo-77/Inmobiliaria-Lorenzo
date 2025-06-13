using System.ComponentModel.DataAnnotations;

namespace InmobiliariaLorenzo.Models
{

public class Propietario
{
    [Key]
    [Display(Name = "Código")]
    public int Id_Propietario { get; set; }

    [Required]
    public string? Apellido { get; set; }
    [Required]
    public string? Nombre { get; set; }

    [Required]
    public string? Dni { get; set; }
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    [Required(ErrorMessage = "La clave es obligatoria"), DataType(DataType.Password)]
    public string? Clave { get; set; }
}

}
