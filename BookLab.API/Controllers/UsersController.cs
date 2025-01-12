using BookLab.API.Dtos;
using BookLab.Application.Dtos;
using BookLab.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        [ActionName("SignUp")]
        public async Task<ActionResult> SignUp(CreateUserRequest request)
        {            
            var createdUser = await _authService.CreateUserAsync(request);

            var userDto = createdUser.Adapt<CreateUserResponse>();

            return Ok(userDto);
        }

        [HttpPost("login")]
        [ActionName("Login")]
        public async Task<ActionResult> Login(LoginUserRequest request)
        {
            var user = await _authService.LoginUserAsync(request);

            var userDto = user.Adapt<LoginUserResponse>();

            return Ok(userDto);
        }

        [Authorize]
        [HttpPost("logout")]
        [ActionName("Logout")]
        public async Task<ActionResult> LogOut()
        {
            string token = Request.Headers.Authorization.ToString().Split(" ").Last();

            await _authService.LogoutUserAsync(token);

            return NoContent();
        }
    }
}
