using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using backend.models.response;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin, Customer")]
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
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customerCollection = Request(request);
        await service.postRequest(customerCollection);
        response = await Response(customerCollection);
        return CreatedAtAction(
            actionName: nameof(GetCustomerCollection),
            routeValues: new { id = response.CCID },
            value: response);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateCustomerCollection(CustomerCollectionRequest request, Guid id)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customerCollection = Request(request);
        customerCollection.CCID = id;
        customerCollection.DateUpdated = DateTimeOffset.UtcNow;
        await service.putRequest(customerCollection, id, 0);
        response = await Response(customerCollection);
        return Ok(response);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> RemoveCustomerCollection(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new static CustomerCollection Request(CustomerCollectionRequest request) =>
            new CustomerCollection(
            request.CPID,
            request.CustomerID
        );

    [NonAction]
    private new async Task<List<CustomerCollectionResponse>> Response(List<CustomerCollection> ccollections)
    {
        try
        {
            var results = new List<CustomerCollectionResponse>();
            var customerProducts = new List<ProductQuantityResponse>();
            foreach (var item in ccollections)
            {
                var customer = await service.getCustomer(item.CustomerID);
                var customerProduct = await service.GetCustomerProduct(item.CPID);
                foreach (var cProduct in customerProduct.Products)
                {
                    customerProducts.Add(new ProductQuantityResponse(
                        cProduct.Quantity,
                        cProduct.ProductTotal,
                        cProduct.Product
                    ));
                }
                results.Add(
                    new CustomerCollectionResponse(
                        item.CCID,
                        item.CPID,
                        item.CustomerID,
                        new PartialCustomerProductResponse(
                             new CustomerResponse(
                                 customer.Name,
                                 customer.Surname,
                                 customer.Email,
                                 customer.Phone),
                            customerProducts,
                            customerProduct.Subtotal
                        )
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
    private new async Task<CustomerCollectionResponse> Response(CustomerCollection ccollection)
    {
        try
        {
            var customerProducts = new List<ProductQuantityResponse>();
            var customer = await service.getCustomer(ccollection.CustomerID);
            var customerProduct = await service.GetCustomerProduct(ccollection.CPID);
            foreach (var cProduct in customerProduct.Products)
            {
                customerProducts.Add(new ProductQuantityResponse(
                    cProduct.Quantity,
                    cProduct.ProductTotal,
                    cProduct.Product
                ));
            }
            return new CustomerCollectionResponse(
                        ccollection.CCID,
                        ccollection.CPID,
                        ccollection.CustomerID,
                        new PartialCustomerProductResponse(
                             new CustomerResponse(
                                 customer.Name,
                                 customer.Surname,
                                 customer.Email,
                                 customer.Phone),
                            customerProducts,
                            customerProduct.Subtotal
                        )
                    );
        }
        catch
        {
            return new CustomerCollectionResponse();
        }
    }
}
