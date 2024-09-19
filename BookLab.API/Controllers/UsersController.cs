using BookLab.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult SignUp(CreateUserRequest request)
        {
            return Ok();
        }
    }
}
