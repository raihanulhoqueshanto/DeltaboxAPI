using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Token.Commands
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public RefreshTokenRequest(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }

    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenRequestHandler(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _tokenService.RefreshTokenRequest(request);
            return result;
        }
    }
}
