namespace AppForSEII.API.Models;

public class Material
{
    [Key]
    public int IdMaterial { get; set; }

    [Required, StringLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }

    [Required]
    public int Cantidad { get; set; }
}