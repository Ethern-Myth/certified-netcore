using backend.models.response;
namespace backend.models.responses;
public record OrderResponse
{
    public Guid OrderID { get; }
    public DateTimeOffset OrderDate { get; }
    public double OrderTotal { get; }
    public bool IsPaid { get; }
    public ShippingResponse ShippingResponse { get; }
    public OnlyProductsResponse OnlyProductsResponse { get; set; }
    public OrderResponse() { }
    public OrderResponse(
        Guid orderID,
        DateTimeOffset orderDate,
        double orderTotal,
        bool isPaid,
        ShippingResponse shippingResponse,
        OnlyProductsResponse onlyProductsResponse)
    {
        OrderID = orderID;
        OrderDate = orderDate;
        OrderTotal = orderTotal;
        IsPaid = isPaid;
        ShippingResponse = shippingResponse;
        OnlyProductsResponse = onlyProductsResponse;
    }
}