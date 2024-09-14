using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> LoginRequest(LoginModel request)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _context.TokenInfos.FirstOrDefault(a => a.Username == user.UserName);

                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Username = user.UserName,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(1)
                    };
                    _context.TokenInfos.Add(info);
                }
                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Result.Failure(new List<string> { "Invalid info. try again!" }, null, null, "500");
                }

                var data = new LoginResponse
                {
                    Name = user.Name,
                    Username = user.UserName,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                };

                return Result.Success("Success", data, "200");

            }
            //login failed condition

            return Result.Failure(new List<string> { "Login failed! Try again." }, null, null, "500");
        }
    }
}
