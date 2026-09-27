namespace AppForSEII.API.Models
{
    public class Competicion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public int Plazas { get; set; }

        [Required]
        [StringLength(100)]
        public string Lugar { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

    }
}
