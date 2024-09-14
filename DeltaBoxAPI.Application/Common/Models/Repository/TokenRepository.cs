using DeltaboxAPI.Domain.Entities.DeltaBox.Common.DTO;
using DeltaBoxAPI.Application.Common.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Common.Models.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //// For previous authorization
        //public string CreateJwtToken(IdentityUser user)
        //{
        //    var claims = new List<Claim>();
        //    claims.Add(new Claim(ClaimTypes.Email, user.Email));

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        _configuration["Jwt:Issuer"],
        //        _configuration["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(15),
        //        signingCredentials: credentials
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //// For New authorization

        //public ClaimsPrincipal GetPrinciplalFromExpiredToken(string token)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        //        ValidateLifetime = false
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;

        //    // Principal
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;

        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }

        //    return principal;
        //}

        //public string GetRefreshToken()
        //{
        //    var randomNumber = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        return Convert.ToBase64String(randomNumber);
        //    }
        //}

        //public TokenResponse GetToken(IEnumerable<Claim> claim)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Audience"],
        //        expires: DateTime.Now.AddDays(7),
        //        claims: claim,
                
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //        );

        //    string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        //    return new TokenResponse { TokenString = tokenString, ValidTo = token.ValidTo };
        //}
    }
}
