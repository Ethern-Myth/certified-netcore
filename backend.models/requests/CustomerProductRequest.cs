using System.ComponentModel.DataAnnotations;
using backend.models.models;

namespace backend.models.requests;

public struct CustomerProductRequest
{
    [Required]
    public Guid CustomerID { get; set; }

    [Required]
    public ICollection<Guid> Products { get; set; }

    public CustomerProductRequest() =>
        Products = new List<Guid>();
}