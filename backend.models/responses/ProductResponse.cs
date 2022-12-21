using backend.models.models;

namespace backend.models.responses;

public record ProductResponse
{
    public Guid ProductID { get; }
    public string Name { get; }
    public string Desc { get; }
    public string Brand { get; }
    public double Price { get; }
    public bool InStock { get; set; }
    public ProductType ProductType { get; set; }
    public string ProductImgPath { get; set; }

    public ProductResponse() { }

    public ProductResponse(
        Guid productID,
        string name,
        string desc,
        string brand,
        double price,
        bool inStock,
        ProductType productType,
        string productImgPath)
    {
        ProductID = productID;
        Name = name;
        Desc = desc;
        Brand = brand;
        Price = price;
        InStock = inStock;
        ProductType = productType;
        ProductImgPath = productImgPath;
    }
}
