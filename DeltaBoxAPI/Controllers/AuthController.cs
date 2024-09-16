using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginModel command)
        {
            var result = await _mediator.Send(new LoginRequest(command));
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UserRegistration(RegistrationModel command)
        {
            var result = await _mediator.Send(new UserRegistrationRequest(command));
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AdminRegistration(RegistrationModel command)
        {
            var result = await _mediator.Send(new AdminRegistrationRequest(command));
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel command)
        {
            var result = await _mediator.Send(new ChangePasswordRequest(command));
            return Ok(result);
        }
    }
}
