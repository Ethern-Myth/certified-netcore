using System.ComponentModel.DataAnnotations;
namespace backend.models.requests;

public struct ProductTypeRequest
{
    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; }
}
