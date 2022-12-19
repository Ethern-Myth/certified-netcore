using backend.models;
using backend.models.models;

namespace backend.interfaces;

public interface ICustomerCollectionService : IService<CustomerCollection>
{
    public Task<Customer> getCustomer(Guid id);
    public Task<CustomerProduct> GetCustomerProduct(Guid id);
}
