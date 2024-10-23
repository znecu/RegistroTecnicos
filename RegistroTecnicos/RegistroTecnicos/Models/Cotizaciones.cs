using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Cotizaciones
{
    [Key]
    public int CotizacionId { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Este campo solo permite letras. ")]
    public string? Observacion { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Este campo solo permite números.")]
    [Range(minimum: 0.1, maximum: 500000, ErrorMessage = "Por favor, ingrese una cantidad mayor a 0 y menor o igual a $500,000. ")]
    public double Monto { get; set; }  

    public int ClienteId {get; set; }
    [ForeignKey("ClienteId")]
    public Clientes? Clientes { get; set; }

    public ICollection<CotizacionesDetalle> CotizacionesDetalles { get; set; } = new List<CotizacionesDetalle>();
}