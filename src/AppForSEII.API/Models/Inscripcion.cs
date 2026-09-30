using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII.API.Models;

public class Inscripcion
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime FechaInscripcion { get; set; }

    [Required]
    public MetodoPago MetodoPago { get; set; }

    //numero de tarjeta, telefono del bizum, etc.
    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Los datos del pago deben tener entre 4 y 100 caracteres.")]
    public string DatosPago { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Currency)]
    [Precision(10, 2)]
    public decimal PrecioTotal { get; set; }

    //el cliente aporta nombre, apellidos, DNI, correo-e y telefono
    public string ClienteId { get; set; } = string.Empty;

    public ApplicationUser Cliente { get; set; } = null!;

    // Descomentar al crear la clase ClaseInscrita:
    // public IList<ClaseInscrita> ClasesInscritas { get; set; } = new List<ClaseInscrita>();
}
