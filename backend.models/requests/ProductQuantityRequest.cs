using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct ProductQuantityRequest
{
    [Required(ErrorMessage = "Product Id is required")]
    public Guid ProductID { get; set; }
    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
}
