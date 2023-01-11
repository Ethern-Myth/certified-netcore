using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly IProductTypeService service;
    private readonly Guid guid;
    public ProductTypeController(IProductTypeService service) =>
        this.service = service;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProductTypes() =>
        Ok(await service.getResponse());

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductType(int id) =>
        Ok(await service.getSingleResponse(guid, id));

    [HttpPost]
    public async Task<IActionResult> SaveProductType(ProductTypeRequest request)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var productType = Request(request);
        await service.postRequest(productType);
        response = Response(productType);
        return CreatedAtAction(
            actionName: nameof(GetProductType),
            routeValues: new { id = response.PDTypeID },
            value: response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProductType(ProductTypeRequest request, int id)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var productType = Request(request);
        productType.PDTypeID = id;
        await service.putRequest(productType, guid, id);
        response = Response(productType);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveProductType(int id)
    {
        await service.deleteRequest(guid, id);
        return NoContent();
    }

    [NonAction]
    private static new ProductType Request(ProductTypeRequest request) => new ProductType
    {
        Category = request.Category
    };

    [NonAction]
    private static new ProductType Response(ProductType productType)
    {
        try
        {
            return productType;
        }
        catch
        {
            return new ProductType();
        }
    }

}
