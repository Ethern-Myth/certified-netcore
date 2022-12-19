using System.ComponentModel.DataAnnotations;

namespace backend.models.models;

public class Login
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
