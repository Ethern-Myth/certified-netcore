using backend.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService service;

    public CountryController(ICountryService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetCountries() =>
        Ok(await service.getResponse());
}
