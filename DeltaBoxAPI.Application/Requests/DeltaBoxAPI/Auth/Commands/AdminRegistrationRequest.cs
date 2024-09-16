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
    public class AdminRegistrationRequest : IRequest<Result>
    {
        public RegistrationModel RegistrationModel { get; set; }

        public AdminRegistrationRequest(RegistrationModel registrationModel)
        {
            RegistrationModel = registrationModel;
        }
    }

    public class AdminRegistrationRequestHandler : IRequestHandler<AdminRegistrationRequest, Result>
    {
        private readonly IAuthService _authService;

        public AdminRegistrationRequestHandler(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        public async Task<Result> Handle(AdminRegistrationRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.AdminRegistrationRequest(request.RegistrationModel);
            return result;
        }
    }
}
