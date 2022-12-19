namespace backend.models.response;

public record CustomerCollectionResponse
{
    public Guid CCID { get; }
    public Guid CPID { get; }
    public Guid CustomerID { get; }
    public PartialCustomerProductResponse PartialCustomerProductResponse { get; }
    public bool IsAvailable { get; }

    public CustomerCollectionResponse() { }
    public CustomerCollectionResponse(
        Guid cCID,
        Guid cPID,
        Guid customerID,
        PartialCustomerProductResponse partialCustomerProductResponse,
        bool isAvailable)
    {
        CCID = cCID;
        CPID = cPID;
        CustomerID = customerID;
        PartialCustomerProductResponse = partialCustomerProductResponse;
        IsAvailable = isAvailable;
    }
}

public record PartialCustomerCollectionResponse
{
    public Guid CPID { get; }
    public Guid CustomerID { get; }
    public PartialCustomerProductResponse PartialCustomerProductResponse { get; }
    public bool IsAvailable { get; }
    public PartialCustomerCollectionResponse() { }
    public PartialCustomerCollectionResponse(
        Guid cPID,
        Guid customerID,
        PartialCustomerProductResponse partialCustomerProductResponse,
        bool isAvailable)
    {
        CPID = cPID;
        CustomerID = customerID;
        PartialCustomerProductResponse = partialCustomerProductResponse;
        IsAvailable = isAvailable;
    }
}
