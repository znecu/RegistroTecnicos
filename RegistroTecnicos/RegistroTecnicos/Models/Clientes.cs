using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClientesId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "En este campo solo se permiten letras. ")]
    
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [StringLength(10, MinimumLength =10, ErrorMessage = "Ingrese un número de telefono válido. ")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
    public string? Whatsapp {  get; set; }
}
