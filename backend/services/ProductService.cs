using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class ProductService : IProductService
{
    private readonly DataContext context;

    public ProductService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Products.Single(p => p.ProductID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Product>> getResponse() =>
        context.Products
        .Where(p => p.PDTypeID == p.ProductType.PDTypeID
        && p.ConversionID == p.Conversion.ConversionID)
        .Include(p => p.ProductType)
        .ToListAsync();

    public Task<Product?> getSingleResponse(Guid? id, int? intId) =>
        context.Products
        .Where(p => p.PDTypeID == p.ProductType.PDTypeID
        && p.ConversionID == p.Conversion.ConversionID)
        .Include(p => p.ProductType)
        .FirstOrDefaultAsync(p => p.ProductID == id);

    public Task<ProductType?> GetProductTypes(int id) =>
        context.ProductTypes
        .FirstOrDefaultAsync(pt => pt.PDTypeID == id);

    public Task<Conversion?> GetConversion(int id) =>
         context.UnitConversions
        .FirstOrDefaultAsync(c => c.ConversionID == id);

    public async Task UpdateByStatus(Guid id, bool status)
    {
        context.ChangeTracker.Clear();
        if (context.Products.Any(p => p.ProductID == id))
        {
            var product = context.Products.SingleOrDefault(p => p.ProductID == id);
            product.InStock = status;
            await Task.Run(() => context.Update(product));
        }
        await context.SaveChangesAsync();
    }

    public async Task postRequest(Product t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Product t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Products.Any(p => p.ProductID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }

    public Task<List<Product>> getResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();

}
