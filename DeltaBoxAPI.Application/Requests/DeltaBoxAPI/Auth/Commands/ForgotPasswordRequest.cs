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
    public class ForgotPasswordRequest : IRequest<Result>
    {
        public ForgotPasswordModel ForgotPasswordModel { get; set; }

        public ForgotPasswordRequest(ForgotPasswordModel forgotPasswordModel)
        {
            ForgotPasswordModel = forgotPasswordModel;
        }
    }

    public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordRequest, Result>
    {
        private readonly IAuthService _authService;

        public ForgotPasswordRequestHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<Result> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _authService.ForgotPasswordRequest(request.ForgotPasswordModel);
        }
    }
}
