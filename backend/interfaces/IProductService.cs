using backend.models.models;

namespace backend.interfaces;

public interface IProductService : IService<Product>
{
    public Task<ProductType?> GetProductTypes(int id);
    public Task<Conversion?> GetConversion(int id);
    public Task UpdateByStatus(Guid id, bool status);
}
