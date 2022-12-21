using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct DeliveryRequest
{
    [Required]
    public Guid OrderID { get; set; }
    [Required]
    public bool IsDelivered { get; set; }
}
