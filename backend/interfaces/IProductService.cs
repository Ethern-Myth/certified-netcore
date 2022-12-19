using backend.models.models;

namespace backend.interfaces;

public interface IProductService : IService<Product>
{
    public Task<ProductType?> GetProductTypes(int id);
}
