using backend.models.response;

namespace backend.models.responses;
public record OrderResponse
{
    public Guid OrderID { get; }
    public DateTimeOffset OrderDate { get; }
    public double OrderTotal { get; }
    public bool IsPaid { get; }
    public Guid CCID { get; }
    public PartialCustomerCollectionResponse customerCollectionResponse { get; }
    public OrderResponse() { }
    public OrderResponse(
        Guid orderID,
        DateTimeOffset orderDate,
        double orderTotal,
        bool isPaid,
        Guid cCID,
        PartialCustomerCollectionResponse customerCollectionResponse)
    {
        OrderID = orderID;
        OrderDate = orderDate;
        OrderTotal = orderTotal;
        IsPaid = isPaid;
        CCID = cCID;
        this.customerCollectionResponse = customerCollectionResponse;
    }

}