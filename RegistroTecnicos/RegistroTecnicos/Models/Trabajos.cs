﻿using System.ComponentModel.DataAnnotations;
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
    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public int ClienteId { get; set; }
    [ForeignKey("ClienteId")]
    public Clientes? Clientes { get; set; }


    public int TecnicoId { get; set; }
    [ForeignKey("TecnicoId")]
    public Tecnicos? Tecnicos { get; set; }
}