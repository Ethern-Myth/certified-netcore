using backend.interfaces;
using backend.models;
using backend.models.models;
using backend.models.requests;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin, Customer")]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService service;
    public CustomerController(ICustomerService service) =>
        this.service = service;

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetCustomers() =>
        Ok(await Response(await service.getResponse()));

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCustomer(Guid id) =>
        Ok(await Response(await service.getSingleResponse(id, 0)));

    [HttpPost]
    public async Task<IActionResult> SaveCustomer(CustomerRequest request)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customer = await Request(request);
        bool isDuplicate = await service.IsDuplicate(customer.Email);
        if (!isDuplicate)
            await service.postRequest(customer);
        response = await Response(customer);
        return CreatedAtAction(
            actionName: nameof(GetCustomer),
            routeValues: new { id = response.CustomerID },
            value: response);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateCustomer(CustomerRequest request, Guid id)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customer = await Request(request);
        customer.CustomerID = id;
        customer.DateUpdated = DateTimeOffset.UtcNow;
        await service.putRequest(customer, id, 0);
        response = await Response(customer);
        return Ok(response);
    }

    [HttpPut("{id:Guid}/Status/{status:bool}")]
    public async Task<IActionResult> UpdateByProductStock(Guid id, bool status)
    {
        await service.UpdateByStatus(id, status);
        return await GetCustomer(id);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> RemoveCustomer(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new async Task<Customer> Request(CustomerRequest request) =>
        await Task.Run(() => new Customer
            (
                request.Name,
                request.Surname,
                request.Email,
                request.Password,
                request.Status,
                request.RoleID,
                request.Phone
            ));

    [NonAction]
    private async new Task<FullCustomerResponse> Response(Customer customer)
    {

        try
        {
            return new FullCustomerResponse(
                customer.CustomerID,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.Phone,
                 new Roles(
                    customer.Roles.RoleID,
                    customer.Roles.Name
                ),
                customer.Status
            );
        }
        catch
        {
            return new FullCustomerResponse();
        }
    }

    [NonAction]
    private new async Task<List<FullCustomerResponse>> Response(List<Customer> customers)
    {
        try
        {
            var results = new List<FullCustomerResponse>();
            foreach (var item in customers)
            {
                results.Add(new FullCustomerResponse(
                item.CustomerID,
                item.Name,
                item.Surname,
                item.Email,
                item.Phone,
                new Roles(
                    item.Roles.RoleID,
                    item.Roles.Name
                ),
                item.Status
                ));
            }
            return results;
        }
        catch
        {
            return new List<FullCustomerResponse>();
        }
    }
}
