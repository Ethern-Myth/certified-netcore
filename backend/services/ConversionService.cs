using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class ConversionService : IConversionService
{
    private readonly DataContext context;
    public ConversionService(DataContext context) =>
        this.context = context;
    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.UnitConversions.Single(c => c.ConversionID == intId);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Conversion>> getResponse() =>
        context.UnitConversions.ToListAsync();

    public Task<Conversion?> getSingleResponse(Guid? id, int? intId) =>
        context.UnitConversions.FirstOrDefaultAsync(c => c.ConversionID == intId);

    public async Task postRequest(Conversion t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Conversion t, Guid? id, int? intId) 
    {
        context.ChangeTracker.Clear();
        if (context.UnitConversions.Any(c => c.ConversionID == intId))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }
    
    public Task<List<Conversion>> getResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();
}
