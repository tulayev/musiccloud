using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Users;
using Application.CQRS.Users.Queries;
using Application.CQRS.Users.Commands;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return HandleResponse(await Mediator.Send(new LoginUserQuery(loginDto)));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return HandleResponse(await Mediator.Send(new RegisterUserCommand(registerDto)));
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            return HandleResponse(await Mediator.Send(new GetCurrentUserQuery(User.FindFirstValue(ClaimTypes.Email))));
        }
    }
}