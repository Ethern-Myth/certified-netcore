using backend.Data;
using backend.interfaces;
using backend.models;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class OrderService : IOrderService
{
    private readonly DataContext context;

    public OrderService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Orders.Single(o => o.OrderID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Order>> getResponse() =>
        context.Orders.AsNoTracking().ToListAsync();

    public Task<List<Order>> getResponse(Guid? id, int? intId) =>
        context.Orders
        .Where(o => o.OrderID == id
        || o.CCID == id)
        .AsNoTracking()
        .ToListAsync();

    public Task<CustomerCollection> getCollection(Guid id) =>
    (from cc in context.CustomerCollections
     join c in context.Customers
     on cc.CustomerID equals c.CustomerID
     join cp in context.CustomerProducts
     on cc.CPID equals cp.CPID
     where cc.CustomerID == c.CustomerID
     && cc.CPID == cp.CPID
     && cc.CCID == id
     select new CustomerCollection(
        cc.CCID,
        cc.CustomerID,
        new Customer(
         cp.Customer.Name,
         cp.Customer.Surname,
         cp.Customer.Email,
         cp.Customer.Phone
        ),
        cc.CPID,
        new CustomerProduct(
        cc.CustomerProduct.Products,
        cc.CustomerProduct.Subtotal
        ),
        cc.IsAvailable
     )
        )
        .AsNoTracking()
        .FirstAsync();

    public async Task postRequest(Order t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Order t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Orders.Any(o => o.OrderID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }
    public Task<Order?> getSingleResponse(Guid? id, int? intId) =>
       throw new NotImplementedException();


}
