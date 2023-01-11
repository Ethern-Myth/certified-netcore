using System.ComponentModel.DataAnnotations;
namespace backend.models.requests;

public struct ConversionRequest
{
    [Required(ErrorMessage = "Unit is required")]
    public string Unit { get; set; }
}
