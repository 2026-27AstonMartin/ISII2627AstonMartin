using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class Inscripcion
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public required string NombreUsuario { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 100 caracteres.")]
    public required string ApellidosUsuario { get; set; }

    [Required]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "El DNI debe tener 9 caracteres.")]
    [RegularExpression(@"^\d{8}[A-Za-z]$", ErrorMessage = "Formato de DNI no válido.")]
    public required string DNI { get; set; }

    [Required]
    [DataType(DataType.PhoneNumber)]
    [StringLength(15, MinimumLength = 9, ErrorMessage = "El teléfono debe tener entre 9 y 15 caracteres.")]
    public required string Telefono { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime FechaInscripcion { get; set; }

    [Required]
    public MetodoPago MetodoPago { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    public decimal PrecioTotal { get; set; }

    public IList<CompeticionInscrita> CompeticionesInscritas { get; set; } = new List<CompeticionInscrita>();
}
