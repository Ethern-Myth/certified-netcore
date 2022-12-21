using backend.models.responses;

namespace backend.models.response;

public record CustomerProductResponse
{
    public Guid CPID { get; }
    public Guid CustomerID { get; }
    public CustomerResponse CustomerResponse { get; }
    public ICollection<ProductQuantityResponse> Products { get; }
    public double Subtotal { get; }
    public CustomerProductResponse() { }

    public CustomerProductResponse(
        Guid cPID,
        Guid customerID,
        CustomerResponse customerResponse,
        ICollection<ProductQuantityResponse> products,
        double subtotal)
    {
        CPID = cPID;
        CustomerID = customerID;
        CustomerResponse = customerResponse;
        Products = products;
        Subtotal = subtotal;
    }

}

public record PartialCustomerProductResponse
{
    public CustomerResponse CustomerResponse { get; }
    public ICollection<ProductQuantityResponse> Products { get; }
    public double Subtotal { get; }
    public PartialCustomerProductResponse() { }

    public PartialCustomerProductResponse(
        CustomerResponse customerResponse,
        ICollection<ProductQuantityResponse> products,
        double subtotal)
    {
        CustomerResponse = customerResponse;
        Products = products;
        Subtotal = subtotal;
    }
}
