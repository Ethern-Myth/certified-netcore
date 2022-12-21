using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct CustomerProductRequest
{
    [Required]
    public Guid CustomerID { get; set; }
    [Required]
    public ICollection<ProductQuantityRequest> Products { get; set; }
    public CustomerProductRequest() =>
        Products = new List<ProductQuantityRequest>();
}
