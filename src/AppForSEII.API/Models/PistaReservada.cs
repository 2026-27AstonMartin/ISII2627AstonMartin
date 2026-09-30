namespace AppForSEII.API.Models;

public class PistaReservada
{
    [Key]
    public int ID { get; set; }

    [Required]
    public int Cantidad { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }

    [StringLength(200)]
    public string? Observaciones { get; set; }

    public int IdPista { get; set; }

    [ForeignKey("IdPista")]
    public Pista Pista { get; set; } = null!;

    public int IdReserva { get; set; }

    [ForeignKey("IdReserva")]
    public Reserva Reserva { get; set; } = null!;
}
