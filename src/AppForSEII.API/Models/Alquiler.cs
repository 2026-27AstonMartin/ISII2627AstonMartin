using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class Alquiler
{
    [Key]
    public int IdAlquiler { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 100 caracteres.")]
    public string ApellidosUsuario { get; set; } = string.Empty;

    [Required]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "El DNI debe tener 9 caracteres.")]
    [RegularExpression(@"^\d{8}[A-Za-z]$", ErrorMessage = "Formato de DNI no válido.")]
    public string DNI { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.PhoneNumber)]
    [StringLength(15, MinimumLength = 9, ErrorMessage = "El teléfono debe tener entre 9 y 15 caracteres.")]
    public string NumeroTelefono { get; set; } = string.Empty;

    //fecha en la que el cliente quiere alquilar el material
    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaAlquiler { get; set; }

    [Required]
    public MetodoPago MetodoPago { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    public decimal PrecioTotal { get; set; }
}