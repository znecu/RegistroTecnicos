using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Prioridades
{
    [Key]
    public int PrioridadId { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Este campo solo permite letras.")]
    public string? Descripcion { set; get; }


    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Este campo solo permite números.")]
    public decimal Tiempo { get; set; }
}
