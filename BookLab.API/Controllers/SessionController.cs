using BookLab.Application.Dtos;
using BookLab.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BookLab.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("refresh_token")]
        public async Task<ActionResult> RefreshSession(string token)
        {
            var session = await _sessionService.RefreshSessionAsync(token);

            var sessionDto = session.Adapt<SessionDto>();

            return Ok(sessionDto);
        }
    }
}
