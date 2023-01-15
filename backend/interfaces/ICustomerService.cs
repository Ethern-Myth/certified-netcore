using backend.models;

namespace backend.interfaces;

public interface ICustomerService : IService<Customer>
{
    public Task<Customer?> GetCurrentUser(string email, string password);
    public Task<bool> IsDuplicate(string email);
    public Task UpdateByStatus(Guid id, bool status);
}
