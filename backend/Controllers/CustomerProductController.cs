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
       Ok(await Response(await service.getResponse()));

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCustomerProduct(Guid id) =>
            Ok(await Response(await service.getSingleResponse(id, 0)));

    [HttpPost]
    public async Task<IActionResult> SaveCustomerProduct(CustomerProductRequest request)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customerProduct = await Request(request);
        await service.postRequest(customerProduct);
        response = await Response(customerProduct);
        return CreatedAtAction(
            actionName: nameof(GetCustomerProduct),
            routeValues: new { id = response.CPID },
            value: response);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateCustomerProduct(CustomerProductRequest request, Guid id)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customerProduct = await Request(request);
        customerProduct.CPID = id;
        customerProduct.DateUpdated = DateTimeOffset.UtcNow;
        await service.putRequest(customerProduct, id, 0);
        response = await Response(customerProduct);
        return Ok(response);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> RemoveCustomerProduct(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new async Task<CustomerProduct> Request(CustomerProductRequest request)
    {
        var productQuantity = new List<ProductQuantity>();
        foreach (var item in request.Products)
        {
            var result = await this.service.getProduct(item.ProductID);
            productQuantity.Add(new ProductQuantity(
             item.Quantity,
             result
            ));
        }
        return new CustomerProduct(
            request.CustomerID,
            productQuantity
        );
    }

    [NonAction]
    private new async Task<CustomerProductResponse> Response(CustomerProduct customerProduct)
    {
        try
        {
            var customer = await service.getCustomer(customerProduct.CustomerID);
            var products = new List<ProductQuantityResponse>();
            foreach (var item in customerProduct.Products)
            {
                products.Add(new ProductQuantityResponse(
                    item.Quantity,
                    item.ProductTotal,
                    item.Product
                ));
            }
            return new CustomerProductResponse(
             customerProduct.CPID,
             customerProduct.CustomerID,
             new CustomerResponse(
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.Phone
             ),
            products,
            customerProduct.Subtotal
        );
        }
        catch
        {
            return new CustomerProductResponse();
        }
    }
    [NonAction]
    private new async Task<List<CustomerProductResponse>> Response(List<CustomerProduct> customerProducts)
    {
        try
        {
            var results = new List<CustomerProductResponse>();
            var products = new List<ProductQuantityResponse>();
            foreach (var customerProduct in customerProducts)
            {
                var customer = await service.getCustomer(customerProduct.CustomerID);
                foreach (var item in customerProduct.Products)
                {
                    products.Add(new ProductQuantityResponse(
                        item.Quantity,
                        item.ProductTotal,
                        item.Product
                    ));
                }
                results.Add(new CustomerProductResponse(
                 customerProduct.CPID,
                 customerProduct.CustomerID,
                 new CustomerResponse(
                    customer.Name,
                    customer.Surname,
                    customer.Email,
                    customer.Phone
                 ),
                products,
                customerProduct.Subtotal
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
