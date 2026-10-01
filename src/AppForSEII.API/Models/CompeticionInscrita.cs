namespace AppForSEII.API.Models;

[PrimaryKey(nameof(CompeticionId), nameof(InscripcionId))]
public class CompeticionInscrita
{
    public int CompeticionId { get; set; }

    public Competicion Competicion { get; set; } = null!;

    public int InscripcionId { get; set; }

    public Inscripcion Inscripcion { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Los problemas físicos no pueden superar los 500 caracteres.")]
    public string? ProblemasFisicos { get; set; }
}
