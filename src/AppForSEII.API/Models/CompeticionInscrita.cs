namespace AppForSEII.API.Models;

public class CompeticionInscrita
{
    [Key]
    public int Id { get; set; }

    [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
    public string? ProblemasFisicos { get; set; }

    public int CompeticionId { get; set; }

    public Competicion Competicion { get; set; } = null!;

    public int InscripcionCompeticionId { get; set; }

    public InscripcionCompeticion InscripcionCompeticion { get; set; } = null!;
}
