using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Este campo solo permite letras. ")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage ="Este campo es obligatorio. ")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Este campo solo permite números.")]
    [Range(minimum: 0.1, maximum: 500000, ErrorMessage = "Por favor, ingrese una cantidad mayor a 0 y menor o igual a $500,000. ")]
    public decimal Monto { get; set; }


    public DateTime Fecha { get; set; }

    //fk
    public int ClienteId { get; set; }
    [ForeignKey("ClienteId")]

    public Clientes? Clientes { get; set; }

    public int TecnicoId { get; set; }
    [ForeignKey("TecnicoId")]

    public Tecnicos? Tecnicos { get; set; }

    public int PrioridadId { get; set; }
    [ForeignKey("PrioridadId")]

    public Prioridades? Prioridades { get; set;}

    public ICollection<TrabajoDetalle> TrabajoDetalle { get; set; }
}
