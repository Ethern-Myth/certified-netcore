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
        (from cc in context.CustomerCollections
         join c in context.Customers
         on cc.CustomerID equals c.CustomerID
         join cp in context.CustomerProducts
         on cc.CPID equals cp.CPID
         where cc.CustomerID == c.CustomerID
         && cc.CPID == cp.CPID
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
        .ToListAsync();

    public Task<List<CustomerCollection>> getResponse(Guid? id, int? intId) =>
     (from cc in context.CustomerCollections
      join c in context.Customers
      on cc.CustomerID equals c.CustomerID
      join cp in context.CustomerProducts
      on cc.CPID equals cp.CPID
      where cc.CustomerID == c.CustomerID
      && cc.CPID == cp.CPID
      && cc.CCID == id
      || cc.CPID == id
      || cc.CustomerID == id
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
        .ToListAsync();

    public Task<CustomerProduct> GetCustomerProduct(Guid id) =>
   (from cp in context.CustomerProducts
    join c in context.Customers
    on cp.CustomerID equals c.CustomerID
    join r in context.Roles
    on c.RoleID equals r.RoleID
    where cp.CustomerID == c.CustomerID
    && c.RoleID == r.RoleID
    && cp.CPID == id
    || cp.CustomerID == id
    select new CustomerProduct(
    cp.CPID,
    cp.CustomerID,
    new Customer(
        cp.Customer.CustomerID,
        cp.Customer.Name,
        cp.Customer.Surname,
        cp.Customer.Email,
        cp.Customer.Phone
    ),
    cp.Products,
    cp.Subtotal
    )
        )
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

    public Task<CustomerCollection?> getSingleResponse(Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }


}
