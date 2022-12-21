using backend.Data;
using backend.interfaces;
using backend.models;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class CustomerProductService : ICustomerProductService
{
    private readonly DataContext context;

    public CustomerProductService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.CustomerProducts.Single(cp => cp.CPID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<Product> getProduct(Guid id) =>
        context.Products
        .Where(p => p.PDTypeID == p.ProductType.PDTypeID)
        .Include(p => p.ProductType)
        .FirstOrDefaultAsync(p => p.ProductID == id);

    public Task<List<CustomerProduct>> getResponse() =>
        context.CustomerProducts
        .AsNoTracking()
        .ToListAsync();

    public Task<List<CustomerProduct>> getResponse(Guid? id, int? intId) =>
        context.CustomerProducts
        .Where(cp => cp.CPID == id
        || cp.CustomerID == id)
        .AsNoTracking()
        .ToListAsync();

    public Task<Customer> getCustomer(Guid id) =>
        context.Customers
        .Where(c => c.RoleID == c.Roles.RoleID)
        .Include(c => c.Roles)
        .FirstOrDefaultAsync(c => c.CustomerID == id);

    public async Task postRequest(CustomerProduct t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(CustomerProduct t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.CustomerProducts.Any(cp => cp.CPID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }

    public Task<CustomerProduct?> getSingleResponse(Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProduct(Guid id)
    {
        throw new NotImplementedException();
    }


}
