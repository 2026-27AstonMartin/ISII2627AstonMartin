using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class Material
{
    [Key]
    public int IdMaterial { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    [Range(0.01, 99999999.99, ErrorMessage = "El precio debe ser mayor que 0.")]
    public decimal Precio { get; set; }

    //unidades disponibles para alquilar; si es 0 el material no esta disponible
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa.")]
    public int Cantidad { get; set; }

    public int TipoMaterialId { get; set; }

    [ForeignKey("TipoMaterialId")]
    public TipoMaterial TipoMaterial { get; set; } = null!;

    public int TipoDeporteId { get; set; }

    [ForeignKey("TipoDeporteId")]
    public TipoDeporte TipoDeporte { get; set; } = null!;

    public IList<MaterialAlquilado> MaterialesAlquilados { get; set; } = new List<MaterialAlquilado>();
}