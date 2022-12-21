using backend.models;
using backend.models.models;

namespace backend.interfaces;

public interface IOrderService : IService<Order>
{
    public Task<CustomerCollection> getCollection(Guid id);
    public Task<Shipping> getShippingAddress(Guid id);
    public Task<Customer> getCustomer(Guid id);
}
