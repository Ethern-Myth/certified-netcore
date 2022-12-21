using System.Net;
using backend.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin, Customer")]
[ApiController]
[Route("api/[controller]")]
public class ShippingController : ControllerBase
{
    private readonly IShippingService service;

    public ShippingController(IShippingService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetShippingAddress() =>
        Ok(await service.getResponse());

}
