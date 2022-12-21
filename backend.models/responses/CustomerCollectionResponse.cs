namespace backend.models.response;

public record CustomerCollectionResponse
{
    public Guid CCID { get; }
    public Guid CPID { get; }
    public Guid CustomerID { get; }
    public PartialCustomerProductResponse PartialCustomerProductResponse { get; }
    public CustomerCollectionResponse() { }
    public CustomerCollectionResponse(
        Guid cCID,
        Guid cPID,
        Guid customerID,
        PartialCustomerProductResponse partialCustomerProductResponse)
    {
        CCID = cCID;
        CPID = cPID;
        CustomerID = customerID;
        PartialCustomerProductResponse = partialCustomerProductResponse;
    }
}

