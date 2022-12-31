using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[Authorize(Roles = "Admin, Driver")]
[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService service;

    public DeliveryController(IDeliveryService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetDeliveries() =>
        Ok(await service.getResponse());

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetDelivery(Guid id) =>
        Ok(await service.getSingleResponse(id, 0));

    [HttpPost]
    public async Task<IActionResult> SaveDelivery(DeliveryRequest request)
    {
        var response = new Delivery();
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var order = Request(request);
        await service.postRequest(order);
        response = order;
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDelivery(DeliveryRequest request)
    {
        var response = new Delivery();
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var order = Request(request);
        await service.putRequest(order, order.OrderID, 0);
        response = order;
        return Ok(response);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> RemoveDelivery(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new Delivery Request(DeliveryRequest request) =>
        new Delivery(
            request.OrderID,
            request.IsDelivered
        );

}
