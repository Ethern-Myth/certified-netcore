using backend.interfaces;
using backend.models;
using backend.models.requests;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Customer, Admin, Super")]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService service;
    public CustomerController(ICustomerService service) =>
        this.service = service;

    [Authorize(Roles = "Admin, Super")]
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
        if (ModelState.IsValid)
        {
            var customer = await Request(request);
            bool isDuplicate = await service.IsDuplicate(customer.Email);
            if (!isDuplicate)
                await service.postRequest(customer);
            response = await Response(customer);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetCustomer),
            routeValues: new { id = response.CustomerID },
            value: response);
    }

    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> UpdateCustomer(CustomerRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var customer = await Request(request);
            customer.CustomerID = id;
            customer.DateUpdated = DateTimeOffset.UtcNow;
            await service.putRequest(customer, id, 0);
            response = await Response(customer);

        }
        else
            return Problem();
        return Ok(response);
    }

    [HttpDelete("Delete/{id:Guid}")]
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
        var roles = await service.GetRoles(customer.RoleID);
        try
        {
            return new FullCustomerResponse(
                customer.CustomerID,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.Phone,
                roles,
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
                var roles = await service.GetRoles(item.RoleID);
                results.Add(new FullCustomerResponse(
                item.CustomerID,
                item.Name,
                item.Surname,
                item.Email,
                item.Phone,
                roles,
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
