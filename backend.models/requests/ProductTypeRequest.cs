using System.ComponentModel.DataAnnotations;
namespace backend.models.requests;

public struct ProductTypeRequest
{
    [Required]
    public string Category { get; set; }
}
