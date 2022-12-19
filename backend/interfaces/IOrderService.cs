using backend.models.models;

namespace backend.interfaces;

public interface IOrderService : IService<Order>
{
    public Task<CustomerCollection> getCollection(Guid id);
}
