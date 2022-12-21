using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct ShippingRequest
{
    [Required]
    public Guid CustomerID { get; set; }
    [Required]
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    [Required]
    public string Suburb { get; set; }
    [Required]
    public string Town { get; set; }
    [Required]
    public string Region { get; set; }
    [Required]
    public int PostalCode { get; set; }
    [Required]
    public string Name { get; set; }
}
