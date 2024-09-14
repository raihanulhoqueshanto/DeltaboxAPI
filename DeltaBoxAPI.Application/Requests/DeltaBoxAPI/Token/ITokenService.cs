using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token.Commands;
using DeltaboxAPI.Domain.Entities.DeltaBox.Common.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token
{
    public interface ITokenService : IDisposable
    {
        // For previous authorization
        string CreateJwtToken(IdentityUser user);

        // For new Authorization
        TokenResponse GetToken(IEnumerable<Claim> claim);
        string GetRefreshToken();
        ClaimsPrincipal GetPrinciplalFromExpiredToken(string token);

        Task<RefreshTokenResponse> RefreshTokenRequest(RefreshTokenRequest request);
    }
}
