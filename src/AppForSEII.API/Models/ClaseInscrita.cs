using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class ClaseInscrita
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1, 2, ErrorMessage = "Se pueden reservar como máximo 2 plazas.")]
    public int PlazasReservadas { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    public decimal Precio { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? Observaciones { get; set; }

    public int ClaseDeportivaId { get; set; }

    [ForeignKey("ClaseDeportivaId")]
    public ClaseDeportiva ClaseDeportiva { get; set; } = null!;

    public int InscripcionId { get; set; }

    [ForeignKey("InscripcionId")]
    public Inscripcion Inscripcion { get; set; } = null!;
}
