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
public class CustomerCollectionController : ControllerBase
{
    private readonly ICustomerCollectionService service;

    public CustomerCollectionController(ICustomerCollectionService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetCustomerCollections() =>
          Ok(await Response(await service.getResponse()));

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCustomerCollection(Guid id) =>
            Ok(await Response(await service.getResponse(id, 0)));

    [HttpPost]
    public async Task<IActionResult> SaveCustomerCollection(CustomerCollectionRequest request)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var customerCollection = Request(request);
            await service.postRequest(customerCollection);
            response = await Response(customerCollection);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetCustomerCollection),
            routeValues: new { id = response[0].CCID },
            value: response);
    }

    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> UpdateCustomerCollection(CustomerCollectionRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var customerCollection = Request(request);
            customerCollection.CCID = id;
            customerCollection.DateUpdated = DateTimeOffset.UtcNow;
            await service.putRequest(customerCollection, id, 0);
            response = await Response(customerCollection);
        }
        else
            return Problem();
        return Ok(response);
    }

    [HttpDelete("Delete/{id:Guid}")]
    public async Task<IActionResult> RemoveCustomerCollection(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new static CustomerCollection Request(CustomerCollectionRequest request) =>
            new CustomerCollection(
            request.CPID,
            request.CustomerID,
            request.IsAvailable
        );

    [NonAction]
    private new async Task<List<CustomerCollectionResponse>> Response(List<CustomerCollection> ccollections)
    {
        try
        {
            var results = new List<CustomerCollectionResponse>();
            foreach (var item in ccollections)
            {
                var customerProduct = await service.GetCustomerProduct(item.CPID);
                results.Add(
                new CustomerCollectionResponse(
                    item.CCID,
                    item.CPID,
                    item.CustomerID,
                    new PartialCustomerProductResponse(
                         new CustomerResponse(
                        customerProduct.Customer.Name,
                        customerProduct.Customer.Surname,
                        customerProduct.Customer.Email,
                        customerProduct.Customer.Phone
                        ),
                        customerProduct.Products,
                        customerProduct.Subtotal
                    ),
                    item.IsAvailable
                )
            );
            }
            return results;
        }
        catch
        {
            return new List<CustomerCollectionResponse>();
        }
    }

    [NonAction]
    private new async Task<List<CustomerCollectionResponse>> Response(CustomerCollection ccollection)
    {
        try
        {
            var results = new List<CustomerCollectionResponse>();
            var customerProduct = await service.GetCustomerProduct(ccollection.CPID);
            results.Add(
                new CustomerCollectionResponse(
                    ccollection.CCID,
                    ccollection.CPID,
                    ccollection.CustomerID,
                    new PartialCustomerProductResponse(
                         new CustomerResponse(
                        customerProduct.Customer.Name,
                        customerProduct.Customer.Surname,
                        customerProduct.Customer.Email,
                        customerProduct.Customer.Phone
                        ),
                        customerProduct.Products,
                        customerProduct.Subtotal
                    ),
                    ccollection.IsAvailable
                )
            );
            return results;
        }
        catch
        {
            return new List<CustomerCollectionResponse>();
        }
    }
}
