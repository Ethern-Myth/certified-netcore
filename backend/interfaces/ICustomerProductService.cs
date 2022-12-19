using backend.models;
using backend.models.models;

namespace backend.interfaces;

public interface ICustomerProductService : IService<CustomerProduct>
{
    public Task<Product> getProduct(Guid id);
    public Task<Customer> getCustomer(Guid id);
}
