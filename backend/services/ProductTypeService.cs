using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class ProductTypeService : IProductTypeService
{
    private readonly DataContext context;

    public ProductTypeService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.ProductTypes.Single(p => p.PDTypeID == intId);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<ProductType>> getResponse() =>
        context.ProductTypes.ToListAsync();


    public Task<ProductType?> getSingleResponse(Guid? id, int? intId) =>
        context.ProductTypes.FirstOrDefaultAsync(p => p.PDTypeID == intId);

    public async Task postRequest(ProductType t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(ProductType t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.ProductTypes.Any(p => p.PDTypeID == intId))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }
    public Task<List<ProductType>> getResponse(Guid? id, int? intId) =>
    throw new NotImplementedException();

}
