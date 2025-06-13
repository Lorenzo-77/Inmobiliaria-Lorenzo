using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaLorenzo.Models;

public class Login
{
    [DataType(DataType.EmailAddress)]
    [Display(Name ="Email")]
    public string Mail { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
    
}

