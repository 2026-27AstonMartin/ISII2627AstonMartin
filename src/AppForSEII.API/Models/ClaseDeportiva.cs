using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class ClaseDeportiva
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 200 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime FechaHora { get; set; }

    [Required]
    [StringLength(100)]
    public string Monitor { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Nivel { get; set; } = string.Empty;

    //el lugar es opcional, puede ser una pista del pabellon
    [StringLength(100)]
    public string? Lugar { get; set; }

    [Required]
    [Range(1, 30, ErrorMessage = "Las plazas disponibles deben estar entre 1 y 30.")]
    public int PlazasDisponibles { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    public decimal PrecioUnitario { get; set; }

    public int TipoDeporteId { get; set; }

    public TipoDeporte TipoDeporte { get; set; } = null!;

    
    public IList<ClaseInscrita> ClasesInscritas { get; set; } = new List<ClaseInscrita>();
}
