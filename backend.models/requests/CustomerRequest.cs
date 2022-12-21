using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;
public struct CustomerRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string? Phone { get; set; }
    [Required]
    public int RoleID { get; set; }
    [Required]
    public bool Status { get; set; }
    public CustomerRequest() =>
        Status = true;
}