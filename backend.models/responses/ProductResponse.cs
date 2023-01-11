using backend.models.models;

namespace backend.models.responses;

public record ProductResponse
{
    public Guid ProductID { get; }
    public string Name { get; }
    public string Desc { get; }
    public string Brand { get; }
    public double Price { get; }
    public bool InStock { get; }
    public ProductType ProductType { get; }
    public double? ConversionSize { get; }
    public Conversion Conversion { get; }
    public string ImageName { get; }
    public string ImageUrl { get; }

    public ProductResponse() { }

    public ProductResponse(
        Guid productID,
        string name,
        string desc,
        string brand,
        double price,
        bool inStock,
        ProductType productType,
        double? conversionSize,
        Conversion conversion,
        string imageName,
        string imageUrl)
    {
        ProductID = productID;
        Name = name;
        Desc = desc;
        Brand = brand;
        Price = price;
        InStock = inStock;
        ProductType = productType;
        ConversionSize = conversionSize;
        Conversion = conversion;
        ImageName = imageName;
        ImageUrl = imageUrl;
    }
}
