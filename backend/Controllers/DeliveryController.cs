using backend.interfaces;
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

}
