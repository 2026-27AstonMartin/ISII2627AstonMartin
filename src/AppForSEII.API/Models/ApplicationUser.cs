using Microsoft.AspNetCore.Identity;

namespace AppForSEII.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }
    public ApplicationUser(string id, string name, string surname, string userName)
    {
        Id = id;
        Name = name;
        Surname = surname;
        UserName = userName;
        Email = userName;
    }

    [StringLength(50)]
    public string? Name {get;set;}

    [StringLength(50)]
    public string? Surname {get;set;}

    [Required]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "El DNI debe tener 9 caracteres.")]
    [RegularExpression(@"^\d{8}[A-Za-z]$", ErrorMessage = "Formato de DNI no válido.")]
    public string DNI {get;set;} = string.Empty;

    [Required]
    [Range(16, 120, ErrorMessage = "La edad debe estar entre 16 y 120 años.")]
    public int Age {get;set;}

    [Required]
    [StringLength(20)]
    public string Sex {get;set;} = string.Empty;
}
