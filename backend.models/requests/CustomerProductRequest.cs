using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct CustomerProductRequest
{
    [Required(ErrorMessage = "Customer Id is required")]
    public Guid CustomerID { get; set; }
    [Required(ErrorMessage = "Products are required")]
    public ICollection<ProductQuantityRequest> Products { get; set; }
    public CustomerProductRequest() =>
        Products = new List<ProductQuantityRequest>();
}
