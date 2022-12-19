using System.ComponentModel.DataAnnotations;
namespace backend.models.requests;

public struct RoleRequest
{

    [Required]
    public string Name { get; set; }
}
