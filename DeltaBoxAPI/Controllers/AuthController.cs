using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token;
using DeltaBoxAPI.Application.Common.Models.DTO;
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
        //private readonly UserManager<IdentityUser> userManager;
        //private readonly ITokenService tokenService;

        //public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        //{
        //    this.userManager = userManager;
        //    this.tokenService = tokenService;
        //}

        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        //{
        //    var identityUser = new IdentityUser
        //    {
        //        Email = registerRequestDto.Email,
        //        UserName = registerRequestDto.Username,
        //    };

        //    var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

        //    if (identityResult.Succeeded)
        //    {
        //        return Ok("User is registered! Please login.");
        //    }

        //    return BadRequest("Something went wrong!");
        //}


        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        //{
        //    var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

        //    if (user != null)
        //    {
        //        var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        //        if (checkPasswordResult)
        //        {
        //            var jwtToken = tokenService.CreateJwtToken(user);

        //            var response = new LoginResponseDto
        //            {
        //                JwtToken = jwtToken,
        //            };
        //            return Ok(response);
        //        }
        //    }

        //    return BadRequest("Email or Password incorrect!");
        //}

        public async Task<IActionResult> Login(LoginModel command)
        {
            var result = await _mediator.Send(new LoginRequest(command));
            return Ok(result);
        }
    }
}
