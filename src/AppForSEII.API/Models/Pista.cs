namespace AppForSEII.API.Models;

public class Pista
{
    [Key]
    public int IdPista { get; set; }

    [Required, StringLength(50)]
    public string NombrePista { get; set; } = string.Empty;

    [Required]
    public int NPersonas { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }

    [Required]
    public int Stock { get; set; }

    public int TipoDeporteId { get; set; }
    public TipoDeporte TipoDeporte { get; set; } = null!;
}
