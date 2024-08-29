using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    [Required]

    public string NombreTecnico { get; set; }
    [Required]

    public decimal SueldoHora { get; set; }
}
