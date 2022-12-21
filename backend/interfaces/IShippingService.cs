using backend.models;
using backend.models.models;

namespace backend.interfaces
{
    public interface IShippingService : IService<Shipping>
    {
        public Task<Customer> getCustomer(Guid id);
        public Task<Country> getCountry(string name);
    }
}