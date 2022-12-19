using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class RolesService : IRolesService
{
    private readonly DataContext context;
    public RolesService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Roles.Single(r => r.RoleID == intId);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Roles>> getResponse() =>
        context.Roles.ToListAsync();

    public Task<Roles?> getSingleResponse(Guid? id, int? intId) =>
        context.Roles.FirstOrDefaultAsync(r => r.RoleID == intId);

    public async Task postRequest(Roles t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Roles t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Roles.Any(r => r.RoleID == intId))
            await Task.Run(() => context.Update(t));
        await context.SaveChangesAsync();
    }

    public Task<List<Roles>> getResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();
}
