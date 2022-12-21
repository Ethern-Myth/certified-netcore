namespace backend.models.responses;

public record DeliveryResponse
{
    public OrderResponse Order { get; }
    public bool IsDelivered { get; }
    public DeliveryResponse(
        OrderResponse order,
        bool isDelivered)
    {
        Order = order;
        IsDelivered = isDelivered;
    }
}
