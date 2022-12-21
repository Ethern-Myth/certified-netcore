using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct ProductQuantityRequest
{
    [Required]
    public Guid ProductID { get; set; }
    [Required]
    public int Quantity { get; set; }
}
