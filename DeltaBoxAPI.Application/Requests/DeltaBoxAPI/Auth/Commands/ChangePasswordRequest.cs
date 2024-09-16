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
    public class ChangePasswordRequest : IRequest<Result>
    {
        public ChangePasswordModel ChangePasswordModel { get; set; }

        public ChangePasswordRequest(ChangePasswordModel changePasswordModel)
        {
            ChangePasswordModel = changePasswordModel;
        }
    }

    public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest, Result>
    {
        private readonly IAuthService _authService;

        public ChangePasswordRequestHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ChangePasswordRequest(request.ChangePasswordModel);
            return result;
        }
    }
}
