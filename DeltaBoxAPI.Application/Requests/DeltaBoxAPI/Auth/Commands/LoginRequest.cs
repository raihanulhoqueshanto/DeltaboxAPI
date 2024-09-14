using DeltaboxAPI.Application.Common.Models.DTO;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Auth.Commands
{
    public class LoginRequest : IRequest<Result>
    {
        public LoginModel LoginModel { get; set; }

        public LoginRequest(LoginModel loginModel)
        {
            LoginModel = loginModel;
        }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, Result>
    {
        private readonly IAuthService _authService;

        public LoginRequestHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<Result> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginRequest(request.LoginModel);
            return result;
        }
    }
}
