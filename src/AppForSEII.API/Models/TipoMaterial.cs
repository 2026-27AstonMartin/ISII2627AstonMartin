namespace AppForSEII.API.Models;
public class TipoMaterial
{
    [Key]
    public int IdTipoMaterial { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre del tipo de material debe tener entre 2 y 50 caracteres.")]
    public string NombreTipoMaterial { get; set; } = string.Empty;
}