using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService service;
    private readonly Guid guid;
    public ConversionController(IConversionService service) =>
        this.service = service;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetConversions() =>
        Ok(await service.getResponse());

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetConversion(int id) =>
        Ok(await service.getSingleResponse(guid, id));

    [HttpPost]
    public async Task<IActionResult> SaveConversion(ConversionRequest request)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var conversion = Request(request);
        await service.postRequest(conversion);
        response = Response(conversion);
        return CreatedAtAction(
            actionName: nameof(GetConversion),
            routeValues: new { id = response.ConversionID },
            value: response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveConversion(int id)
    {
        await service.deleteRequest(guid, id);
        return NoContent();
    }

    [NonAction]
    private static new Conversion Request(ConversionRequest request) => new Conversion
    {
        Unit = request.Unit
    };

    [NonAction]
    private static new Conversion Response(Conversion conversion)
    {
        try
        {
            return conversion;
        }
        catch
        {
            return new Conversion();
        }
    }
}
