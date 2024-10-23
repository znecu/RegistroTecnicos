using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Este campo obligatorio. ")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "En este campo solo se permiten letras. ")]
    public string? Nombres { get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [Range(minimum: 0.1, maximum: 500000, ErrorMessage = "Por favor, ingrese una cantidad mayor a 0 y menor o igual a $500,000. ")]

    public double SueldoHora { get; set; }

    [ForeignKey("TiposTecnicosId")]
    public int TiposTecnicosId {  get; set; }
    public TiposTecnicos? TiposTecnicos { get; set; }
}

