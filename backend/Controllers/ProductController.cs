using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Customer, Admin, Super")]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService service;

    public ProductController(IProductService service) =>
        this.service = service;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProducts() =>
        Ok(await service.getResponse());

    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetProduct(Guid id) =>
        Ok(await service.getSingleResponse(id, 0));

    [HttpPost]
    public async Task<IActionResult> SaveProduct(ProductRequest request)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var product = await Request(request);
            await service.postRequest(product);
            response = await Response(product);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetProduct),
            routeValues: new { id = response.ProductID },
            value: response);
    }

    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> UpdateProduct(ProductRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var product = await Request(request);
            product.ProductID = id;
            product.DateUpdated = DateTimeOffset.UtcNow;
            await service.putRequest(product, id, 0);
            response = await Response(product);
        }
        else
            return Problem();
        return Ok(response);
    }

    [HttpDelete("Delete/{id:Guid}")]
    public async Task<IActionResult> RemoveProduct(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new async Task<Product> Request(ProductRequest request) =>
        await Task.Run(() => new Product(
            request.Name,
            request.Price,
            request.InStock,
            request.PDTypeID
            ));


    [NonAction]
    private new async Task<Product> Response(Product product)
    {
        try
        {
            var productType = await service.GetProductTypes(product.PDTypeID);
            return new Product(
                product.ProductID,
                product.Name,
                product.Price,
                product.InStock,
                productType,
                product.PDTypeID
            );
        }
        catch
        {
            return new Product();
        }
    }
}
