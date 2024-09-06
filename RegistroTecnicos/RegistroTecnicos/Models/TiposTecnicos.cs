using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TiposTecnicosId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Este campo solo permite letras. ")]
    public string? Descripcion { get; set; }


}
