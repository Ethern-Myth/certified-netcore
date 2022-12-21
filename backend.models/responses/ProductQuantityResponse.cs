using backend.models.models;

namespace backend.models.responses;

public record ProductQuantityResponse
{
    public int Quantity { get; }
    public double ProductTotal { get; }
    public Product Product { get; }

    public ProductQuantityResponse() { }
    public ProductQuantityResponse(
        int quantity,
        double productTotal,
        Product product)
    {
        Quantity = quantity;
        ProductTotal = productTotal;
        Product = product;
    }
}
