using Acl.Authentication.Service.Api.Dtos;
using Acl.Authentication.Service.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acl.Authentication.Service.Api.Controllers;

[ApiController]
[Route("login")]
public sealed class LoginController(ILogger<LoginController> logger, ILoginService loginService) : ControllerBase
{
    [HttpPost("authenticate")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        logger.LogInformation("Authentication by username [{Username}]", request.Username);
        return Ok(await loginService.Authenticate(request));
    }
}