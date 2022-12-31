using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;
public struct CustomerRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; }
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public string? Phone { get; set; }
    [Required(ErrorMessage = "Role Id is required")]
    public int RoleID { get; set; }
    [Required(ErrorMessage = "Status is required")]
    public bool Status { get; set; }
    public CustomerRequest() =>
        Status = true;
}