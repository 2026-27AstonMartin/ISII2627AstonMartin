namespace AppForSEII.API.Models;

public class Reserva
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string NombreCliente { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required, StringLength(9)]
    public string Dni { get; set; } = string.Empty;

    [Required]
    public DateTime FechaReserva { get; set; }

    [Required]
    public MetodoPago MetodoPago { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecioTotal { get; set; }

    public IList<PistaReservada> PistasReservadas { get; set; } = new List<PistaReservada>();
}
