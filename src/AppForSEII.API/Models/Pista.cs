namespace AppForSEII.API.Models;

public class Pista
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public int Aforo { get; set; }

    [Required, StringLength(30)]
    public string TipoDeporte { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecioPorDia { get; set; }
}
