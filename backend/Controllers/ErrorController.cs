using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
public class ErrorController : Controller
{
    [Route("/error")]
    [NonAction]
    public IActionResult Error()
    {
        return Problem();
    }
}