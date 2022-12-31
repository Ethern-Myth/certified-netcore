using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Helper;
using backend.models;
using backend.models.models;
using backend.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using backend.models.requests;
using backend.models.responses;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JWTSettings jwtSettings;
    private readonly ICustomerService service;

    public AuthController(IOptions<JWTSettings> jwtSettings, ICustomerService service)
    {
        this.jwtSettings = jwtSettings.Value;
        this.service = service;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(Login login)
    {
        var customer = await Authenticate(login);
        if (customer != null)
        {
            var token = await Generate(customer);
            return Ok(new { token = token, customer = await Response(customer) });
        }
        return NotFound("User not found");
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(CustomerRequest request)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var customer = await Request(request);
        bool isDuplicate = await service.IsDuplicate(customer.Email);
        if (!isDuplicate)
            await service.postRequest(customer);
        response = await Response(customer);
        return Ok(response);
    }

    [NonAction]
    private async Task<string> Generate(Customer customer)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(ClaimTypes.Role, customer.Roles.Name),
            }),
            Expires = DateTime.Now.AddMinutes(1440),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return await Task.Run(() => tokenHandler.WriteToken(token));
    }

    [NonAction]
    private async Task<Customer?> Authenticate(Login login)
    {
        var currentUser = await service.GetCurrentUser(login.Email, login.Password);
        if (currentUser != null)
        {
            return currentUser;
        }
        return null;
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
                customer.Roles,
                customer.Status
            );
        }
        catch
        {
            return new FullCustomerResponse();
        }
    }
}
