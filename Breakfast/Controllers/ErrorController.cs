using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Controllers;



public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() {
        return Problem();
    }
}