using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct DeliveryRequest
{
    [Required(ErrorMessage = "Order Id is required")]
    public Guid OrderID { get; set; }
    [Required(ErrorMessage = "Is delivered is required")]
    public bool IsDelivered { get; set; }
}
