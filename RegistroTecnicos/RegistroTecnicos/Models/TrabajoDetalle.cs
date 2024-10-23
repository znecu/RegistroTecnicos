using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class TrabajoDetalle
{
    [Key]
    public int DetalleId { get; set; }

    [Required]
    public int Cantidad { get; set; }

    public double Costo { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio. ")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Este campo solo permite números.")]
    [Range(minimum: 0.1, maximum: 500000, ErrorMessage = "Por favor, ingrese una cantidad mayor a 0 y menor o igual a $500,000. ")]
    public double Precio { get; set; }

    //fk
    public int ArticuloId { get; set; }
    [ForeignKey("ArticuloId")]
    public Articulos? Articulos { get; set; }

    public int TrabajoId { get; set; }
    [ForeignKey("TrabajoId")]
    public Trabajos? Trabajos { get; set; }

}
