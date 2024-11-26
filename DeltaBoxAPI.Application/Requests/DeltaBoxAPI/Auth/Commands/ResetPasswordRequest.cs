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
    public class ResetPasswordRequest : IRequest<Result>
    {
        public ResetPasswordModel ResetPasswordModel { get; set; }

        public ResetPasswordRequest(ResetPasswordModel resetPasswordModel)
        {
            ResetPasswordModel = resetPasswordModel;
        }
    }

    public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, Result>
    {
        private readonly IAuthService _authService;

        public ResetPasswordRequestHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<Result> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            return await _authService.ResetPasswordRequest(request.ResetPasswordModel);
        }
    }
}
