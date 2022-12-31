using System.ComponentModel.DataAnnotations;
namespace backend.models.requests;

public struct RoleRequest
{
    [Required(ErrorMessage = "Role name is required")]
    public string Name { get; set; }
}
