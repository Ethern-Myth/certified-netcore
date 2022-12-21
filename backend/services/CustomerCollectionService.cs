using backend.Data;
using backend.interfaces;
using backend.models;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class CustomerCollectionService : ICustomerCollectionService
{
    private readonly DataContext context;

    public CustomerCollectionService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.CustomerCollections.Single(cp => cp.CCID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<CustomerCollection>> getResponse() =>
        context.CustomerCollections
        .AsNoTracking()
        .ToListAsync();

    public Task<List<CustomerCollection>> getResponse(Guid? id, int? intId) =>
     context.CustomerCollections
        .Where(cc => cc.CCID == id
        || cc.CPID == id
        || cc.CustomerID == id)
        .AsNoTracking()
        .ToListAsync();

    public Task<CustomerProduct> GetCustomerProduct(Guid id) =>
        context.CustomerProducts
        .Where(cp => cp.CPID == id)
        .AsNoTracking()
        .FirstAsync();

    public Task<Customer> getCustomer(Guid id) =>
      context.Customers
      .Where(c => c.RoleID == c.Roles.RoleID)
      .Include(c => c.Roles)
      .FirstOrDefaultAsync(c => c.CustomerID == id);

    public async Task postRequest(CustomerCollection t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(CustomerCollection t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.CustomerCollections.Any(cp => cp.CCID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }

    public Task<CustomerCollection?> getSingleResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();
}
