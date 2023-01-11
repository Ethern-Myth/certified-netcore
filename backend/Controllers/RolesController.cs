using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRolesService service;
    private readonly Guid guid;
    public RolesController(IRolesService service) =>
        this.service = service;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetRoles() =>
        Ok(await service.getResponse());

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRole(int id) =>
        Ok(await service.getSingleResponse(guid, id));

    [HttpPost]
    public async Task<IActionResult> SaveRole(RoleRequest request)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var role = Request(request);
        await service.postRequest(role);
        response = Response(role);
        return CreatedAtAction(
            actionName: nameof(GetRole),
            routeValues: new { id = response.RoleID },
            value: response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole(RoleRequest request, int id)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var role = Request(request);
        role.RoleID = id;
        await service.putRequest(role, guid, id);
        response = Response(role);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveRole(int id)
    {
        await service.deleteRequest(guid, id);
        return NoContent();
    }

    [NonAction]
    private static new Roles Request(RoleRequest request) => new Roles
    {
        Name = request.Name
    };

    [NonAction]
    private static new Roles Response(Roles role)
    {
        try
        {
            return role;
        }
        catch
        {
            return new Roles();
        }
    }
}
