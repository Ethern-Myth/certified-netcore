using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct ShippingRequest
{
    [Required(ErrorMessage = "Customer Id is required")]
    public Guid CustomerID { get; set; }
    [Required(ErrorMessage = "Address Line 1 is required")]
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    [Required(ErrorMessage = "Suburb is required")]
    public string Suburb { get; set; }
    [Required(ErrorMessage = "Town is required")]
    public string Town { get; set; }
    [Required(ErrorMessage = "Region is required")]
    public string Region { get; set; }
    [Required(ErrorMessage = "Postal Code is required")]
    public int PostalCode { get; set; }
    [Required(ErrorMessage = "Country is required")]
    public string Name { get; set; }
}
