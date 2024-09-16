using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token.Commands;
using DeltaBoxAPI.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public TokenController(IMediator mediator, ApplicationDbContext context = null)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest command)
        {
            if (command == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //Revoken is used for removing token entry
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                var user = _context.TokenInfos.SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    return BadRequest();
                }

                user.RefreshToken = null;
                _context.SaveChanges();

                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
