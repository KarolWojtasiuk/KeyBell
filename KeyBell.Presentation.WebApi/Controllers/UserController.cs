using Microsoft.AspNetCore.Mvc;

namespace KeyBell.Presentation.WebApi.Controllers;

[ApiController]
[Consumes("application/pgp-encrypted")]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("Register")]
    public Task<IActionResult> RegisterAsync([FromBody] char[] test)
    {
        return Task.FromResult<IActionResult>(Ok(test.Length));
    }
}