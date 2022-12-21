using backend.models.models;

namespace backend.models.responses;

public record ShippingResponse
{
    public int ShippingID { get; }
    public CustomerResponse CustomerResponse { get; }
    public string AddressLine1 { get; }
    public string? AddressLine2 { get; }
    public string Suburb { get; }
    public string Town { get; }
    public string Region { get; }
    public int PostalCode { get; }
    public Country Country { get; }

    public ShippingResponse() { }
    public ShippingResponse(
        int shippingID,
        CustomerResponse customerResponse,
        string addressLine1,
        string? addressLine2,
        string suburb,
        string town,
        string region,
        int postalCode,
        Country country)
    {
        ShippingID = shippingID;
        CustomerResponse = customerResponse;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Suburb = suburb;
        Town = town;
        Region = region;
        PostalCode = postalCode;
        Country = country;
    }
}
