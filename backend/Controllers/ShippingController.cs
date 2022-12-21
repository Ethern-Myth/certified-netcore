using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin, Customer")]
[ApiController]
[Route("api/[controller]")]
public class ShippingController : ControllerBase
{
    private readonly IShippingService service;
    private Guid guid;
    public ShippingController(IShippingService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetShippingAddress() =>
        Ok(await Response(await service.getResponse()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetShipping(int? id) =>
        Ok(await Response(await service.getSingleResponse(guid, id)));

    [HttpPost]
    public async Task<IActionResult> SaveShippingAddress(ShippingRequest request)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var shipping = Request(request);
            await service.postRequest(shipping);
            response = await Response(shipping);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetShipping),
            routeValues: new { id = response.ShippingID },
            value: response);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateShippingAddress(ShippingRequest request, int id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var shipping = Request(request);
            shipping.ShippingID = id;
            await service.putRequest(shipping, guid, id);
            response = await Response(shipping);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetShipping),
            routeValues: new { id = response.ShippingID },
            value: response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveCustomer(int id)
    {
        await service.deleteRequest(guid, id);
        return NoContent();
    }

    [NonAction]
    private new Shipping Request(ShippingRequest request) =>
        new Shipping(
            request.CustomerID,
            request.AddressLine1,
            request.AddressLine2,
            request.Suburb,
            request.Town,
            request.Region,
            request.PostalCode,
            request.Name
            );

    [NonAction]
    private new async Task<ShippingResponse> Response(Shipping shipping)
    {
        try
        {
            var customer = await service.getCustomer(shipping.CustomerID);
            var country = await service.getCountry(shipping.Name);
            return new ShippingResponse(
                shipping.ShippingID,
                new CustomerResponse(
                    customer.Name,
                    customer.Surname,
                    customer.Email,
                    customer.Phone
                ),
                shipping.AddressLine1,
                shipping.AddressLine2,
                shipping.Suburb,
                shipping.Town,
                shipping.Region,
                shipping.PostalCode,
                country
            );
        }
        catch
        {
            return new ShippingResponse();
        }
    }

    [NonAction]
    private new async Task<List<ShippingResponse>> Response(List<Shipping> shippings)
    {
        try
        {
            var results = new List<ShippingResponse>();
            foreach (var item in shippings)
            {
                var customer = await service.getCustomer(item.CustomerID);
                var country = await service.getCountry(item.Name);
                results.Add(
                    new ShippingResponse(
                item.ShippingID,
                new CustomerResponse(
                    customer.Name,
                    customer.Surname,
                    customer.Email,
                    customer.Phone
                ),
                item.AddressLine1,
                item.AddressLine2,
                item.Suburb,
                item.Town,
                item.Region,
                item.PostalCode,
                country
            )
                );
            }
            return results;
        }
        catch
        {
            return new List<ShippingResponse>();
        }
    }
}
