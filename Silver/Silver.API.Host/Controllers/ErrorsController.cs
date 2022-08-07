using Microsoft.AspNetCore.Mvc;

namespace Silver.API.Host.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}