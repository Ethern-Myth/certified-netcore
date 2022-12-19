using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using backend.models.response;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Customer, Admin, Super")]
[ApiController]
[Route("api/[controller]")]
public class CustomerProductController : ControllerBase
{
    private readonly ICustomerProductService service;
    public CustomerProductController(ICustomerProductService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetCustomerProducts() =>
       Ok(Response(await service.getResponse()));

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCustomerProduct(Guid id) =>
            Ok(Response(await service.getResponse(id, 0)));

    [HttpPost]
    public async Task<IActionResult> SaveCustomerProduct(CustomerProductRequest request)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var customerProduct = await Request(request);
            await service.postRequest(customerProduct);
            response = await Response(customerProduct);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetCustomerProduct),
            routeValues: new { id = response[0].CPID },
            value: response);
    }

    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> UpdateCustomerProduct(CustomerProductRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var customerProduct = await Request(request);
            customerProduct.CPID = id;
            customerProduct.DateUpdated = DateTimeOffset.UtcNow;
            await service.putRequest(customerProduct, id, 0);
            response = await Response(customerProduct);
        }
        else
            return Problem();
        return Ok(response);
    }

    [HttpDelete("Delete/{id:Guid}")]
    public async Task<IActionResult> RemoveCustomerProduct(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new async Task<CustomerProduct> Request(CustomerProductRequest request)
    {
        var products = new List<Product>();
        foreach (var item in request.Products)
        {
            var result = await this.service.getProduct(item);
            products.Add(result);
        }
        return await Task.Run(() => new CustomerProduct(
            request.CustomerID,
            products
        ));
    }

    [NonAction]
    private new async Task<List<CustomerProductResponse>> Response(CustomerProduct customerProduct)
    {
        try
        {
            var results = new List<CustomerProductResponse>();
            var customer = await service.getCustomer(customerProduct.CustomerID);
            results.Add(new CustomerProductResponse(
                 customerProduct.CPID,
                 customerProduct.CustomerID,
                 new CustomerResponse(
                    customer.Name,
                    customer.Surname,
                    customer.Email,
                    customer.Phone
                 ),
                 customerProduct.Products,
                 customerProduct.Subtotal
            ));
            return results;
        }
        catch
        {
            return new List<CustomerProductResponse>();
        }
    }
    [NonAction]
    private new List<CustomerProductResponse> Response(List<CustomerProduct> customerProducts)
    {
        try
        {
            var results = new List<CustomerProductResponse>();
            foreach (var item in customerProducts)
            {
                results.Add(new CustomerProductResponse(
                 item.CPID,
                 item.CustomerID,
                 new CustomerResponse(
                    item.Customer.Name,
                    item.Customer.Surname,
                    item.Customer.Email,
                    item.Customer.Phone
                 ),
                 item.Products,
                 item.Subtotal
            ));
            }
            return results;
        }
        catch
        {
            return new List<CustomerProductResponse>();
        }
    }
}
