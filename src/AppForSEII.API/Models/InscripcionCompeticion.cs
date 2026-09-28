using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class InscripcionCompeticion
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

    // Relacion 1 a N con Competicion: una inscripcion pertenece siempre a una competicion
    public int CompeticionId { get; set; }

    public Competicion Competicion { get; set; } = null!;

    // Relacion 0..1 a N con ApplicationUser: la inscripcion puede hacerse sin usuario registrado
    public string? UsuarioId { get; set; }

    public ApplicationUser? Usuario { get; set; }
}