using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

[PrimaryKey(nameof(IdMaterial), nameof(IdAlquiler))]
public class MaterialAlquilado
{
    public int IdMaterial { get; set; }

    [ForeignKey("IdMaterial")]
    public Material Material { get; set; } = null!;

    public int IdAlquiler { get; set; }

    [ForeignKey("IdAlquiler")]
    public Alquiler Alquiler { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima a alquilar es 1.")]
    public int Cantidad { get; set; }

    //precio unitario del material en el momento del alquiler
    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    public decimal Precio { get; set; }

    //descripcion opcional de lo que el cliente va a hacer con el material
    [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
    public string? Descripcion { get; set; }
}