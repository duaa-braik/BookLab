using BookLab.API.Dtos;
using BookLab.Application.Dtos;
using BookLab.Application.Interfaces;
using Mapster;
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
    }
}
